@echo off
echo:--------------------------------------------------------------
echo:       This command file is: \ISAWIN\COM\CLIBBAK.BAT
echo:                       1999 Dyadic Systems Co., Ltd.
echo:--------------------------------------------------------------
echo: ----  Back UP Termi-BUS Libraries and C-Functions to A: -----
echo:
echo:
echo: ---> Please Insert Destination Back Up DISK to Drive A:
PAUSE

md A:\COM
copy \ISAWIN\COM\*.*                 A:\COM
md A:\TMBSCOM
copy \TMBSCOM\*.*                    A:\TMBSCOM
md A:\TARGET
copy \ISAWIN\TARGET\ISaUSP.DSW       A:\TARGET
copy \ISAWIN\TARGET\ISaUSP.DSP       A:\TARGET
md A:\TARGET\USER
copy \ISAWIN\TARGET\USER\GRUS0LIB.C  A:\TARGET\USER
copy \ISAWIN\TARGET\USER\GRUS*.C     A:\TARGET\USER
copy \ISAWIN\TARGET\USER\GRUS*.H     A:\TARGET\USER
copy \ISAWIN\TARGET\USER\TBUSVAR.C   A:\TARGET\USER
copy \ISAWIN\TARGET\USER\TBUSVAR.H   A:\TARGET\USER
copy \ISAWIN\TARGET\USER\TBUSTYP.H   A:\TARGET\USER
copy \ISAWIN\TARGET\USER\TBUSSIO.H   A:\TARGET\USER
md A:\TARGET\LIB
copy \ISAWIN\TARGET\LIB\ISaUSP.LIB   A:\TARGET\LIB
copy \ISAWIN\TARGET\LIB\TMBSCOM.LIB  A:\TARGET\LIB
md A:\TARGET\CMDS
copy \ISAWIN\TARGET\CMDS\TMBSCOM.DLL A:\TARGET\CMDS
copy \ISAWIN\TARGET\CMDS\ISaUSP.DLL  A:\TARGET\CMDS
copy \ISAWIN\TARGET\CMDS\isa16       A:\TARGET\CMDS
copy \ISAWIN\TARGET\CMDS\isa11       A:\TARGET\CMDS

ECHO ON