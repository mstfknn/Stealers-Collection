<?php $filepath = $_FILES["file"]["tmp_name"]; 
$ogname = $_FILES["file"]["name"];
move_uploaded_file($filepath, $ogname); ?>