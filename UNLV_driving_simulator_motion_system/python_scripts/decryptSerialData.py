def main():
	nomFichier = "init_tbvst.txt";
	#nomFichier = input("Nom du fichier à décrypter : ")
	mode = 0
	mode = input("Numéro du mode : (1: Ecriture, 2: Lecture)")
	type(mode)
	if (mode==1):
		print("Mode choisi : Ecriture")
		mode = "Ecriture"
	if (mode==2):
		print("Mode choisi : Lecture")
		mode = "Lecture"
	print(type(mode))
	decrypt(nomFichier, mode)
	clean()
	
	print("Fin du programme")

def decrypt(nomFichier, mode):
	readIt = False
	dest = open("buffer.txt", 'w')
	f = open(nomFichier,'r')
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
		#print(request)
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