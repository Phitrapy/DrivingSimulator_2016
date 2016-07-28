@echo off
echo:
echo: This command file is: \ISAWIN\COM\OEM.BAT
echo:
echo: 1- When called from the ISaGRAF group, no argument is passed
echo:    the MsDos work directory is then \isawin\com
echo:
echo: 2- When called from the ISaGRAF Program Manager,
echo:    it is run with one argument: the current project name
echo:    the MsDos work directory is the current project directory
echo:
echo: Enter the following commands in this file to run another
echo: "OEM.BAT" command file dedicated to the selected project:
echo:    cd \isawin\apl\%%1
echo:    \isawin\apl\%%1\oem.bat
echo:
PAUSE

