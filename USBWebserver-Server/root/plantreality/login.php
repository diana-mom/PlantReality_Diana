<?php

include_once("UserController.php");

$pincode = $_POST['pincode'];
$usercode = $_POST['usercode'];

$user = UserController::getUser($usercode, $pincode);

if($user != null){
    //get the user info
    echo json_encode($user);

}else{
    echo "";
}

?>