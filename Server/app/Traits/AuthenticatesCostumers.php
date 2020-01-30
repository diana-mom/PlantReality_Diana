<?php

namespace App\Traits;

use Illuminate\Http\Request;

trait AuthenticatesCustomers
{
    /**
     * Get the needed authorization credentials from the request.
     *
     * @param  \Illuminate\Http\Request  $request
     * @return array
     */
    protected function credentials(Request $request)
    {
        return $request->only($this->username(), 'pincode');
    }
}
