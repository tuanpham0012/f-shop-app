<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class PaymentMethodSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        $payments = [
            [
                'name' => 'Thanh toán khi nhận hàng',
                'token' => 'Tiền mặt',
                'url' => 'Tiền 123412 mặt',
                'description' => 'Tiền mặt',
                'icon' => 'Tiền mặt',
            ],
            [
                'name' => 'Ví Momo',
                'token' => 'Tiền mặt',
                'url' => 'Tiền 412 mặt',
                'description' => 'Tiền mặt',
                'icon' => 'Tiền mặt',
            ],
            [
                'name' => 'Ví ZaloPay',
                'token' => 'Tiền mặt',
                'url' => 'Tiền 4123mặt',
                'description' => 'Tiền mặt',
                'icon' => 'Tiền mặt',
            ],
            [
                'name' => 'Ví VnPay',
                'token' => 'Tiền mặt',
                'url' => 'Tiền2134 mặt',
                'description' => 'Tiền mặt',
                'icon' => 'Tiền mặt',
            ],
            [
                'name' => 'Chuyển khoản',
                'token' => 'Tiền mặt',
                'url' => 'Tiền421 mặt',
                'description' => 'Tiền mặt',
                'icon' => 'Tiền mặt',
            ],
        ];
        \DB::table("payment_methods")->insert($payments);
    }
}
