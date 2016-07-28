def main ():
	nomFichier = "cockpit_Init.txt"
	gen (nomFichier)
	print("\nDone")

def gen (nomFichier):
	i=0
	dest = open("codeGen2.txt", 'w')
	f = open(nomFichier,'r')
	for line in f:
		dest.write("initSequence["+ str(i) +"] = \"" + line[:-1] + "\";\n")
		i+=1
	f.close()
	dest.close


main()