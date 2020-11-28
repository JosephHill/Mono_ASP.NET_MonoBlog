setlocal
set TARGETPATH="c:\temp\BlogX\%1"
md %TARGETPATH%

xcopy "C:\Documents and Settings\Chris\My Documents\Visual Studio Projects\BlogX" %TARGETPATH% /fredikys
xcopy "C:\InetPub\wwwroot\weblogx" %TARGETPATH%\WeblogX /fredikys
del /q %targetpath%\weblogx\SiteConfig\*.*
xcopy /y %targetpath%\weblogx\DefaultSiteConfig\*.* %targetpath%\weblogx\SiteConfig\*.* 
del /q %targetpath%\weblogx\DefaultSiteConfig\*.*
del %targetpath%\weblogx\content\*.xml
del %targetpath%\weblogx\logs\*.log
del %targetpath%\*.pdb /s

endlocal