@ECHO OFF
cd %3\cmds
del isa11. /q
del isa16. /q 
copy %2\apl\%1\appli.tst isa16
copy %2\apl\%1\appli.x8m isa11
start WISaKER.EXE
exit
ECHO ON