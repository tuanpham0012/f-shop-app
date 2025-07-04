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
        Schema::create('delivery_addresses', function (Blueprint $table) {
            $table->id();
            $table->foreignId('customer_id')->constrained()->onDelete('cascade');
            $table->string('full_name', 500)->nullable();
            $table->string('address', 500)->nullable();
            $table->string('phone', 25)->nullable();
            $table->foreignId('province_id')->nullable()->constrained();
            $table->foreignId('ward_id')->nullable()->constrained();
            $table->string('lat')->nullable();
            $table->string('lng')->nullable();
            $table->boolean('default')->default(false)->comment('Is this the default address?');
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('delivery_addresses');
    }
};
