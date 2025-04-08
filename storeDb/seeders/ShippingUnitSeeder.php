<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class ShippingUnitSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        $shipping = [
            [
                'name' => 'Mặc định',
                'token' => 'Tiền mặt',
                'url' => 'Tiền12 123 mặt',
                'description' => 'Tiền mặt',
                'icon' => 'Tiền mặt',
            ],
            [
                'name' => 'Giao hàng nhanh',
                'token' => 'Tiền mặt',
                'url' => 'Tiền 124mặt',
                'description' => 'Tiền mặt',
                'icon' => 'Tiền mặt',
            ],
            [
                'name' => 'Giao hàng tiết kiệm',
                'token' => 'Tiền mặt',
                'url' => 'Tiền 412mặt',
                'description' => 'Tiền mặt',
                'icon' => 'Tiền mặt',
            ],
            [
                'name' => 'Viettel Post',
                'token' => 'Tiền mặt',
                'url' => 'Tiền123 mặt',
                'description' => 'Tiền mặt',
                'icon' => 'Tiền mặt',
            ],
            [
                'name' => 'SPX',
                'token' => 'Tiền mặt',
                'url' => 'Tiền22 mặt',
                'description' => 'Tiền mặt',
                'icon' => 'Tiền mặt',
            ],
        ];
        \DB::table("shipping_units")->insert($shipping);
    }
}
