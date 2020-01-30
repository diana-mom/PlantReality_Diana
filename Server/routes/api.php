<?php

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| is assigned the "api" middleware group. Enjoy building your API!
|
 */

Route::group([
    'prefix' => 'auth',
], function () {
    Route::post('login', 'CustomerController@login');
    Route::post('register', 'CustomerController@register');

    Route::group([
        'middleware' => 'auth:api',
    ], function () {
        Route::get('logout', 'CustomerController@logout');
        Route::get('user', 'CustomerController@user');
    });
});

Route::middleware('auth:api')->group(function () {
    Route::resource('auctions', 'AuctionController')->except(['create', 'edit']);
    Route::resource('auctionflowers', 'AuctionFlowerController')->except(['create', 'edit']);
    Route::resource('flowers', 'FlowerController')->except(['create', 'edit']);
    Route::resource('purchases', 'PurchaseController')->except(['create', 'edit']);
});
