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
        Schema::create('orders', function (Blueprint $table) {
            $table->id();
            $table->string('code', 36)->unique();
            $table->date('order_date');
            $table->date('delivery_date')->nullable();
            $table->foreignId('customer_id')->constrained();
            $table->foreignId('user_id')->nullable()->constrained();
            $table->foreignId('payment_method_id')->constrained();
            $table->foreignId('shipping_unit_id')->constrained();
            $table->double('total_amount', 12, 4)->default(0);
            $table->double('discount_amount', 12, 4)->default(0);
            $table->double('shipping_fee', 12, 4)->default(0);
            $table->string('shipping_address', 255)->nullable();
            $table->string('shipping_phone', 255)->nullable();
            $table->string('receiver_name', 255)->nullable();
            $table->tinyInteger('status')->default(0);
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('orders');
    }
};
