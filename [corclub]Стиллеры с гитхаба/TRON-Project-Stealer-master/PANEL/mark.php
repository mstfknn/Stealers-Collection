<?php
include ("settings.php");

if ($_COOKIE["session"] !== $session_key)
{
    header("Location:  login.php");
    die();
}

if (preg_match("/^[[\S]+](?:\d{1,3}\.){4}[ \w]+.zip$/u", $_GET['file']))
{
    $file = $_GET['file'];

    if (stristr($file, '[Unchecked]') === false)
    {

        rename(__DIR__ . "/files/$file", __DIR__ . "/files/" . str_replace("[Checked]", "[Unchecked]", $file));
    }
    else
    {
        rename(__DIR__ . "/files/$file", __DIR__ . "/files/" . str_replace("[Unchecked]", "[Checked]", $file));
    }

    header("Location: table.php");

}
?>
