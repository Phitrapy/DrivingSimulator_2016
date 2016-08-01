Attribute VB_Name = "TestMain"
Option Explicit

Public Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)

Public Const STANDARD_NRT As Long = 3
Public Const NOT_USED As Long = -1
Public EndFlag As Boolean

Public Sub Main()
    Dim i As Long
    Dim axes_info(16) As Long
    
    For i = 0 To 16
        axes_info(i) = NOT_USED
    Next i
    
    axes_info(0) = 0    'Axis#0 is used
    
    Do While (init_tmbs_config( _
                "COM1", _
                TMBS_BAUD_115200, _
                STANDARD_NRT, _
                False, _
                False, _
                axes_info(0) _
                ) <> SIO_DONE)
        Sleep (5)    'Time slice for another thread
    
        If get_tmbs_state = TMBS_INIT_ERROR Then
            Form1.Print "Initializing was failed!!!"
            DoEvents
            Exit Do
        End If
    Loop
    
    If get_tmbs_state = TMBS_RUNNING Then
        MsgBox ("Termi-BUS is now running")
    Else
        MsgBox ("Termi-BUS isn't running!!!")
    End If
    
    Do While (EndFlag = False)
        DoEvents
    Loop
    
End Sub
