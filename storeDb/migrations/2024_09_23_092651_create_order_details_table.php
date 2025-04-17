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
        Schema::create('order_details', function (Blueprint $table) {
            $table->id();
            $table->foreignId('order_id')->constrained()->cascadeOnDelete();
            $table->foreignId('product_id')->constrained()->cascadeOnDelete();
            $table->foreignId('sku_id')->constrained()->cascadeOnDelete();
            $table->string('product_name', 255)->nullable();
            $table->double('unit_price', 12, 4)->default(0);
            $table->double('unit_discount', 12, 4)->default(0);
            $table->double('tax_fee', 12, 4)->default(0);
            $table->integer('quantity')->default(0);
            $table->double('total_amount', 12, 4)->default(0);
            $table->double('discount_amount', 12, 4)->default(0);
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('order_details');
    }
};
