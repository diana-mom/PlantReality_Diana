<?php


class DBConnection
{
    public static function connectToDatabase(){
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

        return $con;
}

}

?>