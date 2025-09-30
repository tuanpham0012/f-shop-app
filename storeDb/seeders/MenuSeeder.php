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

        $menuAdmin = [
            [
                'name' => 'Dashboard',
                'icon' => '<i class="fa-solid fa-house"></i>',
                'path' => '/admin/dashboard',
                'type' => 0,
                'children' => []
            ],
            [
                'name' => 'Bán hàng',
                'icon' => '<i class="fa-brands fa-themeisle"></i>',
                'path' => null,
                'type' => 0,
                'children' => [
                    [
                        'name' => 'Đơn hàng',
                        'icon' => '<i class="fa-brands fa-jedi-order"></i>',
                        'path' => '/orders',
                        'type' => 0,
                        'children' => []
                    ]
                ]
            ],
            [
                'name' => 'Quản lý Người dùng',
                'icon' => '<i class="fa-solid fa-users"></i>',
                'path' => null,
                'type' => 0,
                'children' => [
                    [
                        'name' => 'Người dùng',
                        'icon' => '<i class="fa-solid fa-user"></i>',
                        'path' => '/users',
                        'type' => 0,
                        'children' => [
                            [
                                'name' => 'Người dùng',
                                'icon' => '<i class="fa-solid fa-user"></i>',
                                'path' => '/users',
                                'type' => 0,
                                'children' => []
                            ],
                            [
                                'name' => 'Khách hàng',
                                'icon' => '<i class="fa-solid fa-person-military-pointing"></i>',
                                'path' => '/customers',
                                'type' => 0,
                                'children' => []
                            ]
                        ]
                    ],
                    [
                        'name' => 'Khách hàng',
                        'icon' => '<i class="fa-solid fa-person-military-pointing"></i>',
                        'path' => '/customers',
                        'type' => 0,
                        'children' => []
                    ]
                ]
            ],
            [
                'name' => 'Cài đặt Hệ thống',
                'icon' => '<i class="fa-solid fa-sliders"></i>',
                'path' => '/admin/settings',
                'type' => 0,
                'children' => [
                    [
                        'name' => 'Danh mục',
                        'icon' => '<i class="fa-solid fa-table-list"></i>',
                        'path' => '/categories',
                        'type' => 0,
                        'children' => []
                    ],
                    [
                        'name' => 'Thương hiệu',
                        'icon' => '<i class="fa-solid fa-copyright"></i>',
                        'path' => '/brands',
                        'type' => 0,
                        'children' => []
                    ],
                    [
                        'name' => 'Sản phẩm',
                        'icon' => '<i class="fa-brands fa-product-hunt"></i>',
                        'path' => '/products',
                        'type' => 0,
                        'children' => []
                    ],
                    [
                        'name' => 'Thuế sản phẩm',
                        'icon' => '<i class="fa-solid fa-person-chalkboard"></i>',
                        'path' => '/taxes',
                        'type' => 0,
                        'children' => []
                    ],
                ]
            ]

        ];
        $menuUser = [
            [
                'name' => 'Dashboard',
                'icon' => 'fas fa-tachometer-alt',
                'path' => '/admin/dashboard',
                'type' => 1,
                'children' => []
            ],
            [
                'name' => 'Bán hàng',
                'icon' => 'fas fa-users',
                'type' => 1,
                'path' => null,
                'children' => [
                    [
                        'name' => 'Đơn hàng',
                        'icon' => '',
                        'path' => '/admin/orders',
                        'type' => 1,
                        'children' => []
                    ]
                ]
            ],
            [
                'name' => 'Quản lý Người dùng',
                'icon' => 'fas fa-users',
                'type' => 1,
                'path' => null,
                'children' => [
                    [
                        'name' => 'Người dùng',
                        'icon' => '',
                        'path' => '/admin/users',
                        'type' => 1,
                        'children' => []
                    ],
                    [
                        'name' => 'Khách hàng',
                        'icon' => '',
                        'path' => '/admin/users/create',
                        'type' => 1,
                        'children' => []
                    ]
                ]
            ],
            [
                'name' => 'Cài đặt Hệ thống',
                'icon' => 'fas fa-cogs',
                'path' => '/admin/settings',
                'type' => 1,
                'children' => [
                    [
                        'name' => 'Danh mục',
                        'icon' => '',
                        'path' => '/admin/categories',
                        'type' => 1,
                        'children' => []
                    ],
                    [
                        'name' => 'Thương hiệu',
                        'icon' => '',
                        'path' => '/admin/users/create',
                        'type' => 1,
                        'children' => []
                    ],
                    [
                        'name' => 'Sản phẩm',
                        'icon' => '',
                        'path' => '/admin/users/create',
                        'type' => 1,
                        'children' => []
                    ],
                    [
                        'name' => 'Thuế sản phẩm',
                        'icon' => '',
                        'path' => '/admin/users/create',
                        'type' => 1,
                        'children' => []
                    ],
                ]
            ]
        ];
        $this->insertMenu(null, $menuAdmin);
        $this->insertMenu(null, $menuUser);
    }

    public function insertMenu($parentId = null, $menus)
    {
        foreach ($menus as $key => $menu) {
            $children = $menu['children'];
            unset($menu['children']);
            $menu['position'] = $key + 1;
            $menu['parent_id'] = $parentId;
            $menuId = \DB::table('menus')->insertGetId($menu);
            if (!empty($children)) {
                $this->insertMenu($menuId, $children);
            }
        }
    }
}
