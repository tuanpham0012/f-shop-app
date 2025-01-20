using System.Collections;
using System.Collections.Generic;
using InfluxDB.Client.Api.Domain;
using ShopAppApi.Response;

namespace ShopAppApi.Response
{

    public class SuccessResponse(int _code = 200, string _message = "")
    {
        public int Code { get; set; } = _code;
        public string Message { get; set; } = _message;
    }
    public class ResponsePaginatedCollection<T>: SuccessResponse
    {
        public Object? Meta { get; set; }
        public IEnumerable? Data { get; set; } = new List<object>();

        public ResponsePaginatedCollection(PaginatedList<T> paginatedList, int _code = 200, string _message = "")
        {
            Code = _code;
            Message = _message;
            Data = paginatedList.Items;
            Meta = new { currentPage = paginatedList.CurrentPage, pageSize = paginatedList.PageSize, totalCount = paginatedList.TotalCount, totalPages = paginatedList.TotalPages };
        }
    }

    public class ResponseCollection<T> : SuccessResponse
    {
        public IEnumerable<T>? Data { get; set; }

        public ResponseCollection(IEnumerable<T> list, int _code = 200, string _message = "")
        {
            Code = _code;
            Message = _message;
            Data = list;
        }
    }

    public class ResponseOne<T> : SuccessResponse
    {
        public T? Data { get; set; }
        public ResponseOne(T _data, int _code = 200, string _message = "")
        {
            Code = _code;
            Message = _message;
            Data = _data;
        }
    }

    public class ErrorResponse(int _code = 404, string _message = "", object? errors = null)
    {
        public int Code { get; set; } = _code;
        public int Status { get; set; } = _code;
        public string Title { get; set; } = _message;
        public object? Errors { get; set; } = errors;
    }
} 
