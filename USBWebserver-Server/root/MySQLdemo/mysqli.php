<?php 
$username=$_GET['username']; 

if($username) 
{ 
	$conn=mysql_connect("localhost","root","usbw") or 
		die('Error: ' . mysql_error()); 
		
	mysql_select_db("test"); 
	
	$SQL="select * from t1 where user='".$username."'"; 
	//echo "SQL=".$SQL."<br>"; 
	$result=mysql_query($SQL) or 
		die('Error: ' . mysql_error());; 
	
	$row=mysql_fetch_array($result); 
	if($row['user']) 
	{ 
		echo "Username:".$row['user']."<br>"; 
		echo "Description:".$row['des']."<br>"; 
		echo "TestOK!<br>"; 
	} 
	else 
		echo "No Record!"; 
		
	mysql_free_result($result); 
	mysql_close(); 
} 
?>
