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
        Schema::create('countries', function (Blueprint $table) {
            $table->id();
            $table->string('code',255);
            $table->string('name',255)->nullable();
            $table->bigInteger('continent_id')->unsigned()->nullable();
            $table->string('code_alpha2',20)->nullable();
            $table->string('code_alpha3',20)->nullable();
            $table->string('code_numeric',20)->nullable();
            $table->string('fips',20)->nullable();
            $table->string('capital',30)->nullable();
            $table->string('flag',10)->nullable();
            $table->string('map',200)->nullable();
            $table->string('phone_code',20)->nullable();
            $table->integer('key')->unsigned()->nullable();
            $table->integer('area')->unsigned()->nullable();
            $table->integer('population')->unsigned()->nullable();
            $table->string('tld',20)->nullable();
            $table->string('currency_code',20)->nullable();
            $table->string('currency_name',20)->nullable();
            $table->string('postal_code_format',255)->nullable();
            $table->string('postal_code_regex',255)->nullable();
            $table->string('languages',50)->nullable();
            $table->integer('geoname_id')->unsigned()->nullable();
            $table->string('neighbours',255)->nullable();
            $table->string('equivalent_fips_code',255)->nullable();
            $table->tinyInteger('status')->default(1);
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
        Schema::dropIfExists('countries');
    }
};
