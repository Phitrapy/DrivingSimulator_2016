.autodepend
CC=bcc32
RC=brc32
CFLAG=-WC  -3 -Od -w- -AT -pc -H- -k -b -v -y 
OUTDIR=-nDebug
CINCS=
TARGET=Debug\TBsample.exe
SRC1="TBsample.cpp"
OBJ1=Debug\TBsample.obj
LIB1="TMBSCOM.lib"

TARGET: $(TARGET)

$(TARGET): $(OBJ1)
    $(CC) $(CFLAG) -e$(TARGET) $(OBJ1) $(LIB1)
$(OBJ1): $(SRC1)
    $(CC) $(CFLAG) $(OUTDIR) $(CINCS) -c $(SRC1)
