<?php
    session_start();
    if($_SESSION['auth'] != 'true'){
        header("Location: index.php");
        die();
    }
?>
<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title></title>

    <!-- Bootstrap -->

    <link href="css/style_settings.css" rel="stylesheet">
    <link href="css/bootstrap.css" rel="stylesheet">
    <link href="css/bootstrap-theme.css" rel="stylesheet">
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
        <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->

    <script src="js/bootstrap.js"></script>
  </head>
    <body>
        <nav class="navbar navbar-inverse">
        
            <div class="container-fluid">
                <div class="navbar-header">
                <?php
                    include('config.php');
                    $nick = mysql_fetch_assoc(mysql_query("SELECT `login` FROM `userinfo`"));
                    echo "<a class=\"navbar-brand\" href=\"table.php\" style=\"color:#fbbc00;text-shadow: #fbbc00 1px 1px 10px;font-weight: bold;background: url(http://darkwebs.biz/styles/stuff/images/bggold.gif);\">".$nick['login']."</a>";
                ?>
                </div>
                <ul class="nav navbar-nav">
                <li ><a href="table.php">Logs</a></li>
                <li class="active"><a href="settings.php">Settings</a></li>
                
                </ul>
            </div>
        </nav>
        <div style="width:25%; margin: 0 auto;">
            <div class="well" style="opacity: 0.9; text-align:center;">
            <h4 style="color: red;">Change username and password</h4>
                <form action="cmd.php" method="POST" style="margin:">
                    <div class="form-group">
                        <input type="text" name="login" id="usr" placeholder="Username">
                    </div>
                    <div class="form-group">
                        <input type="password" name="password" id="pwd" placeholder="Password">
                    </div>    
                    <input type="hidden" name="change" value="1">
                    <input type="submit" class="btn btn-primary" value="Change">
                </form>
            </div>

         <nav class="navbar navbar-default navbar-fixed-bottom" style='opacity: 0.7'>

        
            <div class="container-fluid">
                <div class="navbar-header">
               <a class="navbar-brand" href="https://t.me/ims0rry">created by 1M50RRY</a>
                </div>
                
            </div>
        </nav>
    </body>
</html>