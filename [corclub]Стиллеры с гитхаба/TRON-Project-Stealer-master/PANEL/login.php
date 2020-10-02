<?php
include ("settings.php");

if (isset($_COOKIE["session"]))
{
    if ($_COOKIE["session"] === $session_key)
    {
        header("Location: table.php");
        die();
    }
}

if (isset($_POST["password"]))
{
    if (md5($_POST["password"]) === $password_md5)
    {
        setcookie("session", $session_key);
        header("Location: table.php");
        die();
    }
}

?>

<!DOCTYPE html>
<html>
	<head>
		<link rel="stylesheet" href="css/login.css">
		<link rel="stylesheet" href="css/common.css">
		<meta charset="utf-8">
		<title>Welcome</title>
	<head>
	<body>
	<a id="project" style="font-size: 50pt;">TRON</a>
<div id="login">
	 <div id="login-form">
		<form action="" method="POST">
			<input type = "password" name = "password">          
			<input type="submit" name="submit" value="Login">
			</form>
	</div>
	</div>
	</body>
</html>
