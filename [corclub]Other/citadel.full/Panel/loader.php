<?php
session_start();
if($_SESSION['auth'] != 'true'){
    header("Location: /index.php");
    exit();
}
if(isset($_POST['down']))
{
$filepost = $_POST['down'];
file_force_download($filepost);
}
function file_force_download($file) {
  if (file_exists($file)) {
    header('Content-Description: File Transfer');
    header('Content-Type: application/octet-stream');
    header('Content-Disposition: attachment; filename=' . basename($file));
    header('Content-Transfer-Encoding: binary');
    header('Expires: 0');
    header('Cache-Control: must-revalidate');
    header('Pragma: public');
    header('Content-Length: ' . filesize($file));
    readfile($file);
    unlink('logs.zip');
    exit();
  }
}

?>
