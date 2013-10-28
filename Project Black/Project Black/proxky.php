<?php

       if(isset($_REQUEST["url"]))
       {
               list($host, $restURI) = preg_split("/\//", $_REQUEST["url"], 2);
               if(!isset($host))
               {
                       header("HTTP/1.1 500 Internal Server Error");
                       exit();
               }
               if(!isset($restURI))
                       $restURI="";

               $fp = fsockopen($host, 80, $errno, $errstr, 30);
               if (!$fp)
                       header("HTTP/1.1 500 Internal Server Error");
               else
               {
                       $allHeadersPassed = false;
                       $non100ResponseOccurred=false;
                       $CRLF="\r\n";

                       $req = "GET /" . $restURI . " HTTP/1.1" . $CRLF;
                       $req .= 'Host: '. $host . $CRLF;
                       $req .= 'User-Agent: Mozilla/5.0 Firefox/3.6.12' . $CRLF;
                       $req .= 'Accept:
text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8' .
$CRLF;
                       $req .= 'Accept-Language: en-us,en;q=0.5' . $CRLF;
                       $req .= 'Accept-Encoding: deflate' . $CRLF;
                       $req .= 'Accept-Charset: ISO-8859-1,utf-8;q=0.7,*;q=0.7' . $CRLF;
                       $req .= "Connection: Close" . $CRLF . $CRLF;

                       fwrite($fp, $req);

                       while (!feof($fp))
                       {
                               $line = fgets($fp, 1024);

                               if(!$non100ResponseOccurred)
                               {
                                       list($http, $code, $msg) = preg_split("/ /", $line, 3);

                                       if(isset($http)&&($http=="HTTP/1.1"||$http=="HTTP/1.0"))
                                       {
                                               if(isset($code)&&$code>=200)
                                               {
                                                       $non100ResponseOccurred=true;
                                                       header($line);
                                               }
                                       }
                               }
                               else
                               {
                                       if($allHeadersPassed==true)
                                               echo $line;
                                       else
                                       {
                                               list($header) = preg_split("/:/", $line, 2);
                                               $header = strtolower($header);
                                               if(isset($header)&&($header=="content-length"||$header=="content-type"))
                                                       header($line);
                                       }

                                       if($line==$CRLF)
                                               $allHeadersPassed=true;
                               }
                       }
                       fclose($fp);
               }
       }
       else
               header("HTTP/1.1 500 Internal Server Error");
?>