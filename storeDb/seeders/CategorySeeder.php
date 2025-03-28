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
                "code" =>"Vegetable",
                "is_popular" => 1
            ],
            [
                "name"=> "Hoa quả",
                "code" =>"Fruit",
                "is_popular" => 1
            ],
            [
                "name"=> "Thực phẩm khô",
                "code" =>"Dry_food",
                "is_popular" => 1
            ],
            [
                "name"=> "Thực phẩm đông lạnh",
                "code" =>"Frozen_food",
                "is_popular" => 1
            ],
            [
                "name"=> "Đồ uống",
                "code" =>"Beverage",
                "is_popular" => 1
            ],
            [
                "name"=> "Bánh kẹo",
                "code" =>"Confectionery",
                "is_popular" => 1
            ],
            [
                "name"=> "Gia vị",
                "code" =>"Spice",
                "is_popular" => 1
            ],
        ];
        \DB::table("categories")->insert($categories);
    }
}
