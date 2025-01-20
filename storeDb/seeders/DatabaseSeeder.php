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
                "name"=> "Test 1",
                "code" =>"DD"
            ],
            [
                "name"=> "Test 2",
                "code" =>"DD2"
            ],
        ]);
        $this->call([MenuSeeder::class, CategorySeeder::class, SupplierSeeder::class, ProductSeeder::class, CustomerSeeder::class]);
    }
}
