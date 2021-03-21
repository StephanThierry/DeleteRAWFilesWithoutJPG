# DeleteRAWFilesWithoutJPG
A program to delete all RAW files on your SDCARD that don't have a corrosponding JPG file  

FIRST - copy ALL your RAW files into a subfolder. If both folders are "the same" - it will not work, since the program actually does NOT look at the fileextention. It ONLY looks at matches in the filename!  

In `settings.json` set `jpgFolder` to the path that contains your jpegs and `rawFolder` to the folder that contains your RAW files.
```
{
 "jpgFolder":"J:\\DCIM\\100CANON",
 "rawFolder":"J:\\DCIM\\100CANON\\RAW"
}
```  


