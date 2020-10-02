<?php
$dblocation = "127.0.0.1"; 
$dbuser = "root";
$dbpasswd = "";
$dbname  = "citadel";
$dbcnx = @mysqli_connect($dblocation,$dbuser,$dbpasswd);


if (!$dbcnx) 
{
  echo ("<h1>Server unavailable</h1>");
  exit();
}

if (!@mysqli_select_db($dbcnx, $dbname)) 
{
  echo( "<h1>Server is not available now</h1>" );
  exit();
}
mysqli_select_db($dbcnx, $dbname);
mysqli_set_charset($dbcnx,'utf8');
?>