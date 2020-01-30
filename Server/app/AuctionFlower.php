<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class AuctionFlower extends Model
{
    /**
     * The table associated with the model.
     *
     * @var string
     */
    protected $table = 'auctions_flowers';
    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = [
        'amount',
        'flowers_per_element',
        'price_highest',
        'price_lowest',
        'auction_id',
        'flower_id',
    ];
}
