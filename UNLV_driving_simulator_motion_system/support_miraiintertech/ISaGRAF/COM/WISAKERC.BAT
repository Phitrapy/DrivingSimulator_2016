@ECHO OFF
cd %3\cmds
del isa16. /q
copy %2\apl\%1\appli.tst isa16
start WISaKERc.EXE
exit
ECHO ON