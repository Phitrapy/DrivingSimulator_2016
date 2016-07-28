def main ():
	nomFichier = "cockpit_Init.txt"
	gen (nomFichier)
	print("\nDone")

def gen (nomFichier):
	dest = open("codeGen1.txt", 'w')
	f = open(nomFichier,'r')
	for line in f:
		dest.write("SendSerialData(\"" + line[:-1] + "\");\n")
		dest.write("System.Threading.Thread.Sleep(40);\n")
	f.close()
	dest.close


main()