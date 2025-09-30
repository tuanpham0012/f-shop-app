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
        Schema::create('menus', function (Blueprint $table) {
            $table->id();
            $table->string('name');
            $table->string('path')->nullable();
            $table->string('icon')->nullable();
            $table->unsignedTinyInteger('position')->default(1);
            $table->nestedSet();
            $table->boolean('active')->default(1);
            $table->tinyInteger('type')->default(0)->comment('0: admin, 1: user');
            $table->boolean('group_menu')->default(false);
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('menus');
    }
};
