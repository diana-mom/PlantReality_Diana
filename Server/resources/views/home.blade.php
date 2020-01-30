@extends('layouts.app')

@section('content')
<div class="container">
    <div class="row justify-content-center">
        <div class="col-md-8">
            <passport-clients class="p-2"></passport-clients>
            <passport-authorized-clients class="p-2"></passport-authorized-clients>
            <passport-personal-access-tokens class="p-2"></passport-personal-access-tokens>
        </div>
    </div>
</div>
@endsection
