@echo off
echo:--------------------------------------------------------------
echo:       This command file is: \ISAWIN\COM\CLIBTRG.BAT
echo:                       1999 Dyadic Systems Co., Ltd.
echo:--------------------------------------------------------------
echo:
echo: ----  Trnasfer C-Function Libraries from \ISaWIN to \ISaWIN\Target
echo: ----  This command is used for rebuilding New C-Functions' code
PAUSE

copy \ISAWIN\LIB\SRC\GRUS*.C \ISAWIN\TARGET\USER
copy \ISAWIN\LIB\DEFS\GRUS*.H \ISAWIN\TARGET\USER

