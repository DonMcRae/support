<?php

$user = "ENTER_USERNAME_HERE";
$pass = "ENTER_PASSWORD_HERE";

$rule_value = "testRule";
$rule_tag = "testTag";

//	Ensure that your stream format matches the rule format you intend to use (e.g. '.xml' or '.json')
//	See below to edit the rule format used when adding and deleting rules (xml or json)
			
//	Expected Enterprise Data Collector URL formats:
//		JSON:	https://<host>.gnip.com/data_collectors/<data_collector_id>/rules.json
//		XML:	https://<host>.gnip.com/data_collectors/<data_collector_id>/rules.xml

//	Expected Premium Stream URL Format:
//		https://api.gnip.com:443/accounts/<account>/publishers/<publisher>/streams/<stream>/<label>/rules.json

$url = "ENTER_RULES_API_URL_HERE";

//	Edit below to use the rule format that matches the Rules API URL you entered above

//	Use this line for XML formatted rules
	$rules = "<rules><rule tag=\"".$rule_tag."\"><value>".$rule_value."</value></rule></rules>";

//	Use this line for JSON formatted rules
//	$rules = "{\"rules\":[{\"value\":\"".$rule_value."\",\"tag\":\"".$rule_tag."\"}]}";

$ch   = curl_init($url);
curl_setopt_array($ch, array(
  CURLOPT_URL => $url,
  CURLOPT_ENCODING => "gzip",
  CURLOPT_HTTPAUTH => CURLAUTH_BASIC,
  CURLOPT_USERPWD => $user.":".$pass,
  CURLOPT_POST    => 1,
  CURLOPT_POSTFIELDS => $rules,
//  CURLOPT_VERBOSE => true  // Uncomment for curl verbosity
));

    $content = curl_exec( $ch );
    $err     = curl_errno( $ch );
    $errmsg  = curl_error( $ch );
    $header  = curl_getinfo( $ch );
    curl_close( $ch );

    $header['errno']   = $err;
    $header['errmsg']  = $errmsg;
    $header['content'] = $content;
    return $header;

print $content."\n";

?>
