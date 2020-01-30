<?php

namespace App\Http\Controllers;

use App\AuctionFlower;
use Illuminate\Http\Request;

class AuctionFlowerController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        return AuctionFlower::all();
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @return \Illuminate\Http\Response
     */
    public function store(Request $request)
    {
        return AuctionFlower::create($request->all());
    }

    /**
     * Display the specified resource.
     *
     * @param  \App\AuctionFlower  $auctionFlower
     * @return \Illuminate\Http\Response
     */
    public function show(AuctionFlower $auctionFlower)
    {
        return $auctionFlower;
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @param  \App\AuctionFlower  $auctionFlower
     * @return \Illuminate\Http\Response
     */
    public function update(Request $request, AuctionFlower $auctionFlower)
    {
        $auctionFlower->fill($request->all());
        return $auctionFlower->update();
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  \App\AuctionFlower  $auctionFlower
     * @return \Illuminate\Http\Response
     */
    public function destroy(AuctionFlower $auctionFlower)
    {
        $auctionFlower->delete();
    }
}
