<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateAuctionsFlowersTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('auctions_flowers', function (Blueprint $table) {
            $table->bigIncrements('id');
            $table->integer('amount');
            $table->integer('flowers_per_element');
            $table->integer('min_price');
            $table->integer('start_price');
            $table->integer('price');
            $table->timestamps();
        });
        Schema::table('auctions_flowers', function (Blueprint $table) {
            $table->bigInteger('auction_id')->unsigned();
            $table->foreign('auction_id')->references('id')->on('auctions');
            $table->bigInteger('flower_id')->unsigned();
            $table->foreign('flower_id')->references('id')->on('flowers');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('auction_flowers');
    }
}
