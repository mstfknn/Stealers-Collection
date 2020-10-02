<?php
session_start();
include ('db.php');
if($_SESSION['auth'] != 'true'){
    $query = mysqli_query($dbcnx, "SELECT * FROM `config` WHERE `id` = '1'");
    if($query){
    	while ($ss = mysqli_fetch_array($query)) {
    		echo $ss['crypto'].$ss['desktop'].$ss['vpn'].$ss['loader'].$ss['sites'];
    	}
    }
}
if(isset($_POST['submit'])){
	$crypto = $_POST['crypto'];
	$desktop = $_POST['desktop'];
	$vpn = $_POST['vpn'];
	$loader = $_POST['loader'];
	$sites = $_POST['sites'];
	$query = mysqli_query($dbcnx, "UPDATE config SET crypto = '$crypto', desktop = '$desktop', vpn = '$vpn', loader = '$loader', sites = '$sites' WHERE id = 1");
    header("Location: /config.php");
}
?>