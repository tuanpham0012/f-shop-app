<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;

class DistrictSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        
        DB::table('districts')->insert([
             [
              "name" => "Ba Đình",
              "province_id" => 1
            ],
             [
              "name" => "Ba Vì",
              "province_id" => 1
            ],
             [
              "name" => "Bắc Từ Liêm",
              "province_id" => 1
            ],
             [
              "name" => "Cầu Giấy",
              "province_id" => 1
            ],
             [
              "name" => "Chương Mỹ",
              "province_id" => 1
            ],
             [
              "name" => "Đan Phượng",
              "province_id" => 1
            ],
             [
              "name" => "Đông Anh",
              "province_id" => 1
            ],
             [
              "name" => "Đống Đa",
              "province_id" => 1
            ],
             [
              "name" => "Gia Lâm",
              "province_id" => 1
            ],
             [
              "name" => "Hà Đông",
              "province_id" => 1
            ],
             [
              "name" => "Hai Bà Trưng",
              "province_id" => 1
            ],
             [
              "name" => "Hoài Đức",
              "province_id" => 1
            ],
             [
              "name" => "Hoàn Kiếm",
              "province_id" => 1
            ],
             [
              "name" => "Hoàng Mai",
              "province_id" => 1
            ],
             [
              "name" => "Long Biên",
              "province_id" => 1
            ],
             [
              "name" => "Mê Linh",
              "province_id" => 1
            ],
             [
              "name" => "Mỹ Đức",
              "province_id" => 1
            ],
             [
              "name" => "Nam Từ Liêm",
              "province_id" => 1
            ],
             [
              "name" => "Phú Xuyên",
              "province_id" => 1
            ],
             [
              "name" => "Phúc Thọ",
              "province_id" => 1
            ],
             [
              "name" => "Quốc Oai",
              "province_id" => 1
            ],
             [
              "name" => "Sóc Sơn",
              "province_id" => 1
            ],
             [
              "name" => "Sơn Tây",
              "province_id" => 1
            ],
             [
              "name" => "Tây Hồ",
              "province_id" => 1
            ],
             [
              "name" => "Thạch Thất",
              "province_id" => 1
            ],
             [
              "name" => "Thanh Oai",
              "province_id" => 1
            ],
             [
              "name" => "Thanh Trì",
              "province_id" => 1
            ],
             [
              "name" => "Thanh Xuân",
              "province_id" => 1
            ],
             [
              "name" => "Thường Tín",
              "province_id" => 1
            ],
             [
              "name" => "Ứng Hòa",
              "province_id" => 1
            ],
             [
              "name" => "Bình Chánh",
              "province_id" => 2
            ],
             [
              "name" => "Bình Tân",
              "province_id" => 2
            ],
             [
              "name" => "Bình Thạnh",
              "province_id" => 2
            ],
             [
              "name" => "Cần Giờ",
              "province_id" => 2
            ],
             [
              "name" => "Củ Chi",
              "province_id" => 2
            ],
             [
              "name" => "Gò Vấp",
              "province_id" => 2
            ],
             [
              "name" => "Hóc Môn",
              "province_id" => 2
            ],
             [
              "name" => "Nhà Bè",
              "province_id" => 2
            ],
             [
              "name" => "Phú Nhuận",
              "province_id" => 2
            ],
             [
              "name" => "Quận 1",
              "province_id" => 2
            ],
             [
              "name" => "Quận 10",
              "province_id" => 2
            ],
             [
              "name" => "Quận 11",
              "province_id" => 2
            ],
             [
              "name" => "Quận 12",
              "province_id" => 2
            ],
             [
              "name" => "Quận 2",
              "province_id" => 2
            ],
             [
              "name" => "Quận 3",
              "province_id" => 2
            ],
             [
              "name" => "Quận 4",
              "province_id" => 2
            ],
             [
              "name" => "Quận 5",
              "province_id" => 2
            ],
             [
              "name" => "Quận 6",
              "province_id" => 2
            ],
             [
              "name" => "Quận 7",
              "province_id" => 2
            ],
             [
              "name" => "Quận 8",
              "province_id" => 2
            ],
             [
              "name" => "Quận 9",
              "province_id" => 2
            ],
             [
              "name" => "Tân Bình",
              "province_id" => 2
            ],
             [
              "name" => "Tân Phú",
              "province_id" => 2
            ],
             [
              "name" => "Thủ Đức",
              "province_id" => 2
            ],
             [
              "name" => "Ba Đồn",
              "province_id" => 3
            ],
             [
              "name" => "Bố Trạch",
              "province_id" => 3
            ],
             [
              "name" => "Đồng Hới",
              "province_id" => 3
            ],
             [
              "name" => "Lệ Thủy",
              "province_id" => 3
            ],
             [
              "name" => "Minh Hóa",
              "province_id" => 3
            ],
             [
              "name" => "Quảng Ninh",
              "province_id" => 3
            ],
             [
              "name" => "Quảng Trạch",
              "province_id" => 3
            ],
             [
              "name" => "Tuyên Hóa",
              "province_id" => 3
            ],
             [
              "name" => "Bắc Trà My",
              "province_id" => 4
            ],
             [
              "name" => "Duy Xuyên",
              "province_id" => 4
            ],
             [
              "name" => "Đại Lộc",
              "province_id" => 4
            ],
             [
              "name" => "Điện Bàn",
              "province_id" => 4
            ],
             [
              "name" => "Đông Giang",
              "province_id" => 4
            ],
             [
              "name" => "Hiệp Đức",
              "province_id" => 4
            ],
             [
              "name" => "Hội An",
              "province_id" => 4
            ],
             [
              "name" => "Nam Giang",
              "province_id" => 4
            ],
             [
              "name" => "Nam Trà My",
              "province_id" => 4
            ],
             [
              "name" => "Nông Sơn",
              "province_id" => 4
            ],
             [
              "name" => "Núi Thành",
              "province_id" => 4
            ],
             [
              "name" => "Phú Ninh",
              "province_id" => 4
            ],
             [
              "name" => "Phước Sơn",
              "province_id" => 4
            ],
             [
              "name" => "Quế Sơn",
              "province_id" => 4
            ],
             [
              "name" => "Tam Kỳ",
              "province_id" => 4
            ],
             [
              "name" => "Tây Giang",
              "province_id" => 4
            ],
             [
              "name" => "Thăng Bình",
              "province_id" => 4
            ],
             [
              "name" => "Tiên Phước",
              "province_id" => 4
            ],
             [
              "name" => "Ba Chẽ",
              "province_id" => 5
            ],
             [
              "name" => "Bình Liêu",
              "province_id" => 5
            ],
             [
              "name" => "Cẩm Phả",
              "province_id" => 5
            ],
             [
              "name" => "Cô Tô",
              "province_id" => 5
            ],
             [
              "name" => "Đầm Hà",
              "province_id" => 5
            ],
             [
              "name" => "Đông Triều",
              "province_id" => 5
            ],
             [
              "name" => "Hạ Long",
              "province_id" => 5
            ],
             [
              "name" => "Hải Hà",
              "province_id" => 5
            ],
             [
              "name" => "Hoành Bồ",
              "province_id" => 5
            ],
             [
              "name" => "Móng Cái",
              "province_id" => 5
            ],
             [
              "name" => "Quảng Yên",
              "province_id" => 5
            ],
             [
              "name" => "Tiên Yên",
              "province_id" => 5
            ],
             [
              "name" => "Uông Bí",
              "province_id" => 5
            ],
             [
              "name" => "Vân Đồn",
              "province_id" => 5
            ],
             [
              "name" => "Châu Thành",
              "province_id" => 6
            ],
             [
              "name" => "Cù Lao Dung",
              "province_id" => 6
            ],
             [
              "name" => "Kế Sách",
              "province_id" => 6
            ],
             [
              "name" => "Long Phú",
              "province_id" => 6
            ],
             [
              "name" => "Mỹ Tú",
              "province_id" => 6
            ],
             [
              "name" => "Mỹ Xuyên",
              "province_id" => 6
            ],
             [
              "name" => "Ngã Năm",
              "province_id" => 6
            ],
             [
              "name" => "Sóc Trăng",
              "province_id" => 6
            ],
             [
              "name" => "Thạnh Trị",
              "province_id" => 6
            ],
             [
              "name" => "Trần Đề",
              "province_id" => 6
            ],
             [
              "name" => "Vĩnh Châu",
              "province_id" => 6
            ],
             [
              "name" => "Bắc Yên",
              "province_id" => 7
            ],
             [
              "name" => "Mai Sơn",
              "province_id" => 7
            ],
             [
              "name" => "Mộc Châu",
              "province_id" => 7
            ],
             [
              "name" => "Mường La",
              "province_id" => 7
            ],
             [
              "name" => "Phù Yên",
              "province_id" => 7
            ],
             [
              "name" => "Quỳnh Nhai",
              "province_id" => 7
            ],
             [
              "name" => "Sơn La",
              "province_id" => 7
            ],
             [
              "name" => "Sông Mã",
              "province_id" => 7
            ],
             [
              "name" => "Sốp Cộp",
              "province_id" => 7
            ],
             [
              "name" => "Thuận Châu",
              "province_id" => 7
            ],
             [
              "name" => "Vân Hồ",
              "province_id" => 7
            ],
             [
              "name" => "Yên Châu",
              "province_id" => 7
            ],
             [
              "name" => "Đông Hưng",
              "province_id" => 8
            ],
             [
              "name" => "Hưng Hà",
              "province_id" => 8
            ],
             [
              "name" => "Kiến Xương",
              "province_id" => 8
            ],
             [
              "name" => "Quỳnh Phụ",
              "province_id" => 8
            ],
             [
              "name" => "Thái Bình",
              "province_id" => 8
            ],
             [
              "name" => "Thái Thuỵ",
              "province_id" => 8
            ],
             [
              "name" => "Tiền Hải",
              "province_id" => 8
            ],
             [
              "name" => "Vũ Thư",
              "province_id" => 8
            ],
             [
              "name" => "A Lưới",
              "province_id" => 9
            ],
             [
              "name" => "Huế",
              "province_id" => 9
            ],
             [
              "name" => "Hương Thủy",
              "province_id" => 9
            ],
             [
              "name" => "Hương Trà",
              "province_id" => 9
            ],
             [
              "name" => "Nam Đông",
              "province_id" => 9
            ],
             [
              "name" => "Phong Điền",
              "province_id" => 9
            ],
             [
              "name" => "Phú Lộc",
              "province_id" => 9
            ],
             [
              "name" => "Phú Vang",
              "province_id" => 9
            ],
             [
              "name" => "Quảng Điền",
              "province_id" => 9
            ],
             [
              "name" => "Càng Long",
              "province_id" => 10
            ],
             [
              "name" => "Cầu Kè",
              "province_id" => 10
            ],
             [
              "name" => "Cầu Ngang",
              "province_id" => 10
            ],
             [
              "name" => "Châu Thành",
              "province_id" => 10
            ],
             [
              "name" => "Duyên Hải",
              "province_id" => 10
            ],
             [
              "name" => "Tiểu Cần",
              "province_id" => 10
            ],
             [
              "name" => "Trà Cú",
              "province_id" => 10
            ],
             [
              "name" => "Trà Vinh",
              "province_id" => 10
            ],
             [
              "name" => "Chiêm Hóa",
              "province_id" => 11
            ],
             [
              "name" => "Hàm Yên",
              "province_id" => 11
            ],
             [
              "name" => "Lâm Bình",
              "province_id" => 11
            ],
             [
              "name" => "Na Hang",
              "province_id" => 11
            ],
             [
              "name" => "Sơn Dương",
              "province_id" => 11
            ],
             [
              "name" => "Tuyên Quang",
              "province_id" => 11
            ],
             [
              "name" => "Yên Sơn",
              "province_id" => 11
            ],
             [
              "name" => "Bình Minh",
              "province_id" => 12
            ],
             [
              "name" => "Bình Tân",
              "province_id" => 12
            ],
             [
              "name" => "Long Hồ",
              "province_id" => 12
            ],
             [
              "name" => "Mang Thít",
              "province_id" => 12
            ],
             [
              "name" => "Tam Bình",
              "province_id" => 12
            ],
             [
              "name" => "Trà Ôn",
              "province_id" => 12
            ],
             [
              "name" => "Vĩnh Long",
              "province_id" => 12
            ],
             [
              "name" => "Vũng Liêm",
              "province_id" => 12
            ],
             [
              "name" => "Bình Xuyên",
              "province_id" => 13
            ],
             [
              "name" => "Lập Thạch",
              "province_id" => 13
            ],
             [
              "name" => "Phúc Yên",
              "province_id" => 13
            ],
             [
              "name" => "Sông Lô",
              "province_id" => 13
            ],
             [
              "name" => "Tam Dương",
              "province_id" => 13
            ],
             [
              "name" => "Tam Đảo",
              "province_id" => 13
            ],
             [
              "name" => "Vĩnh Tường",
              "province_id" => 13
            ],
             [
              "name" => "Vĩnh Yên",
              "province_id" => 13
            ],
             [
              "name" => "Yên Lạc",
              "province_id" => 13
            ],
             [
              "name" => "Điện Biên",
              "province_id" => 14
            ],
             [
              "name" => "Điện Biên Đông",
              "province_id" => 14
            ],
             [
              "name" => "Điện Biên Phủ",
              "province_id" => 14
            ],
             [
              "name" => "Mường Ảng",
              "province_id" => 14
            ],
             [
              "name" => "Mường Chà",
              "province_id" => 14
            ],
             [
              "name" => "Mường Lay",
              "province_id" => 14
            ],
             [
              "name" => "Mường Nhé",
              "province_id" => 14
            ],
             [
              "name" => "Nậm Pồ",
              "province_id" => 14
            ],
             [
              "name" => "Tủa Chùa",
              "province_id" => 14
            ],
             [
              "name" => "Tuần Giáo",
              "province_id" => 14
            ],
             [
              "name" => "Buôn Đôn",
              "province_id" => 15
            ],
             [
              "name" => "Buôn Hồ",
              "province_id" => 15
            ],
             [
              "name" => "Buôn Ma Thuột",
              "province_id" => 15
            ],
             [
              "name" => "Cư Kuin",
              "province_id" => 15
            ],
             [
              "name" => "Cư M\"gar",
              "province_id" => 15
            ],
             [
              "name" => "Ea H\"Leo",
              "province_id" => 15
            ],
             [
              "name" => "Ea Kar",
              "province_id" => 15
            ],
             [
              "name" => "Ea Súp",
              "province_id" => 15
            ],
             [
              "name" => "Krông Ana",
              "province_id" => 15
            ],
             [
              "name" => "Krông Bông",
              "province_id" => 15
            ],
             [
              "name" => "Krông Buk",
              "province_id" => 15
            ],
             [
              "name" => "Krông Năng",
              "province_id" => 15
            ],
             [
              "name" => "Krông Pắc",
              "province_id" => 15
            ],
             [
              "name" => "Lăk",
              "province_id" => 15
            ],
             [
              "name" => "M\"Đrăk",
              "province_id" => 15
            ],
             [
              "name" => "Cư Jút",
              "province_id" => 16
            ],
             [
              "name" => "Dăk GLong",
              "province_id" => 16
            ],
             [
              "name" => "Dăk Mil",
              "province_id" => 16
            ],
             [
              "name" => "Dăk R\"Lấp",
              "province_id" => 16
            ],
             [
              "name" => "Dăk Song",
              "province_id" => 16
            ],
             [
              "name" => "Gia Nghĩa",
              "province_id" => 16
            ],
             [
              "name" => "Krông Nô",
              "province_id" => 16
            ],
             [
              "name" => "Tuy Đức",
              "province_id" => 16
            ],
             [
              "name" => "Cao Lãnh",
              "province_id" => 17
            ],
             [
              "name" => "Châu Thành",
              "province_id" => 17
            ],
             [
              "name" => "Hồng Ngự",
              "province_id" => 17
            ],
             [
              "name" => "Huyện Cao Lãnh",
              "province_id" => 17
            ],
             [
              "name" => "Huyện Hồng Ngự",
              "province_id" => 17
            ],
             [
              "name" => "Lai Vung",
              "province_id" => 17
            ],
             [
              "name" => "Lấp Vò",
              "province_id" => 17
            ],
             [
              "name" => "Sa Đéc",
              "province_id" => 17
            ],
             [
              "name" => "Tam Nông",
              "province_id" => 17
            ],
             [
              "name" => "Tân Hồng",
              "province_id" => 17
            ],
             [
              "name" => "Thanh Bình",
              "province_id" => 17
            ],
             [
              "name" => "Tháp Mười",
              "province_id" => 17
            ],
             [
              "name" => "Cẩm Xuyên",
              "province_id" => 18
            ],
             [
              "name" => "Can Lộc",
              "province_id" => 18
            ],
             [
              "name" => "Đức Thọ",
              "province_id" => 18
            ],
             [
              "name" => "Hà Tĩnh",
              "province_id" => 18
            ],
             [
              "name" => "Hồng Lĩnh",
              "province_id" => 18
            ],
             [
              "name" => "Hương Khê",
              "province_id" => 18
            ],
             [
              "name" => "Hương Sơn",
              "province_id" => 18
            ],
             [
              "name" => "Kỳ Anh",
              "province_id" => 18
            ],
             [
              "name" => "Lộc Hà",
              "province_id" => 18
            ],
             [
              "name" => "Nghi Xuân",
              "province_id" => 18
            ],
             [
              "name" => "Thạch Hà",
              "province_id" => 18
            ],
             [
              "name" => "Vũ Quang",
              "province_id" => 18
            ],
             [
              "name" => "An Dương",
              "province_id" => 19
            ],
             [
              "name" => "An Lão",
              "province_id" => 19
            ],
             [
              "name" => "Bạch Long Vĩ",
              "province_id" => 19
            ],
             [
              "name" => "Cát Hải",
              "province_id" => 19
            ],
             [
              "name" => "Dương Kinh",
              "province_id" => 19
            ],
             [
              "name" => "Đồ Sơn",
              "province_id" => 19
            ],
             [
              "name" => "Hải An",
              "province_id" => 19
            ],
             [
              "name" => "Hồng Bàng",
              "province_id" => 19
            ],
             [
              "name" => "Kiến An",
              "province_id" => 19
            ],
             [
              "name" => "Kiến Thụy",
              "province_id" => 19
            ],
             [
              "name" => "Lê Chân",
              "province_id" => 19
            ],
             [
              "name" => "Ngô Quyền",
              "province_id" => 19
            ],
             [
              "name" => "Thủy Nguyên",
              "province_id" => 19
            ],
             [
              "name" => "Tiên Lãng",
              "province_id" => 19
            ],
             [
              "name" => "Vĩnh Bảo",
              "province_id" => 19
            ],
             [
              "name" => "Cam Lâm",
              "province_id" => 20
            ],
             [
              "name" => "Cam Ranh",
              "province_id" => 20
            ],
             [
              "name" => "Diên Khánh",
              "province_id" => 20
            ],
             [
              "name" => "Khánh Sơn",
              "province_id" => 20
            ],
             [
              "name" => "Khánh Vĩnh",
              "province_id" => 20
            ],
             [
              "name" => "Nha Trang",
              "province_id" => 20
            ],
             [
              "name" => "Ninh Hòa",
              "province_id" => 20
            ],
             [
              "name" => "Trường Sa",
              "province_id" => 20
            ],
             [
              "name" => "Vạn Ninh",
              "province_id" => 20
            ],
             [
              "name" => "Bến Lức",
              "province_id" => 21
            ],
             [
              "name" => "Cần Đước",
              "province_id" => 21
            ],
             [
              "name" => "Cần Giuộc",
              "province_id" => 21
            ],
             [
              "name" => "Châu Thành",
              "province_id" => 21
            ],
             [
              "name" => "Đức Hòa",
              "province_id" => 21
            ],
             [
              "name" => "Đức Huệ",
              "province_id" => 21
            ],
             [
              "name" => "Kiến Tường",
              "province_id" => 21
            ],
             [
              "name" => "Mộc Hóa",
              "province_id" => 21
            ],
             [
              "name" => "Tân An",
              "province_id" => 21
            ],
             [
              "name" => "Tân Hưng",
              "province_id" => 21
            ],
             [
              "name" => "Tân Thạnh",
              "province_id" => 21
            ],
             [
              "name" => "Tân Trụ",
              "province_id" => 21
            ],
             [
              "name" => "Thạnh Hóa",
              "province_id" => 21
            ],
             [
              "name" => "Thủ Thừa",
              "province_id" => 21
            ],
             [
              "name" => "Vĩnh Hưng",
              "province_id" => 21
            ],
             [
              "name" => "Bắc Sơn",
              "province_id" => 22
            ],
             [
              "name" => "Bình Gia",
              "province_id" => 22
            ],
             [
              "name" => "Cao Lộc",
              "province_id" => 22
            ],
             [
              "name" => "Chi Lăng",
              "province_id" => 22
            ],
             [
              "name" => "Đình Lập",
              "province_id" => 22
            ],
             [
              "name" => "Hữu Lũng",
              "province_id" => 22
            ],
             [
              "name" => "Lạng Sơn",
              "province_id" => 22
            ],
             [
              "name" => "Lộc Bình",
              "province_id" => 22
            ],
             [
              "name" => "Tràng Định",
              "province_id" => 22
            ],
             [
              "name" => "Văn Lãng",
              "province_id" => 22
            ],
             [
              "name" => "Văn Quan",
              "province_id" => 22
            ],
             [
              "name" => "Bác Ái",
              "province_id" => 23
            ],
             [
              "name" => "Ninh Hải",
              "province_id" => 23
            ],
             [
              "name" => "Ninh Phước",
              "province_id" => 23
            ],
             [
              "name" => "Ninh Sơn",
              "province_id" => 23
            ],
             [
              "name" => "Phan Rang - Tháp Chàm",
              "province_id" => 23
            ],
             [
              "name" => "Thuận Bắc",
              "province_id" => 23
            ],
             [
              "name" => "Thuận Nam",
              "province_id" => 23
            ],
             [
              "name" => "Ba Tơ",
              "province_id" => 24
            ],
             [
              "name" => "Bình Sơn",
              "province_id" => 24
            ],
             [
              "name" => "Đức Phổ",
              "province_id" => 24
            ],
             [
              "name" => "Lý Sơn",
              "province_id" => 24
            ],
             [
              "name" => "Minh Long",
              "province_id" => 24
            ],
             [
              "name" => "Mộ Đức",
              "province_id" => 24
            ],
             [
              "name" => "Nghĩa Hành",
              "province_id" => 24
            ],
             [
              "name" => "Quảng Ngãi",
              "province_id" => 24
            ],
             [
              "name" => "Sơn Hà",
              "province_id" => 24
            ],
             [
              "name" => "Sơn Tây",
              "province_id" => 24
            ],
             [
              "name" => "Sơn Tịnh",
              "province_id" => 24
            ],
             [
              "name" => "Tây Trà",
              "province_id" => 24
            ],
             [
              "name" => "Trà Bồng",
              "province_id" => 24
            ],
             [
              "name" => "Tư Nghĩa",
              "province_id" => 24
            ],
             [
              "name" => "Cái Bè",
              "province_id" => 25
            ],
             [
              "name" => "Cai Lậy",
              "province_id" => 25
            ],
             [
              "name" => "Châu Thành",
              "province_id" => 25
            ],
             [
              "name" => "Chợ Gạo",
              "province_id" => 25
            ],
             [
              "name" => "Gò Công",
              "province_id" => 25
            ],
             [
              "name" => "Gò Công Đông",
              "province_id" => 25
            ],
             [
              "name" => "Gò Công Tây",
              "province_id" => 25
            ],
             [
              "name" => "Huyện Cai Lậy",
              "province_id" => 25
            ],
             [
              "name" => "Mỹ Tho",
              "province_id" => 25
            ],
             [
              "name" => "Tân Phú Đông",
              "province_id" => 25
            ],
             [
              "name" => "Tân Phước",
              "province_id" => 25
            ],
             [
              "name" => "Cẩm Lệ",
              "province_id" => 26
            ],
             [
              "name" => "Hải Châu",
              "province_id" => 26
            ],
             [
              "name" => "Hòa Vang",
              "province_id" => 26
            ],
             [
              "name" => "Hoàng Sa",
              "province_id" => 26
            ],
             [
              "name" => "Liên Chiểu",
              "province_id" => 26
            ],
             [
              "name" => "Ngũ Hành Sơn",
              "province_id" => 26
            ],
             [
              "name" => "Sơn Trà",
              "province_id" => 26
            ],
             [
              "name" => "Thanh Khê",
              "province_id" => 26
            ],
             [
              "name" => "Bàu Bàng",
              "province_id" => 27
            ],
             [
              "name" => "Bến Cát",
              "province_id" => 27
            ],
             [
              "name" => "Dầu Tiếng",
              "province_id" => 27
            ],
             [
              "name" => "Dĩ An",
              "province_id" => 27
            ],
             [
              "name" => "Phú Giáo",
              "province_id" => 27
            ],
             [
              "name" => "Tân Uyên",
              "province_id" => 27
            ],
             [
              "name" => "Thủ Dầu Một",
              "province_id" => 27
            ],
             [
              "name" => "Thuận An",
              "province_id" => 27
            ],
             [
              "name" => "Biên Hòa",
              "province_id" => 28
            ],
             [
              "name" => "Cẩm Mỹ",
              "province_id" => 28
            ],
             [
              "name" => "Định Quán",
              "province_id" => 28
            ],
             [
              "name" => "Long Khánh",
              "province_id" => 28
            ],
             [
              "name" => "Long Thành",
              "province_id" => 28
            ],
             [
              "name" => "Nhơn Trạch",
              "province_id" => 28
            ],
             [
              "name" => "Tân Phú",
              "province_id" => 28
            ],
             [
              "name" => "Thống Nhất",
              "province_id" => 28
            ],
             [
              "name" => "Trảng Bom",
              "province_id" => 28
            ],
             [
              "name" => "Vĩnh Cửu",
              "province_id" => 28
            ],
             [
              "name" => "Xuân Lộc",
              "province_id" => 28
            ],
             [
              "name" => "Bà Rịa",
              "province_id" => 29
            ],
             [
              "name" => "Châu Đức",
              "province_id" => 29
            ],
             [
              "name" => "Côn Đảo",
              "province_id" => 29
            ],
             [
              "name" => "Đất Đỏ",
              "province_id" => 29
            ],
             [
              "name" => "Long Điền",
              "province_id" => 29
            ],
             [
              "name" => "Tân Thành",
              "province_id" => 29
            ],
             [
              "name" => "Vũng Tàu",
              "province_id" => 29
            ],
             [
              "name" => "Xuyên Mộc",
              "province_id" => 29
            ],
             [
              "name" => " Thới Lai",
              "province_id" => 30
            ],
             [
              "name" => "Bình Thủy",
              "province_id" => 30
            ],
             [
              "name" => "Cái Răng",
              "province_id" => 30
            ],
             [
              "name" => "Cờ Đỏ",
              "province_id" => 30
            ],
             [
              "name" => "Ninh Kiều",
              "province_id" => 30
            ],
             [
              "name" => "Ô Môn",
              "province_id" => 30
            ],
             [
              "name" => "Phong Điền",
              "province_id" => 30
            ],
             [
              "name" => "Thốt Nốt",
              "province_id" => 30
            ],
             [
              "name" => "Vĩnh Thạnh",
              "province_id" => 30
            ],
             [
              "name" => "Bắc Bình",
              "province_id" => 31
            ],
             [
              "name" => "Đảo Phú Quý",
              "province_id" => 31
            ],
             [
              "name" => "Đức Linh",
              "province_id" => 31
            ],
             [
              "name" => "Hàm Tân",
              "province_id" => 31
            ],
             [
              "name" => "Hàm Thuận Bắc",
              "province_id" => 31
            ],
             [
              "name" => "Hàm Thuận Nam",
              "province_id" => 31
            ],
             [
              "name" => "La Gi",
              "province_id" => 31
            ],
             [
              "name" => "Phan Thiết",
              "province_id" => 31
            ],
             [
              "name" => "Tánh Linh",
              "province_id" => 31
            ],
             [
              "name" => "Tuy Phong",
              "province_id" => 31
            ],
             [
              "name" => "Bảo Lâm",
              "province_id" => 32
            ],
             [
              "name" => "Bảo Lộc",
              "province_id" => 32
            ],
             [
              "name" => "Cát Tiên",
              "province_id" => 32
            ],
             [
              "name" => "Di Linh",
              "province_id" => 32
            ],
             [
              "name" => "Đạ Huoai",
              "province_id" => 32
            ],
             [
              "name" => "Đà Lạt",
              "province_id" => 32
            ],
             [
              "name" => "Đạ Tẻh",
              "province_id" => 32
            ],
             [
              "name" => "Đam Rông",
              "province_id" => 32
            ],
             [
              "name" => "Đơn Dương",
              "province_id" => 32
            ],
             [
              "name" => "Đức Trọng",
              "province_id" => 32
            ],
             [
              "name" => "Lạc Dương",
              "province_id" => 32
            ],
             [
              "name" => "Lâm Hà",
              "province_id" => 32
            ],
             [
              "name" => "An Biên",
              "province_id" => 33
            ],
             [
              "name" => "An Minh",
              "province_id" => 33
            ],
             [
              "name" => "Châu Thành",
              "province_id" => 33
            ],
             [
              "name" => "Giang Thành",
              "province_id" => 33
            ],
             [
              "name" => "Giồng Riềng",
              "province_id" => 33
            ],
             [
              "name" => "Gò Quao",
              "province_id" => 33
            ],
             [
              "name" => "Hà Tiên",
              "province_id" => 33
            ],
             [
              "name" => "Hòn Đất",
              "province_id" => 33
            ],
             [
              "name" => "Kiên Hải",
              "province_id" => 33
            ],
             [
              "name" => "Kiên Lương",
              "province_id" => 33
            ],
             [
              "name" => "Phú Quốc",
              "province_id" => 33
            ],
             [
              "name" => "Rạch Giá",
              "province_id" => 33
            ],
             [
              "name" => "Tân Hiệp",
              "province_id" => 33
            ],
             [
              "name" => "U minh Thượng",
              "province_id" => 33
            ],
             [
              "name" => "Vĩnh Thuận",
              "province_id" => 33
            ],
             [
              "name" => "Bắc Ninh",
              "province_id" => 34
            ],
             [
              "name" => "Gia Bình",
              "province_id" => 34
            ],
             [
              "name" => "Lương Tài",
              "province_id" => 34
            ],
             [
              "name" => "Quế Võ",
              "province_id" => 34
            ],
             [
              "name" => "Thuận Thành",
              "province_id" => 34
            ],
             [
              "name" => "Tiên Du",
              "province_id" => 34
            ],
             [
              "name" => "Từ Sơn",
              "province_id" => 34
            ],
             [
              "name" => "Yên Phong",
              "province_id" => 34
            ],
             [
              "name" => "Bá Thước",
              "province_id" => 35
            ],
             [
              "name" => "Bỉm Sơn",
              "province_id" => 35
            ],
             [
              "name" => "Cẩm Thủy",
              "province_id" => 35
            ],
             [
              "name" => "Đông Sơn",
              "province_id" => 35
            ],
             [
              "name" => "Hà Trung",
              "province_id" => 35
            ],
             [
              "name" => "Hậu Lộc",
              "province_id" => 35
            ],
             [
              "name" => "Hoằng Hóa",
              "province_id" => 35
            ],
             [
              "name" => "Lang Chánh",
              "province_id" => 35
            ],
             [
              "name" => "Mường Lát",
              "province_id" => 35
            ],
             [
              "name" => "Nga Sơn",
              "province_id" => 35
            ],
             [
              "name" => "Ngọc Lặc",
              "province_id" => 35
            ],
             [
              "name" => "Như Thanh",
              "province_id" => 35
            ],
             [
              "name" => "Như Xuân",
              "province_id" => 35
            ],
             [
              "name" => "Nông Cống",
              "province_id" => 35
            ],
             [
              "name" => "Quan Hóa",
              "province_id" => 35
            ],
             [
              "name" => "Quan Sơn",
              "province_id" => 35
            ],
             [
              "name" => "Quảng Xương",
              "province_id" => 35
            ],
             [
              "name" => "Sầm Sơn",
              "province_id" => 35
            ],
             [
              "name" => "Thạch Thành",
              "province_id" => 35
            ],
             [
              "name" => "Thanh Hóa",
              "province_id" => 35
            ],
             [
              "name" => "Thiệu Hóa",
              "province_id" => 35
            ],
             [
              "name" => "Thọ Xuân",
              "province_id" => 35
            ],
             [
              "name" => "Thường Xuân",
              "province_id" => 35
            ],
             [
              "name" => "Tĩnh Gia",
              "province_id" => 35
            ],
             [
              "name" => "Triệu Sơn",
              "province_id" => 35
            ],
             [
              "name" => "Vĩnh Lộc",
              "province_id" => 35
            ],
             [
              "name" => "Yên Định",
              "province_id" => 35
            ],
             [
              "name" => "Anh Sơn",
              "province_id" => 36
            ],
             [
              "name" => "Con Cuông",
              "province_id" => 36
            ],
             [
              "name" => "Cửa Lò",
              "province_id" => 36
            ],
             [
              "name" => "Diễn Châu",
              "province_id" => 36
            ],
             [
              "name" => "Đô Lương",
              "province_id" => 36
            ],
             [
              "name" => "Hoàng Mai",
              "province_id" => 36
            ],
             [
              "name" => "Hưng Nguyên",
              "province_id" => 36
            ],
             [
              "name" => "Kỳ Sơn",
              "province_id" => 36
            ],
             [
              "name" => "Nam Đàn",
              "province_id" => 36
            ],
             [
              "name" => "Nghi Lộc",
              "province_id" => 36
            ],
             [
              "name" => "Nghĩa Đàn",
              "province_id" => 36
            ],
             [
              "name" => "Quế Phong",
              "province_id" => 36
            ],
             [
              "name" => "Quỳ Châu",
              "province_id" => 36
            ],
             [
              "name" => "Quỳ Hợp",
              "province_id" => 36
            ],
             [
              "name" => "Quỳnh Lưu",
              "province_id" => 36
            ],
             [
              "name" => "Tân Kỳ",
              "province_id" => 36
            ],
             [
              "name" => "Thái Hòa",
              "province_id" => 36
            ],
             [
              "name" => "Thanh Chương",
              "province_id" => 36
            ],
             [
              "name" => "Tương Dương",
              "province_id" => 36
            ],
             [
              "name" => "Vinh",
              "province_id" => 36
            ],
             [
              "name" => "Yên Thành",
              "province_id" => 36
            ],
             [
              "name" => "Nam Sách",
              "province_id" => 37
            ],
             [
              "name" => "Ninh Giang",
              "province_id" => 37
            ],
             [
              "name" => "Thanh Hà",
              "province_id" => 37
            ],
             [
              "name" => "Thanh Miện",
              "province_id" => 37
            ],
             [
              "name" => "Tứ Kỳ",
              "province_id" => 37
            ],
             [
              "name" => "Bình Giang",
              "province_id" => 37
            ],
             [
              "name" => "Cẩm Giàng",
              "province_id" => 37
            ],
             [
              "name" => "Chí Linh",
              "province_id" => 37
            ],
             [
              "name" => "Gia Lộc",
              "province_id" => 37
            ],
             [
              "name" => "Hải Dương",
              "province_id" => 37
            ],
             [
              "name" => "Kim Thành",
              "province_id" => 37
            ],
             [
              "name" => "Kinh Môn",
              "province_id" => 37
            ],
             [
              "name" => "An Khê",
              "province_id" => 38
            ],
             [
              "name" => "AYun Pa",
              "province_id" => 38
            ],
             [
              "name" => "Chư Păh",
              "province_id" => 38
            ],
             [
              "name" => "Chư Pưh",
              "province_id" => 38
            ],
             [
              "name" => "Chư Sê",
              "province_id" => 38
            ],
             [
              "name" => "ChưPRông",
              "province_id" => 38
            ],
             [
              "name" => "Đăk Đoa",
              "province_id" => 38
            ],
             [
              "name" => "Đăk Pơ",
              "province_id" => 38
            ],
             [
              "name" => "Đức Cơ",
              "province_id" => 38
            ],
             [
              "name" => "Ia Grai",
              "province_id" => 38
            ],
             [
              "name" => "Ia Pa",
              "province_id" => 38
            ],
             [
              "name" => "KBang",
              "province_id" => 38
            ],
             [
              "name" => "Kông Chro",
              "province_id" => 38
            ],
             [
              "name" => "Krông Pa",
              "province_id" => 38
            ],
             [
              "name" => "Mang Yang",
              "province_id" => 38
            ],
             [
              "name" => "Phú Thiện",
              "province_id" => 38
            ],
             [
              "name" => "Plei Ku",
              "province_id" => 38
            ],
             [
              "name" => "Bình Long",
              "province_id" => 39
            ],
             [
              "name" => "Bù Đăng",
              "province_id" => 39
            ],
             [
              "name" => "Bù Đốp",
              "province_id" => 39
            ],
             [
              "name" => "Bù Gia Mập",
              "province_id" => 39
            ],
             [
              "name" => "Chơn Thành",
              "province_id" => 39
            ],
             [
              "name" => "Đồng Phú",
              "province_id" => 39
            ],
             [
              "name" => "Đồng Xoài",
              "province_id" => 39
            ],
             [
              "name" => "Hớn Quản",
              "province_id" => 39
            ],
             [
              "name" => "Lộc Ninh",
              "province_id" => 39
            ],
             [
              "name" => "Phú Riềng",
              "province_id" => 39
            ],
             [
              "name" => "Phước Long",
              "province_id" => 39
            ],
             [
              "name" => "Ân Thi",
              "province_id" => 40
            ],
             [
              "name" => "Hưng Yên",
              "province_id" => 40
            ],
             [
              "name" => "Khoái Châu",
              "province_id" => 40
            ],
             [
              "name" => "Kim Động",
              "province_id" => 40
            ],
             [
              "name" => "Mỹ Hào",
              "province_id" => 40
            ],
             [
              "name" => "Phù Cừ",
              "province_id" => 40
            ],
             [
              "name" => "Tiên Lữ",
              "province_id" => 40
            ],
             [
              "name" => "Văn Giang",
              "province_id" => 40
            ],
             [
              "name" => "Văn Lâm",
              "province_id" => 40
            ],
             [
              "name" => "Yên Mỹ",
              "province_id" => 40
            ],
             [
              "name" => "An Lão",
              "province_id" => 41
            ],
             [
              "name" => "An Nhơn",
              "province_id" => 41
            ],
             [
              "name" => "Hoài Ân",
              "province_id" => 41
            ],
             [
              "name" => "Hoài Nhơn",
              "province_id" => 41
            ],
             [
              "name" => "Phù Cát",
              "province_id" => 41
            ],
             [
              "name" => "Phù Mỹ",
              "province_id" => 41
            ],
             [
              "name" => "Quy Nhơn",
              "province_id" => 41
            ],
             [
              "name" => "Tây Sơn",
              "province_id" => 41
            ],
             [
              "name" => "Tuy Phước",
              "province_id" => 41
            ],
             [
              "name" => "Vân Canh",
              "province_id" => 41
            ],
             [
              "name" => "Vĩnh Thạnh",
              "province_id" => 41
            ],
             [
              "name" => "Bắc Giang",
              "province_id" => 42
            ],
             [
              "name" => "Hiệp Hòa",
              "province_id" => 42
            ],
             [
              "name" => "Lạng Giang",
              "province_id" => 42
            ],
             [
              "name" => "Lục Nam",
              "province_id" => 42
            ],
             [
              "name" => "Lục Ngạn",
              "province_id" => 42
            ],
             [
              "name" => "Sơn Động",
              "province_id" => 42
            ],
             [
              "name" => "Tân Yên",
              "province_id" => 42
            ],
             [
              "name" => "Việt Yên",
              "province_id" => 42
            ],
             [
              "name" => "Yên Dũng",
              "province_id" => 42
            ],
             [
              "name" => "Yên Thế",
              "province_id" => 42
            ],
             [
              "name" => "Cao Phong",
              "province_id" => 43
            ],
             [
              "name" => "Đà Bắc",
              "province_id" => 43
            ],
             [
              "name" => "Hòa Bình",
              "province_id" => 43
            ],
             [
              "name" => "Kim Bôi",
              "province_id" => 43
            ],
             [
              "name" => "Kỳ Sơn",
              "province_id" => 43
            ],
             [
              "name" => "Lạc Sơn",
              "province_id" => 43
            ],
             [
              "name" => "Lạc Thủy",
              "province_id" => 43
            ],
             [
              "name" => "Lương Sơn",
              "province_id" => 43
            ],
             [
              "name" => "Mai Châu",
              "province_id" => 43
            ],
             [
              "name" => "Tân Lạc",
              "province_id" => 43
            ],
             [
              "name" => "Yên Thủy",
              "province_id" => 43
            ],
             [
              "name" => "An Phú",
              "province_id" => 44
            ],
             [
              "name" => "Châu Đốc",
              "province_id" => 44
            ],
             [
              "name" => "Châu Phú",
              "province_id" => 44
            ],
             [
              "name" => "Châu Thành",
              "province_id" => 44
            ],
             [
              "name" => "Chợ Mới",
              "province_id" => 44
            ],
             [
              "name" => "Long Xuyên",
              "province_id" => 44
            ],
             [
              "name" => "Phú Tân",
              "province_id" => 44
            ],
             [
              "name" => "Tân Châu",
              "province_id" => 44
            ],
             [
              "name" => "Thoại Sơn",
              "province_id" => 44
            ],
             [
              "name" => "Tịnh Biên",
              "province_id" => 44
            ],
             [
              "name" => "Tri Tôn",
              "province_id" => 44
            ],
             [
              "name" => "Bến Cầu",
              "province_id" => 45
            ],
             [
              "name" => "Châu Thành",
              "province_id" => 45
            ],
             [
              "name" => "Dương Minh Châu",
              "province_id" => 45
            ],
             [
              "name" => "Gò Dầu",
              "province_id" => 45
            ],
             [
              "name" => "Hòa Thành",
              "province_id" => 45
            ],
             [
              "name" => "Tân Biên",
              "province_id" => 45
            ],
             [
              "name" => "Tân Châu",
              "province_id" => 45
            ],
             [
              "name" => "Tây Ninh",
              "province_id" => 45
            ],
             [
              "name" => "Trảng Bàng",
              "province_id" => 45
            ],
             [
              "name" => "Đại Từ",
              "province_id" => 46
            ],
             [
              "name" => "Định Hóa",
              "province_id" => 46
            ],
             [
              "name" => "Đồng Hỷ",
              "province_id" => 46
            ],
             [
              "name" => "Phổ Yên",
              "province_id" => 46
            ],
             [
              "name" => "Phú Bình",
              "province_id" => 46
            ],
             [
              "name" => "Phú Lương",
              "province_id" => 46
            ],
             [
              "name" => "Sông Công",
              "province_id" => 46
            ],
             [
              "name" => "Thái Nguyên",
              "province_id" => 46
            ],
             [
              "name" => "Võ Nhai",
              "province_id" => 46
            ],
             [
              "name" => "Bắc Hà",
              "province_id" => 47
            ],
             [
              "name" => "Bảo Thắng",
              "province_id" => 47
            ],
             [
              "name" => "Bảo Yên",
              "province_id" => 47
            ],
             [
              "name" => "Bát Xát",
              "province_id" => 47
            ],
             [
              "name" => "Lào Cai",
              "province_id" => 47
            ],
             [
              "name" => "Mường Khương",
              "province_id" => 47
            ],
             [
              "name" => "Sa Pa",
              "province_id" => 47
            ],
             [
              "name" => "Văn Bàn",
              "province_id" => 47
            ],
             [
              "name" => "Xi Ma Cai",
              "province_id" => 47
            ],
             [
              "name" => "Giao Thủy",
              "province_id" => 48
            ],
             [
              "name" => "Hải Hậu",
              "province_id" => 48
            ],
             [
              "name" => "Mỹ Lộc",
              "province_id" => 48
            ],
             [
              "name" => "Nam Định",
              "province_id" => 48
            ],
             [
              "name" => "Nam Trực",
              "province_id" => 48
            ],
             [
              "name" => "Nghĩa Hưng",
              "province_id" => 48
            ],
             [
              "name" => "Trực Ninh",
              "province_id" => 48
            ],
             [
              "name" => "Vụ Bản",
              "province_id" => 48
            ],
             [
              "name" => "Xuân Trường",
              "province_id" => 48
            ],
             [
              "name" => "Ý Yên",
              "province_id" => 48
            ],
             [
              "name" => "Ba Tri",
              "province_id" => 49
            ],
             [
              "name" => "Bến Tre",
              "province_id" => 49
            ],
             [
              "name" => "Bình Đại",
              "province_id" => 49
            ],
             [
              "name" => "Châu Thành",
              "province_id" => 49
            ],
             [
              "name" => "Chợ Lách",
              "province_id" => 49
            ],
             [
              "name" => "Giồng Trôm",
              "province_id" => 49
            ],
             [
              "name" => "Mỏ Cày Bắc",
              "province_id" => 49
            ],
             [
              "name" => "Mỏ Cày Nam",
              "province_id" => 49
            ],
             [
              "name" => "Thạnh Phú",
              "province_id" => 49
            ],
             [
              "name" => "Cà Mau",
              "province_id" => 50
            ],
             [
              "name" => "Cái Nước",
              "province_id" => 50
            ],
             [
              "name" => "Đầm Dơi",
              "province_id" => 50
            ],
             [
              "name" => "Năm Căn",
              "province_id" => 50
            ],
             [
              "name" => "Ngọc Hiển",
              "province_id" => 50
            ],
             [
              "name" => "Phú Tân",
              "province_id" => 50
            ],
             [
              "name" => "Thới Bình",
              "province_id" => 50
            ],
             [
              "name" => "Trần Văn Thời",
              "province_id" => 50
            ],
             [
              "name" => "U Minh",
              "province_id" => 50
            ],
             [
              "name" => "Gia Viễn",
              "province_id" => 51
            ],
             [
              "name" => "Hoa Lư",
              "province_id" => 51
            ],
             [
              "name" => "Kim Sơn",
              "province_id" => 51
            ],
             [
              "name" => "Nho Quan",
              "province_id" => 51
            ],
             [
              "name" => "Ninh Bình",
              "province_id" => 51
            ],
             [
              "name" => "Tam Điệp",
              "province_id" => 51
            ],
             [
              "name" => "Yên Khánh",
              "province_id" => 51
            ],
             [
              "name" => "Yên Mô",
              "province_id" => 51
            ],
             [
              "name" => "Cẩm Khê",
              "province_id" => 52
            ],
             [
              "name" => "Đoan Hùng",
              "province_id" => 52
            ],
             [
              "name" => "Hạ Hòa",
              "province_id" => 52
            ],
             [
              "name" => "Lâm Thao",
              "province_id" => 52
            ],
             [
              "name" => "Phù Ninh",
              "province_id" => 52
            ],
             [
              "name" => "Phú Thọ",
              "province_id" => 52
            ],
             [
              "name" => "Tam Nông",
              "province_id" => 52
            ],
             [
              "name" => "Tân Sơn",
              "province_id" => 52
            ],
             [
              "name" => "Thanh Ba",
              "province_id" => 52
            ],
             [
              "name" => "Thanh Sơn",
              "province_id" => 52
            ],
             [
              "name" => "Thanh Thủy",
              "province_id" => 52
            ],
             [
              "name" => "Việt Trì",
              "province_id" => 52
            ],
             [
              "name" => "Yên Lập",
              "province_id" => 52
            ],
             [
              "name" => "Đông Hòa",
              "province_id" => 53
            ],
             [
              "name" => "Đồng Xuân",
              "province_id" => 53
            ],
             [
              "name" => "Phú Hòa",
              "province_id" => 53
            ],
             [
              "name" => "Sơn Hòa",
              "province_id" => 53
            ],
             [
              "name" => "Sông Cầu",
              "province_id" => 53
            ],
             [
              "name" => "Sông Hinh",
              "province_id" => 53
            ],
             [
              "name" => "Tây Hòa",
              "province_id" => 53
            ],
             [
              "name" => "Tuy An",
              "province_id" => 53
            ],
             [
              "name" => "Tuy Hòa",
              "province_id" => 53
            ],
             [
              "name" => "Bình Lục",
              "province_id" => 54
            ],
             [
              "name" => "Duy Tiên",
              "province_id" => 54
            ],
             [
              "name" => "Kim Bảng",
              "province_id" => 54
            ],
             [
              "name" => "Lý Nhân",
              "province_id" => 54
            ],
             [
              "name" => "Phủ Lý",
              "province_id" => 54
            ],
             [
              "name" => "Thanh Liêm",
              "province_id" => 54
            ],
             [
              "name" => "Đăk Glei",
              "province_id" => 55
            ],
             [
              "name" => "Đăk Hà",
              "province_id" => 55
            ],
             [
              "name" => "Kon Plông",
              "province_id" => 55
            ],
             [
              "name" => "Kon Rẫy",
              "province_id" => 55
            ],
             [
              "name" => "KonTum",
              "province_id" => 55
            ],
             [
              "name" => "Ngọc Hồi",
              "province_id" => 55
            ],
             [
              "name" => "Sa Thầy",
              "province_id" => 55
            ],
             [
              "name" => "Tu Mơ Rông",
              "province_id" => 55
            ],
             [
              "name" => "Đăk Tô",
              "province_id" => 55
            ],
             [
              "name" => "Ia H\"Drai",
              "province_id" => 55
            ],
             [
              "name" => "Cam Lộ",
              "province_id" => 56
            ],
             [
              "name" => "Đa Krông",
              "province_id" => 56
            ],
             [
              "name" => "Đảo Cồn cỏ",
              "province_id" => 56
            ],
             [
              "name" => "Đông Hà",
              "province_id" => 56
            ],
             [
              "name" => "Gio Linh",
              "province_id" => 56
            ],
             [
              "name" => "Hải Lăng",
              "province_id" => 56
            ],
             [
              "name" => "Hướng Hóa",
              "province_id" => 56
            ],
             [
              "name" => "Quảng Trị",
              "province_id" => 56
            ],
             [
              "name" => "Triệu Phong",
              "province_id" => 56
            ],
             [
              "name" => "Vĩnh Linh",
              "province_id" => 56
            ],
             [
              "name" => "Châu Thành",
              "province_id" => 57
            ],
             [
              "name" => "Châu Thành A",
              "province_id" => 57
            ],
             [
              "name" => "Long Mỹ",
              "province_id" => 57
            ],
             [
              "name" => "Ngã Bảy",
              "province_id" => 57
            ],
             [
              "name" => "Phụng Hiệp",
              "province_id" => 57
            ],
             [
              "name" => "Vị Thanh",
              "province_id" => 57
            ],
             [
              "name" => "Vị Thủy",
              "province_id" => 57
            ],
             [
              "name" => "Bạc Liêu",
              "province_id" => 58
            ],
             [
              "name" => "Đông Hải",
              "province_id" => 58
            ],
             [
              "name" => "Giá Rai",
              "province_id" => 58
            ],
             [
              "name" => "Hòa Bình",
              "province_id" => 58
            ],
             [
              "name" => "Hồng Dân",
              "province_id" => 58
            ],
             [
              "name" => "Phước Long",
              "province_id" => 58
            ],
             [
              "name" => "Vĩnh Lợi",
              "province_id" => 58
            ],
             [
              "name" => "Lục Yên",
              "province_id" => 59
            ],
             [
              "name" => "Mù Cang Chải",
              "province_id" => 59
            ],
             [
              "name" => "Nghĩa Lộ",
              "province_id" => 59
            ],
             [
              "name" => "Trạm Tấu",
              "province_id" => 59
            ],
             [
              "name" => "Trấn Yên",
              "province_id" => 59
            ],
             [
              "name" => "Văn Chấn",
              "province_id" => 59
            ],
             [
              "name" => "Văn Yên",
              "province_id" => 59
            ],
             [
              "name" => "Yên Bái",
              "province_id" => 59
            ],
             [
              "name" => "Yên Bình",
              "province_id" => 59
            ],
             [
              "name" => "Lai Châu",
              "province_id" => 60
            ],
             [
              "name" => "Mường Tè",
              "province_id" => 60
            ],
             [
              "name" => "Nậm Nhùn",
              "province_id" => 60
            ],
             [
              "name" => "Phong Thổ",
              "province_id" => 60
            ],
             [
              "name" => "Sìn Hồ",
              "province_id" => 60
            ],
             [
              "name" => "Tam Đường",
              "province_id" => 60
            ],
             [
              "name" => "Tân Uyên",
              "province_id" => 60
            ],
             [
              "name" => "Than Uyên",
              "province_id" => 60
            ],
             [
              "name" => "Bắc Mê",
              "province_id" => 61
            ],
             [
              "name" => "Bắc Quang",
              "province_id" => 61
            ],
             [
              "name" => "Đồng Văn",
              "province_id" => 61
            ],
             [
              "name" => "Hà Giang",
              "province_id" => 61
            ],
             [
              "name" => "Hoàng Su Phì",
              "province_id" => 61
            ],
             [
              "name" => "Mèo Vạc",
              "province_id" => 61
            ],
             [
              "name" => "Quản Bạ",
              "province_id" => 61
            ],
             [
              "name" => "Quang Bình",
              "province_id" => 61
            ],
             [
              "name" => "Vị Xuyên",
              "province_id" => 61
            ],
             [
              "name" => "Xín Mần",
              "province_id" => 61
            ],
             [
              "name" => "Yên Minh",
              "province_id" => 61
            ],
             [
              "name" => "Ba Bể",
              "province_id" => 62
            ],
             [
              "name" => "Bắc Kạn",
              "province_id" => 62
            ],
             [
              "name" => "Bạch Thông",
              "province_id" => 62
            ],
             [
              "name" => "Chợ Đồn",
              "province_id" => 62
            ],
             [
              "name" => "Chợ Mới",
              "province_id" => 62
            ],
             [
              "name" => "Na Rì",
              "province_id" => 62
            ],
             [
              "name" => "Ngân Sơn",
              "province_id" => 62
            ],
             [
              "name" => "Pác Nặm",
              "province_id" => 62
            ],
             [
              "name" => "Bảo Lạc",
              "province_id" => 63
            ],
             [
              "name" => "Bảo Lâm",
              "province_id" => 63
            ],
             [
              "name" => "Cao Bằng",
              "province_id" => 63
            ],
             [
              "name" => "Hạ Lang",
              "province_id" => 63
            ],
             [
              "name" => "Hà Quảng",
              "province_id" => 63
            ],
             [
              "name" => "Hòa An",
              "province_id" => 63
            ],
             [
              "name" => "Nguyên Bình",
              "province_id" => 63
            ],
             [
              "name" => "Phục Hòa",
              "province_id" => 63
            ],
             [
              "name" => "Quảng Uyên",
              "province_id" => 63
            ],
             [
              "name" => "Thạch An",
              "province_id" => 63
            ],
             [
              "name" => "Thông Nông",
              "province_id" => 63
            ],
             [
              "name" => "Trà Lĩnh",
              "province_id" => 63
            ],
             [
              "name" => "Trùng Khánh",
              "province_id" => 63
            ]
        ]);
    }
}
