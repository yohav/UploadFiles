<?php
/**
 * Created by PhpStorm.
 * User: Yoav
 * Date: 26/08/2016
 * Time: 10:43
 */

$file = $_FILES['testfile'];
$finfo = new finfo(FILEINFO_MIME_TYPE);
$mime = $finfo->file($file['tmp_name']);
echo "<p class='mime-type'>$mime</p>";