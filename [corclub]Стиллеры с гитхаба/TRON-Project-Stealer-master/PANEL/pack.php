<?php
include ("settings.php");

if ($_COOKIE["session"] !== $session_key)
{
    header("Location:  login.php");
    die();
}
$outfile = __DIR__ . "/files/packed_" . md5(mt_rand()) . ".zip";

$za = new ZipArchive();
if ($za->open($outfile, ZipArchive::CREATE | ZipArchive::OVERWRITE) !== true)
{
    echo "Can't create an archive.";
    die();
}

$za->addPattern("/^[[\S]+](?:\d{1,3}\.){4}[ \w]+.zip$/u", "files");
$za->close();

ignore_user_abort(true);

header("Content-Type: application/zip");
header("Content-Transfer-Encoding: Binary");
header("Content-Length:" . filesize($outfile));
header("Content-Disposition: attachment; filename=\"" . basename($outfile) . "\"");
readfile($outfile);

unlink($outfile);
?>
