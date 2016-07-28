@ECHO OFF
cd %3
del src\appli.c /q
del defs\applic.h /q
copy %2\apl\%1\appli.c src\
copy %2\apl\%1\appli.h defs\ 
if not exist src\appli.c goto error
if not exist defs\appli.h goto error

if not exist .\rels\nul md .\rels
nmake -fisaaplc.mak CFG="ISaAPLc - Win32 Release" -A -S
nmake -fwisakerc.mak CFG="WISaKERc - Win32 Release" -S 
pause
exit

error:
echo.
echo Can't find appli.c or appli.h, make the 
echo application with the 'C' compiler option
echo.
pause
exit