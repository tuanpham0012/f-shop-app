<?php

namespace Database\Seeders;

use Illuminate\Support\Str;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\Hash;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;

class CustomerSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        for ($j = 0; $j < 10; $j++) {
            $arr = [];
            for ($k = 0; $k < 10; $k++) {
                $arr[] = [
                    'name' => fake()->name(),
                    'email' => $j . $k . fake()->unique()->safeEmail(),
                    'email_verified_at' => now(),
                    'address' => fake()->address(),
                    'status' => rand(0, 2),
                    'gender' => rand(0, 2),
                    'phone' => fake()->phoneNumber(),
                    'password' => 'yX9/jUw4mRFUQ61jxD0OuAuN+3+DaGz44M4UvMYzL94=', // password@123A
                    'salt' => 'Kjv8o96q8DLYYidCFlmQgA==',
                    'remember_token' => Str::random(15),
                    'created_at' => now(),
                    'updated_at' => now(),
                ];
            }
            \DB::table('customers')->insert($arr);
            
        }
        \DB::table('customers')->insert([
                    'name' => 'Phạm Quốc Tuấn',
                    'email' => 'tuanpham0012@gmail.com',
                    'email_verified_at' => now(),
                    'address' => fake()->address(),
                    'status' => 1,
                    'gender' => 1,
                    'phone' => fake()->phoneNumber(),
                    'password' => 'yX9/jUw4mRFUQ61jxD0OuAuN+3+DaGz44M4UvMYzL94=', // password@123A
                    'salt' => 'Kjv8o96q8DLYYidCFlmQgA==',
                    'remember_token' => Str::random(15),
                    'created_at' => now(),
                    'updated_at' => now(),
                ]);
    }
}
