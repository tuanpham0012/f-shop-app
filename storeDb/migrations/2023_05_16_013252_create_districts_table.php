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
        // Schema::create('districts', function (Blueprint $table) {
        //     $table->id();
        //     $table->string('name',100);
        //     $table->foreignId('province_id')->nullable()->constrained();
        //     $table->string('ghn_district_id')->nullable();
        //     $table->string('ghn_district_code')->nullable();
        //     $table->string('vtp_district_id')->nullable();
        //     $table->string('vtp_district_code')->nullable();
        //     $table->string('vnp_district_id')->nullable();
        //     $table->string('vnp_district_code')->nullable();
        //     $table->unsignedBigInteger('created_by')->nullable();
        //     $table->unsignedBigInteger('updated_by')->nullable();
        //     $table->unsignedBigInteger('deleted_by')->nullable();
        //     $table->softDeletes();
        //     $table->timestamps();
        // });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('districts');
    }
};
