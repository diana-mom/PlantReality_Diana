<?php

namespace App\Http\Controllers;

use App\Purchase;
use Illuminate\Http\Request;

class PurchaseController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        return Auth::user()->purchases()->get();
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @return \Illuminate\Http\Response
     */
    public function store(Request $request)
    {
        return Purchase::create([
            'price' => $request->input('price'),
            'amount' => $request->input('amount'),
            'auction_flower_id' => $request->input('auction_flower_id'),
            'customer_id' => Auth::id(),
        ]);

    }

    /**
     * Display the specified resource.
     *
     * @param  \App\Purchase  $purchase
     * @return \Illuminate\Http\Response
     */
    public function show(Purchase $purchase)
    {
        if (!IsAuthorized($purchase)) {
            return response()->json(['error' => 'Not authorized.'], 403);
        }
        return $purchase;
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @param  \App\Purchase  $purchase
     * @return \Illuminate\Http\Response
     */
    public function update(Request $request, Purchase $purchase)
    {
        if (!IsAuthorized($purchase)) {
            return response()->json(['error' => 'Not authorized.'], 403);
        }
        $purchase->fill($request->all());
        return $purchase->update();
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  \App\Purchase  $purchase
     * @return \Illuminate\Http\Response
     */
    public function destroy(Purchase $purchase)
    {
        if (!IsAuthorized($purchase)) {
            return response()->json(['error' => 'Not authorized.'], 403);
        }
        return $purchase->delete();

    }

    private function IsAuthorized($purchase)
    {
        return $purchase->customer->id == Auth::id();
    }
}
