<?php
include ("settings.php");

if ($_COOKIE["session"] !== $session_key)
{
    header("Location:  login.php");
    die();
}

$files = array_filter(scandir(__DIR__ . "/files") , function ($elem)
{
    return preg_match("/^[[\S]+](?:\d{1,3}\.){4}[ \w]+.zip$/u", $elem);
});

foreach ($files as $filename)
{
    unlink(__DIR__ . "/files/$filename");
}

header("Location: table.php");
?>
