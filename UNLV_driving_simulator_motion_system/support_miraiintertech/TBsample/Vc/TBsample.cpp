//---------------------------------------------------------------
//
//    TMBSCOM.DLL Sample Application Main Module
//     File Name: TBsampl2.cpp
//
//     Copyright(c) 2002 Dyadic Systems Co.,Ltd.
//
// This code is a simple console program that uses the 
// function of TMBSCOM.DLL
//
//----------------------------------------------------------------
#include <windows.h>
#include <tchar.h>
#include <iostream.h>
#include <stdlib.h>
#include <stdio.h>
#include <conio.h>
#include <string.h>
#include "tbussio.h"

#define STANDARD_NRT 3
#define NOT_USED -1

void main( void )
{
	int i;
	int axes_info[16];
	char c[8];
	
	for ( i = 0; i < 16; i++ )
		axes_info[ i ] = NOT_USED;
		
	axes_info[0] = 0; // Axis#0 is used
	
	cout << "Start Termi-BUS Initializing" << endl;
	
	while ( init_tmbs_config(
		                    _T("COM1"),
		                    TMBS_BAUD_115200,
							STANDARD_NRT,
							FALSE, // reset
							FALSE, // automatic
							axes_info
	                        ) != SIO_DONE )
	{
		Sleep( 5 );   // Time slice for another thread
		
		if ( get_tmbs_state() == TMBS_INIT_ERROR )
		{
			cout << "Initializing was failed!!!" << endl;
			break;
		}
	}
	
	if ( get_tmbs_state() == TMBS_RUNNING )
	{
		cout << "Termi-BUS is now running" << endl;
		
		while ( TRUE )
		{

			cout << "Input PointNo. to move, or E to exit." << endl;
			gets( c );
			
			if ( ( c[0] == 'E' ) || ( c[0] == 'e' ) )
				break;
			else
			{
				i = atoi( c );
				
				if ( move_point( 0, i ) == SIO_DONE ) // Move to point
				{
					while ( !check_pfin( 0 ) ) // Check InPosition
						check_status( 0 );
						
					cout << "The moving was completed." << endl;
				}
				else
					cout << "The command was failed!!!" << endl;
			}
		}
	}
	else
		cout << "Termi-BUS isn't running!!!" << endl;

	cout << "Hit any key to Exit" << endl;
	getchar();
	close_tmbs();
}
