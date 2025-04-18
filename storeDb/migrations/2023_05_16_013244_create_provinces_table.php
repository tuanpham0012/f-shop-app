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
        Schema::create('provinces', function (Blueprint $table) {
            $table->id();
            $table->string('country_code',255)->nullable();
            $table->string('name',100)->nullable();
            $table->string('lat',255)->nullable();
            $table->string('lng',255)->nullable();
            $table->string('zipcode',255)->nullable();
            $table->string('code',255)->nullable();
            $table->foreignId('country_id')->nullable()->constrained();
            $table->string('ghn_province_id')->nullable();
            $table->string('ghn_province_code')->nullable();
            $table->string('vtp_province_id')->nullable();
            $table->string('vtp_province_code')->nullable();
            $table->string('vnp_province_id')->nullable();
            $table->string('vnp_province_code')->nullable();
            $table->unsignedBigInteger('created_by')->nullable();
            $table->unsignedBigInteger('updated_by')->nullable();
            $table->unsignedBigInteger('deleted_by')->nullable();
            $table->softDeletes();
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('cities');
    }
};
