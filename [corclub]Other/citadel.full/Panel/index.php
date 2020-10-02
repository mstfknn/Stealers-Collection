<?php
session_start();
if(isset($_SESSION['auth']))
    header("Location: /main.php");
include ('db.php');
if(isset($_POST['submit']))
{
	if(!empty($_POST['username']) && !empty($_POST['password']))
	{
		$username = mysqli_real_escape_string($dbcnx, htmlspecialchars($_POST['username']));
		$password = mysqli_real_escape_string($dbcnx, htmlspecialchars($_POST['password']));
		$query = mysqli_query($dbcnx, "SELECT * FROM users WHERE id=1;");
		$numrows=mysqli_num_rows($query);
		if($numrows != 0)
		{
			while($row=mysqli_fetch_assoc($query))
 			{
				$dbusername=$row['username'];
  				$dbpassword=$row['password'];
 			}
  			if($username == $dbusername && $password == $dbpassword)
 			{
    			$_SESSION['auth'] = 'true';
				header("Location: /main.php");
			}
			} 
			else 
			{
	
				echo '<script language="javascript">';
				echo 'alert("Bad password")';
				echo '</script>';
 			}

		} 
		else 
		{
    			echo '<script language="javascript">';
				echo 'alert("All fields are reqired ")';
				echo '</script>';
		}
	}
?>
<!DOCTYPE html>
<html>
 <head>
  <meta charset="utf-8">
  <title>Login</title>
 <link href="css/style.css" rel ="stylesheet" type="text/css"/>
 <link rel="shortcut icon" href="/img/cit.png" type="image/x-icon">
 </head>
 <body>
<div class ="login">
	<img src ="img/anon.png">
	<form action = "" method = "POST">
		<div class = form>
			<input type = "text" name = "username" placeholder="Login">
		</div>
		<div class = form>	
			<input type = "password" name = "password" placeholder="Password">
		</div>	
			<input class ="submit" type = "submit" name = "submit" value="GO">
	</p>
	</form>	
</div>
 </body>
</html>
	
	