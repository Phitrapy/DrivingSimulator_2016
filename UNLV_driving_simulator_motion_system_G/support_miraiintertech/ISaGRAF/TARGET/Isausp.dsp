# Microsoft Developer Studio Project File - Name="ISaUSP" - Package Owner=<4>
# Microsoft Developer Studio Generated Build File, Format Version 5.00
# ** 編集しないでください **

# TARGTYPE "Win32 (x86) Dynamic-Link Library" 0x0102

CFG=ISaUSP - Win32 Release
!MESSAGE これは有効なﾒｲｸﾌｧｲﾙではありません。 このﾌﾟﾛｼﾞｪｸﾄをﾋﾞﾙﾄﾞするためには NMAKE を使用してください。
!MESSAGE [ﾒｲｸﾌｧｲﾙのｴｸｽﾎﾟｰﾄ] ｺﾏﾝﾄﾞを使用して実行してください
!MESSAGE 
!MESSAGE NMAKE /f "Isausp.mak".
!MESSAGE 
!MESSAGE NMAKE の実行時に構成を指定できます
!MESSAGE ｺﾏﾝﾄﾞ ﾗｲﾝ上でﾏｸﾛの設定を定義します。例:
!MESSAGE 
!MESSAGE NMAKE /f "Isausp.mak" CFG="ISaUSP - Win32 Release"
!MESSAGE 
!MESSAGE 選択可能なﾋﾞﾙﾄﾞ ﾓｰﾄﾞ:
!MESSAGE 
!MESSAGE "ISaUSP - Win32 Release" ("Win32 (x86) Dynamic-Link Library" 用)
!MESSAGE "ISaUSP - Win32 Debug" ("Win32 (x86) Dynamic-Link Library" 用)
!MESSAGE 

# Begin Project
# PROP Scc_ProjName ""
# PROP Scc_LocalPath ""
CPP=cl.exe
MTL=midl.exe
RSC=rc.exe

!IF  "$(CFG)" == "ISaUSP - Win32 Release"

# PROP BASE Use_MFC 0
# PROP BASE Use_Debug_Libraries 0
# PROP BASE Output_Dir ".\Release"
# PROP BASE Intermediate_Dir ".\Release"
# PROP BASE Target_Dir ""
# PROP Use_MFC 0
# PROP Use_Debug_Libraries 0
# PROP Output_Dir ".\Lib"
# PROP Intermediate_Dir ".\Rels"
# PROP Ignore_Export_Lib 0
# PROP Target_Dir ""
# ADD BASE CPP /nologo /MT /W3 /GX /O2 /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /YX /c
# ADD CPP /nologo /MT /W3 /GX /O2 /I ".\defs" /I ".\user" /D "NDEBUG" /D "WIN32" /D "_WINDOWS" /D "ISAWNT" /D "ISADLLEXP" /D "_WINDLL" /D "_MBCS" /YX /FD /c
# ADD BASE MTL /nologo /D "NDEBUG" /win32
# ADD MTL /nologo /D "NDEBUG" /mktyplib203 /win32
# ADD BASE RSC /l 0x40c /d "NDEBUG"
# ADD RSC /l 0x40c /d "NDEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /subsystem:windows /dll /machine:I386
# ADD LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib .\lib\tmbscom.lib /nologo /subsystem:windows /dll /machine:I386 /out:".\Cmds\ISaUSP.dll"
# SUBTRACT LINK32 /pdb:none

!ELSEIF  "$(CFG)" == "ISaUSP - Win32 Debug"

# PROP BASE Use_MFC 0
# PROP BASE Use_Debug_Libraries 1
# PROP BASE Output_Dir ".\Debug"
# PROP BASE Intermediate_Dir ".\Debug"
# PROP BASE Target_Dir ""
# PROP Use_MFC 0
# PROP Use_Debug_Libraries 1
# PROP Output_Dir ".\Lib"
# PROP Intermediate_Dir ".\Rels"
# PROP Ignore_Export_Lib 0
# PROP Target_Dir ""
# ADD BASE CPP /nologo /MTd /W3 /Gm /GX /Zi /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /YX /c
# ADD CPP /nologo /MTd /W3 /Gm /GX /Zi /Od /I ".\defs" /I ".\user" /D "_DEBUG" /D "WIN32" /D "_WINDOWS" /D "ISAWNT" /D "ISADLLEXP" /D "_WINDLL" /D "_MBCS" /YX /FD /c
# ADD BASE MTL /nologo /D "_DEBUG" /win32
# ADD MTL /nologo /D "_DEBUG" /mktyplib203 /win32
# ADD BASE RSC /l 0x40c /d "_DEBUG"
# ADD RSC /l 0x40c /d "_DEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LINK32=link.exe
# ADD BASE LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /subsystem:windows /dll /debug /machine:I386
# ADD LINK32 kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib .\lib\tmbscom.lib /nologo /subsystem:windows /dll /debug /machine:I386 /out:".\Cmds\ISaUSP.dll"
# SUBTRACT LINK32 /pdb:none

!ENDIF 

# Begin Target

# Name "ISaUSP - Win32 Release"
# Name "ISaUSP - Win32 Debug"
# Begin Group "Source Files"

# PROP Default_Filter "cpp;c;cxx;rc;def;r;odl;hpj;bat;for;f90"
# Begin Source File

SOURCE=.\USER\GRUS0001.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0002.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0003.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0004.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0005.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0006.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0007.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0008.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0009.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0010.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0011.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0012.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0013.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0014.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0015.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0016.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0017.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0018.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0019.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0020.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0021.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0022.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0023.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0024.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0025.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0026.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0027.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0028.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0029.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0030.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0031.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0032.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0033.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0034.c
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0035.c
# End Source File
# Begin Source File

SOURCE=.\User\GRUS0LIB.C
# End Source File
# Begin Source File

SOURCE=.\Lib\ISaSYS.lib
# End Source File
# Begin Source File

SOURCE=.\USER\TBUSVAR.C
# End Source File
# End Group
# Begin Group "Header Files"

# PROP Default_Filter "h;hpp;hxx;hm;inl;fi;fd"
# Begin Source File

SOURCE=.\USER\GRUS0001.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0002.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0003.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0004.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0005.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0006.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0007.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0008.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0009.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0010.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0011.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0012.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0013.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0014.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0015.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0016.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0017.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0018.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0019.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0020.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0021.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0022.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0023.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0024.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0025.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0026.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0027.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0028.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0029.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0030.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0031.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0032.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0033.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0034.h
# End Source File
# Begin Source File

SOURCE=.\USER\GRUS0035.h
# End Source File
# Begin Source File

SOURCE=.\USER\TBUSSIO.h
# End Source File
# Begin Source File

SOURCE=.\USER\TBUSTYP.H
# End Source File
# Begin Source File

SOURCE=.\USER\TBUSVAR.H
# End Source File
# End Group
# Begin Group "Resource Files"

# PROP Default_Filter "ico;cur;bmp;dlg;rc2;rct;bin;cnt;rtf;gif;jpg;jpeg;jpe"
# End Group
# End Target
# End Project
