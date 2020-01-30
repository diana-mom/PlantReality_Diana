<?php
include_once("DBConnection.php");

class UserController
{

    public static function getUser($usercode, $pincode){
        $con = DBConnection::connectToDatabase();
        $namecheckquery = "SELECT * FROM customer WHERE usercode = ? AND pincode = ?;";
        $statement = $con->prepare($namecheckquery);
        $statement->bindParam(1, $usercode);
        $statement->bindParam(2, $pincode);
        $statement->execute();

        if($statement->rowCount()!=0) {
            //get the user info
            $namecheck = $statement->fetch() or die("Name check query failed");
            return $namecheck;
        }
        return null;
    }
}

?>