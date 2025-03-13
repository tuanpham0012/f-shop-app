<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     */
    public function up(): void
    {
        Schema::create('suppliers', function (Blueprint $table) {
            $table->id();
            $table->string('code', 24)->unique();
            $table->string('name');
            $table->string('address', 300)->nullable();
            $table->string('phone', 18)->nullable();
            $table->string('email', 100)->unique();
            $table->string('logo', 500)->nullable();
            $table->string('tax_code', 24)->nullable();
            $table->text('description')->nullable();
            $table->boolean('not_use')->default(false);
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('suppliers');
    }
};
