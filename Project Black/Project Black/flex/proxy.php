<?php
$url = (isset($_POST['url']) && $_POST['url']) ? $_POST['url'] : 'undefined';
 
if ($url !='undefined') echo file_get_contents($url);
?>