<?php

namespace Database\Seeders;

use Illuminate\Support\Str;
use Illuminate\Database\Seeder;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;

class ProductSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        for ($i = 0; $i < 40; $i++) {
            $products = [];
            for ($j = 0; $j < 40; $j++) {
                $name = fake()->name;
                $products[] = [
                    'code' => Str::random(10),
                    'barcode' => (Str::random(17)),
                    'name' => $name,
                    'number_warning' => rand(20, 50),
                    'price' => rand(100000, 500000),
                    'unit_sell' => fake()->firstName,
                    'unit_buy' => fake()->lastName,
                    'alias' => Str::slug($name, '-'),
                    'category_id' => rand(1, 7),
                    'brand_id' => rand(1, 18),
                    'tax_id' => 1,
                    'is_new' => rand(0, 1),
                    'is_featured' => rand(0, 1),
                    'is_sale' => rand(0, 1),
                    'created_at' => now(),
                    'updated_at' => now(),
                ];
            }
            \DB::table('products')->insert($products);
        }
    }
}
