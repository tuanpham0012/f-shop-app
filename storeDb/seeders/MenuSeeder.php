<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class MenuSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        $menus = [
            [
                // 'id' => 1,
                'title' => 'Admin Root',
                'icon' => '',
                'url' => null,
                '_lft' => 1,
                '_rgt' => 2,
                'parent_id' => null
            ],
            [
                // 'id' => 2,
                'title' => 'User Root',
                'icon' => '',
                'url' => null,
                '_lft' => 0,
                '_rgt' => 11,
                'parent_id' => null
            ],
            [
                // 'id' => 3,
                'title' => 'Dashboard',
                'url' => null,
                'icon' => '<i class="menu-icon tf-icons bx bx-home-circle"></i>',
                '_lft' => 1,
                '_rgt' => 10,
                'parent_id' => 1
            ],
            [
                // 'id' => 4,
                'title' => 'Quản lý',
                'url' => null,
                'icon' => '<i class="fa-solid fa-gears"></i>',
                '_lft' => 2,
                '_rgt' => 9,
                'parent_id' => 1
            ],
            [
                // 'id' => 5,
                'title' => 'Khách hàng',
                'url' => '/customers',
                'icon' => '',
                '_lft' => 3,
                '_rgt' => 8,
                'parent_id' => 4
            ],
            [
                // 'id' => 6,
                'title' => 'Sản phẩm',
                'url' => '/products',
                'icon' => '',
                '_lft' => 4,
                '_rgt' => 7,
                'parent_id' => 1
            ],
            [
                // 'id' => 6,
                'title' => 'Danh sách sản phẩm',
                'url' => '/products',
                'icon' => '',
                '_lft' => 4,
                '_rgt' => 7,
                'parent_id' => 6
            ],
            [
                // 'id' => 7,
                'title' => 'Thể loại',
                'url' => '/categories',
                'icon' => '',
                '_lft' => 4,
                '_rgt' => 7,
                'parent_id' => 6
            ],
            [
                // 'id' => 8,
                'title' => 'Thương hiệu',
                'url' => '/brands',
                'icon' => '',
                '_lft' => 4,
                '_rgt' => 7,
                'parent_id' => 6
            ],
            [
                // 'id' => 9,
                'title' => 'Menu',
                'url' => '/menus',
                'icon' => '',
                '_lft' => 5,
                '_rgt' => 6,
                'parent_id' => 4
            ],
        ];
        \DB::table("menus")->insert($menus);
    }
}
