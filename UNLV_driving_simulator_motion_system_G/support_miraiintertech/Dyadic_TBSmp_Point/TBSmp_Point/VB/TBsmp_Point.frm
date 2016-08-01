VERSION 5.00
Begin VB.Form Form1 
   AutoRedraw      =   -1  'True
   Caption         =   "TBsmp_Point"
   ClientHeight    =   2760
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   3960
   LinkTopic       =   "Form1"
   ScaleHeight     =   2760
   ScaleWidth      =   3960
   StartUpPosition =   3  'Windows の既定値
   Begin VB.TextBox tx1024 
      Alignment       =   1  '右揃え
      BeginProperty Font 
         Name            =   "Arial Unicode MS"
         Size            =   9
         Charset         =   129
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   300
      Index           =   1
      Left            =   1320
      TabIndex        =   8
      Text            =   "0"
      Top             =   2160
      Width           =   1275
   End
   Begin VB.TextBox tx1024 
      Alignment       =   1  '右揃え
      BeginProperty Font 
         Name            =   "Arial Unicode MS"
         Size            =   9
         Charset         =   129
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   300
      Index           =   0
      Left            =   1320
      TabIndex        =   5
      Text            =   "0"
      Top             =   1680
      Width           =   1275
   End
   Begin VB.CommandButton cmdSave 
      Caption         =   "Save"
      BeginProperty Font 
         Name            =   "Arial Unicode MS"
         Size            =   9.75
         Charset         =   129
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Index           =   0
      Left            =   2280
      TabIndex        =   4
      Top             =   1080
      Width           =   1425
   End
   Begin VB.CommandButton cmdRead 
      Caption         =   "Read"
      BeginProperty Font 
         Name            =   "Arial Unicode MS"
         Size            =   9
         Charset         =   128
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   240
      TabIndex        =   3
      Top             =   1080
      Width           =   1455
   End
   Begin VB.ComboBox Combo1 
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   360
      Index           =   0
      ItemData        =   "TBsmp_Point.frx":0000
      Left            =   1440
      List            =   "TBsmp_Point.frx":003A
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
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   372
      Left            =   2280
      TabIndex        =   0
      Top             =   240
      Width           =   1455
   End
   Begin VB.Label Label1024 
      Alignment       =   2  '中央揃え
      AutoSize        =   -1  'True
      BackColor       =   &H00FFFF80&
      Caption         =   "[0.2rpm]"
      BeginProperty Font 
         Name            =   "Arial Unicode MS"
         Size            =   9
         Charset         =   129
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   240
      Index           =   3
      Left            =   2520
      TabIndex        =   10
      Top             =   2160
      Width           =   1065
   End
   Begin VB.Label Label1024 
      Alignment       =   2  '中央揃え
      AutoSize        =   -1  'True
      BackColor       =   &H00FFFF80&
      Caption         =   "[count]"
      BeginProperty Font 
         Name            =   "Arial Unicode MS"
         Size            =   9
         Charset         =   129
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   240
      Index           =   2
      Left            =   2505
      TabIndex        =   9
      Top             =   1680
      Width           =   1080
   End
   Begin VB.Label Label1024 
      AutoSize        =   -1  'True
      BackColor       =   &H00FFFF80&
      Caption         =   "Velocity"
      BeginProperty Font 
         Name            =   "Arial Unicode MS"
         Size            =   9
         Charset         =   129
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   240
      Index           =   1
      Left            =   240
      TabIndex        =   7
      Top             =   2160
      Width           =   1125
   End
   Begin VB.Label Label1024 
      AutoSize        =   -1  'True
      BackColor       =   &H00FFFF80&
      Caption         =   "Position"
      BeginProperty Font 
         Name            =   "Arial Unicode MS"
         Size            =   9
         Charset         =   129
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   240
      Index           =   0
      Left            =   240
      TabIndex        =   6
      Top             =   1680
      Width           =   1140
   End
   Begin VB.Label Label1 
      Alignment       =   1  '右揃え
      Caption         =   "Point No."
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   210
      TabIndex        =   2
      Top             =   300
      Width           =   1155
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim PointData As COMPACK

Private Sub cmdRead_Click()
    Dim Result As Long
  
    Result = read_point(0, Combo1(0).ListIndex, PointData) ' Read into PointData
    tx1024(0).Text = PointData.data(0) ' Set Position
    tx1024(1).Text = PointData.data(4) ' Set Velocity
End Sub

Private Sub cmdSave_Click(Index As Integer)
    Dim Result As Long
  
    PointData.data(0) = tx1024(0).Text ' Reflect modified Position into PointData
    PointData.data(4) = tx1024(1).Text ' Reflect modified Velocity into PointData
    Result = write_point(0, Combo1(0).ListIndex, PointData) ' Write from PointData
End Sub

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
                "COM2", _
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
    
    For i = 0 To 31  '-- Initialize address of COMPACK structure --
        PointData.address(i) = i
    Next i
    
    Combo1(0).ListIndex = 0
    cmdRead_Click '-- Read Point Data --
End Sub

Private Sub Form_Unload(Cancel As Integer)
    close_tmbs
End Sub

