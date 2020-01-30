<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateAuctionsTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('auctions', function (Blueprint $table) {
            $table->bigIncrements('id');
            $table->dateTime('start');
            $table->dateTime('end')->nullable();
            $table->integer('clock')->unsigned();
            $table->timestamps();
        });
        Schema::table('auctions', function (Blueprint $table) {
            // $table->bigInteger('auctions_flowers_id')->unsigned();
            // $table->foreign('auctions_flowers_id')->references('id')->on('auctions_flowers');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('auctions');
    }
}
