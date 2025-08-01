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
        Schema::create('wards', function (Blueprint $table) {
            $table->id();
            $table->string('name',100);
            $table->string('short_name',100);
            $table->string('ward_code',100);
            // $table->string('prefix',100);
            $table->foreignId('province_id')->nullable()->constrained();
            // $table->foreignId('district_id')->nullable()->constrained();
            $table->string('ghn_ward_id')->nullable();
            $table->string('ghn_ward_code')->nullable();
            $table->string('vtp_ward_id')->nullable();
            $table->string('vtp_ward_code')->nullable();
            $table->string('vnp_ward_id')->nullable();
            $table->string('vnp_ward_code')->nullable();
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('wards');
    }
};
