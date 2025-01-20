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
        Schema::create('products', function (Blueprint $table) {
            $table->id();
            $table->string('code', 24)->unique();
            $table->string('barcode', 24)->unique();
            $table->string('name');
            $table->double('price',12,4)->default(0);
            $table->unsignedTinyInteger('number_warning')->default(0);
            $table->json('images')->nullable();
            $table->string('unit_sell', 150);
            $table->string('unit_buy', 150);
            $table->text('description')->nullable();
            $table->string('alias');
            $table->boolean('can_edit')->default(true);
            $table->boolean('can_delete')->default(true);
            $table->boolean('has_variants')->default(false);
            $table->boolean('is_new')->default(false);
            $table->boolean('is_featured')->default(false);
            $table->foreignId('brand_id')->constrained();
            $table->foreignId('category_id')->constrained();
            $table->foreignId('tax_id')->constrained();
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('products');
    }
};
