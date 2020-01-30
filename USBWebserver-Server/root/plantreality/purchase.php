<?php
include_once("DBConnection.php");
include_once("UserController.php");


$con = DBConnection::connectToDatabase();
$auction_flower_id = $_POST['auction_flower_id'];
$usercode = $_POST['usercode'];
$pincode = $_POST['pincode'];
$price = $_POST['price'];
$amount = $_POST['amount'];

//check if user exists
$user = UserController::getUser($usercode, $pincode);

if($user == null){
    echo "User does not exist.";
    exit();
}

//check if this person is buying more than the amount left
$amountcheckquery = "SELECT amount FROM auction_flower WHERE id = ?;";
$amountcheckstatement = $con->prepare($amountcheckquery);
$amountcheckstatement->bindParam(1, $auction_flower_id);
$amountcheckstatement->execute();
$amount_left = $amountcheckstatement->fetchColumn();


if($amount > $amount_left){
    $amount = $amount_left; //correct amount to the units left.
}

$new_amount = $amount_left-$amount;

//edit auction_flower_id, lower amount
$loweramountquery = "UPDATE auction_flower SET amount = ? WHERE id = ?;";
$loweramountstatement = $con->prepare($loweramountquery);
$loweramountstatement->bindParam(1, $new_amount);
$loweramountstatement->bindParam(2, $auction_flower_id);
$loweramountstatement->execute();

//insert purchase into database
$namecheckquery = "INSERT INTO purchase (auction_flower_id, customer_id, price, amount) VALUES (?, ?, ?, ?);";
$insertstatement = $con->prepare($namecheckquery);
$insertstatement->bindParam(1, $auction_flower_id);
$insertstatement->bindValue(2, intVal($user["id"]));
var_dump(intVal($user["id"]));
$insertstatement->bindParam(3, $price);
$insertstatement->bindValue(4, 1);


if($insertstatement->execute()){
    echo "Purchase made!";
}else{
    echo "Error while making purchase...";
}
?>