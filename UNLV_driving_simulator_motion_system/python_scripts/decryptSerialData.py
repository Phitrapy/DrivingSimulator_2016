#	BY Clement BEGNAUD - 07/25/2016
#	
#	This script reads a "SerialPortMonitor Dump View record"
#	and builds a new file which contains only the serial requests sent.
#


def main():
	#inputName = "init_tbvst.txt";
	inputName = input("Name of the file to read : ")
	mode = 0
	mode = input("Mode number : (1: Write, 2: Read)")
	if (mode==1):
		print("Chosen mode : Write")
		mode = "Ecriture"
	if (mode==2):
		print("Chosen mode : Read")
		mode = "Lecture"
	print(type(mode))
	decrypt(inputName, mode)
	clean()
	
	print("Fin du programme")

def decrypt(inputName, mode):
	readIt = False
	dest = open("buffer.txt", 'w')
	f = open(inputName,'r')
	for line in f:
		request = ""
		if (readIt):
			if(line[0]=='[' or len(line)<54):
				readIT = False
				print("stop! ")
			else:
				if(line[54] == '.'):
					request = '\u0002' + line[55:-3] + '\u0003'
		if (line.find(mode) == 1):
			readIt = True;
		dest.write('\n' + request)
	f.close()
	dest.close()

def clean():
	dest = open("decryptSerialData.txt", 'w')
	f = open("buffer.txt", "r")
	lst = []
	for line in f:
		if(len(line)>2):
			lst.append(line)
	for line in lst:
		if (line[1]!='U'):
			dest.write(line)
	f.close()
	dest.close()

main()