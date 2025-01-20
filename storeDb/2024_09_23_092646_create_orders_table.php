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
            $table->string('code', 24)->unique();
            $table->date('order_date');
            $table->date('delivery_date')->nullable();
            $table->foreignId('customer_id')->nullable()->constrained();
            $table->foreignId('user_id')->nullable()->constrained();
            $table->double('total_amount', 12, 4)->default(0);
            $table->double('discount_amount', 12, 4)->default(0);
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
