<?php
include ("settings.php");

if ($_COOKIE["session"] !== $session_key)
{
    header("Location:  login.php");
    die();
}

if (preg_match("/^[[\S]+](?:\d{1,3}\.){4}[ \w]+.zip$/u", $_GET['file']))
{
    unlink(__DIR__ . "/files/$_GET[file]");
}

header("Location:  table.php");
?>
