@echo off
echo:--------------------------------------------------------------
echo:       This command file is: \ISAWIN\COM\TBUSTRG.BAT
echo:                       1999 Dyadic Systems Co., Ltd.
echo:--------------------------------------------------------------
echo:
echo: ----  Trnasfer Termi-BUS Libraries from \TMBSCOM to \ISaWIN\Target
echo: ----  This command is used for rebuilding New C-Functions' code
PAUSE

copy \TMBSCOM\TBUSVAR.C \ISAWIN\TARGET\USER
copy \TMBSCOM\TBUSVAR.H \ISAWIN\TARGET\USER
copy \TMBSCOM\TBUSTYP.H \ISAWIN\TARGET\USER
copy \TMBSCOM\TBUSSIO.H \ISAWIN\TARGET\USER
copy \TMBSCOM\TMBSCOM.LIB \ISAWIN\TARGET\LIB
copy \TMBSCOM\TMBSCOM.DLL \ISAWIN\TARGET\CMDS

