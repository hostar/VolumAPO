## VolumAPO

TODO:  
add volumeswitcher project  
~~wire trackbar events to volumeswitcher lib~~  
~~add switching of default playback device~~  
~~add switching of default capture device~~  
~~add showing of current volume on tray icon~~  
~~add OSD for volume~~  
~~add OSD for balance~~  
add option to right click menu to open Volume mixer and Playback devices  
play "ding" when clicked on volume trackbar  

playback devices cmd: "C:\WINDOWS\system32\rundll32.exe" shell32.dll,Control_RunDLL mmsys.cpl,,playback
volume mixer cmd: sndvol.exe

add hotkey settings for:
 - ~~volume change~~
 - ~~balance change~~
 - mute
  
add settings to:
 - ~~enable changing volume by scroll wheel on taskbar~~
 - ~~change by how many points volume moves when changed by one step~~
 - ~~make it possible to set maximum volume~~
 - ~~show or hide OSD when volume is being changed~~ 
 - change behaviour of double click on notification icon

bugs:  
OSD thingy can move outside rectangle  

investigate tooltip on trackbar - no  