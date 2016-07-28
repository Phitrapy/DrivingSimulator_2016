@echo off
echo:--------------------------------------------------------------
echo:       This command file is: \ISAWIN\COM\CLIBUBK.BAT
echo:                       1999 Dyadic Systems Co., Ltd.
echo:--------------------------------------------------------------
echo: ----  Restore Termi-BUS Libraries and C-Functions from A: -----
echo:
echo:
echo: ---> Please Insert Source Back Up DISK to Drive A:
PAUSE

copy A:\COM\*.*            \ISAWIN\COM
copy A:\TMBSCOM\*.*        \TMBSCOM
copy A:\TARGET\*.*         \ISAWIN\TARGET
copy A:\TARGET\USER\*.*    \ISAWIN\TARGET\USER
copy A:\TARGET\LIB\*.*     \ISAWIN\TARGET\LIB
copy A:\TARGET\CMDS\*.*    \ISAWIN\TARGET\CMDS
copy A:\TARGET\CMDS\isa16  \ISAWIN\TARGET\CMDS
copy A:\TARGET\CMDS\isa11  \ISAWIN\TARGET\CMDS

ECHO ON