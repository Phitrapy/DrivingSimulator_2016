@ECHO OFF
cd \isawin\target\cmds
del isa11. /q
del isa16. /q 
copy \isawin\apl\tbdemo\appli.tst isa16
copy \isawin\apl\tbdemo\appli.x8m isa11
start WISaKER.EXE
exit
ECHO ON