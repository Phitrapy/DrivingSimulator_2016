VERSION 5.00
Begin VB.Form Form1 
   AutoRedraw      =   -1  'True
   Caption         =   "TBsample"
   ClientHeight    =   945
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4290
   LinkTopic       =   "Form1"
   ScaleHeight     =   945
   ScaleWidth      =   4290
   StartUpPosition =   3  'Windows ‚ÌŠù’è’l
   Begin VB.ComboBox Combo1 
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   336
      Index           =   0
      ItemData        =   "TBsample.frx":0000
      Left            =   1440
      List            =   "TBsample.frx":003A
      TabIndex        =   1
      Text            =   "0"
      Top             =   240
      Width           =   612
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Go"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   372
      Left            =   2280
      TabIndex        =   0
      Top             =   240
      Width           =   1212
   End
   Begin VB.Label Label1 
      Alignment       =   1  '‰E‘µ‚¦
      Caption         =   "Point No."
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   252
      Left            =   480
      TabIndex        =   2
      Top             =   360
      Width           =   852
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub Command1_Click()
    Dim RetVal As Long
    
    RetVal = move_point(0, Combo1(0).ListIndex)
End Sub

Private Sub Form_Load()
    Dim i As Long
    Dim axes_info(16) As Long
    
    For i = 0 To 16
        axes_info(i) = -1
    Next i
    
    axes_info(0) = 0    'Axis#0 is used
    
    Do While (init_tmbs_config( _
                "COM1", _
                TMBS_BAUD_115200, _
                3, _
                False, _
                False, _
                axes_info(0) _
                ) <> SIO_DONE)
    
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
    
End Sub

Private Sub Form_Unload(Cancel As Integer)
    close_tmbs
End Sub
