<?php

namespace Database\Seeders;

// use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class DatabaseSeeder extends Seeder
{
    /**
     * Seed the application's database.
     */
    public function run(): void
    {
        // \App\Models\User::factory(10)->create();

        // \App\Models\User::factory()->create([
        //     'name' => 'Test User',
        //     'email' => 'test@example.com',
        // ]);


        \DB::table("taxes")->insert([
            [
                "name"=> "Không thuế",
                "value" =>"0"
            ],
        ]);
        \DB::table("brands")->insert([
            [
                "name"=> "Apple",
                "code" =>"Apple"
            ],
            [
                "name"=> "SamSung",
                "code" =>"SamSung"
            ],
            [
                "name"=> "Xiaomi",
                "code" =>"Xiaomi"
            ],
            [
                "name"=> "ViVo",
                "code" =>"ViVo"
            ],
            [
                "name"=> "Oppo",
                "code" =>"Oppo"
            ],
            [
                "name"=> "LG",
                "code" =>"lg"
            ],
            [
                "name"=> "Motorola",
                "code" =>"DD"
            ],
            [
                "name"=> "Nokia",
                "code" =>"Nokia"
            ],
            [
                "name"=> "Realme",
                "code" =>"Realme"
            ],
            [
                "name"=> "Huawei",
                "code" =>"Huawei"
            ],
            [
                "name"=> "OnePlus",
                "code" =>"OnePlus"
            ],
            [
                "name"=> "Sony",
                "code" =>"Sony"
            ],
            [
                "name"=> "HTC",
                "code" =>"htc"
            ],
            [
                "name"=> "Lenovo",
                "code" =>"Lenovo"
            ],
            [
                "name"=> "Dell",
                "code" =>"Dell"
            ],
            [
                "name"=> "MSI",
                "code" =>"msi"
            ],
            [
                "name"=> "Asus",
                "code" =>"Asus"
            ],
            [
                "name"=> "HP",
                "code" =>"hp"
            ],
        ]);
        $this->call([PaymentMethodSeeder::class, ShippingUnitSeeder::class, MenuSeeder::class, CategorySeeder::class, SupplierSeeder::class, ProductSeeder::class, CustomerSeeder::class]);
    }
}
