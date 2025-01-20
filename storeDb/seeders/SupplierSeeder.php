<?php

namespace Database\Seeders;

use Illuminate\Support\Str;
use Illuminate\Database\Seeder;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;

class SupplierSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        $suppliers = [];
            for ($j = 0; $j < 20; $j++) {
                $name = fake()->name;
                $suppliers[] = [
                    'code' => Str::random(10),
                    'name' => $name,
                    'address' => fake()->unique()->safeEmail(),
                    'phone' => fake()->phoneNumber,
                    'email' => fake()->email,
                ];
            }
            \DB::table('suppliers')->insert($suppliers);
    }
}
