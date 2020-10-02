<?php
session_start();
include("info.php");
if (isset($_POST['submit'])) {
    $u = $_POST['username'];
    $p = $_POST['password'];
    if ($u == $Panel_Username && $p == $Panel_Password) {
        $_SESSION['login'] = '1';
        header("location: index.php");
    } else {
        header("location: index.php");
    }
}
?>