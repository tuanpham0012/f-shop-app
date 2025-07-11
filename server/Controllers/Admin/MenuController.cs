using Microsoft.AspNetCore.Mvc;
using ShopAppApi.Data;
using ShopAppApi.Repositories.Menus;
using ShopAppApi.Request;
using ShopAppApi.Response;
using ShopAppApi.Services.TelegramBotRepository;
using ShopAppApi.ViewModels;

namespace ShopAppApi.Controllers.Admin
{
    [ApiController]
    [Route("menus")]
    public class MenuController(IMenuRepository repository, ITelegramRepository telegramRepository) : Controller
    {
        private readonly IMenuRepository _repo = repository;
        private readonly ITelegramRepository _telegramRepository = telegramRepository;
        private readonly Dictionary<string, int> menuType = new()
        {
            {"admin", 1}, {"user", 2}
        };

        [HttpGet("get-tree")]
        public async Task<IActionResult> GetTree([FromQuery(Name = "type")] string? type)
        {
            var mnType = type != null && menuType.ContainsKey(type) ? menuType[type] : 0;
            var entries = await _repo.GetAll();
            var tree = _repo.BuildTree(entries);

            return Ok(new ResponseCollection<MenuTree>(mnType > 0 ? tree.Where(x => x.Id == mnType).First().Children : tree));
        }

        [HttpGet("admin-menu")]
        public async Task<IActionResult> GetAdminMenu()
        {
            var entries = await _repo.GetAdminMenu();

            return Ok(new ResponseCollection<MenuTree>(entries));
        }

        [HttpGet("user-menu")]
        public async Task<IActionResult> GetUserMenu()
        {
            var entries = await _repo.GetUserMenu();

            return Ok(new ResponseCollection<MenuTree>(entries));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var entries = await _repo.GetAll();
            var tree = _repo.BuildTree(entries);

            return Ok(new ResponseCollection<MenuTree>(tree));
        }

        [HttpPost]
        public async Task<IActionResult> Store(StoreMenuRequest menu)
        {
            try
            {
                await _repo.Create(menu);
                return Ok(new SuccessResponse(200, "Thêm mới thành công"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Show(long Id)
        {
            try
            {
                var entry = await _repo.Show(Id);
                return Ok(new ResponseOne<Menu>(entry));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(400,ex.Message));
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(long Id, UpdateMenuRequest menu)
        {
            try
            {
                await _repo.Update(Id, menu);
                return Ok(new SuccessResponse(200, "Cập nhật thành công"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(422, ex.Message, ex.ToString()));
            }
        }

        [HttpPost("test-alert")]
        public async Task<IActionResult> Alert(TelegramMessage data)
        {
            Console.WriteLine(data);
            await _telegramRepository.SendMessage(data._message ?? "send message");
            return Ok(data);
        }
    }
}
