def main ():
	nomFichier = "cockpit_Init.txt"
	gen (nomFichier)
	print("\nDone")

def gen (nomFichier):
	i=0
	prev = ""
	dest = open("codeGen2(noDouble).txt", 'w')
	f = open(nomFichier,'r')
	for line in f:
		if (line != prev):
			dest.write("initSequence["+ str(i) +"] = \"" + line[:-1] + "\";\n")
			i+=1
			prev = line
	f.close()
	dest.close


main()