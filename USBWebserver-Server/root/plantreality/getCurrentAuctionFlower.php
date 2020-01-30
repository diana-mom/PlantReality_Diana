<?php

$servername = "remotemysql.com";
$username = "gQKLrxIjYu";
$password = "x38CzVkIHR";
$dbname = "gQKLrxIjYu";

$con = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
$con->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

//check that connection happened
if ($con->errorCode() != 0) {
    echo "1: connection failed"; //error code #1 = connection failed
    exit();
}

$auction_id = $_POST["auction_id"];

//Get auction_flower_id
$query = "SELECT id FROM auction_flower WHERE auction_id = ? AND amount != 0 LIMIT 1;";
$statement = $con->prepare($query);
$statement->bindParam(1, $auction_id);
$statement->execute();

//Check if object exists
if($statement->rowCount()!=0)
{
    $res = $statement->fetch() or die("Couldn't fetch auction_flower");
    //get login info from query
    echo json_encode($res);
}
else
{
    echo "";
}
?>