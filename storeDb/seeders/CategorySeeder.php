<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class CategorySeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        $categories = [
            [
                "name"=> "Rau củ",
                "code" =>"Vegetable"
            ],
            [
                "name"=> "Hoa quả",
                "code" =>"Fruit"
            ],
            [
                "name"=> "Thực phẩm khô",
                "code" =>"Dry_food"
            ],
            [
                "name"=> "Thực phẩm đông lạnh",
                "code" =>"Frozen_food"
            ],
            [
                "name"=> "Đồ uống",
                "code" =>"Beverage"
            ],
            [
                "name"=> "Bánh kẹo",
                "code" =>"Confectionery"
            ],
            [
                "name"=> "Gia vị",
                "code" =>"Spice"
            ],
        ];
        \DB::table("categories")->insert($categories);
    }
}
