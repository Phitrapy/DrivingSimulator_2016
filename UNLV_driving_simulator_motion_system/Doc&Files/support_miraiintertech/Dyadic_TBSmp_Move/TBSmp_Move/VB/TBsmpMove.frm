VERSION 5.00
Begin VB.Form Form1 
   AutoRedraw      =   -1  'True
   Caption         =   "TBsample"
   ClientHeight    =   3360
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   7680
   LinkTopic       =   "Form1"
   ScaleHeight     =   3360
   ScaleWidth      =   7680
   StartUpPosition =   3  'Windows の既定値
   Begin VB.TextBox TxtMove 
      Alignment       =   1  '右揃え
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Index           =   1
      Left            =   4440
      TabIndex        =   10
      Text            =   "0"
      Top             =   1800
      Width           =   2175
   End
   Begin VB.CommandButton CmdGo 
      Caption         =   "Incremental Feed"
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
      Index           =   1
      Left            =   240
      TabIndex        =   9
      Top             =   1800
      Width           =   2175
   End
   Begin VB.Timer Timer1 
      Enabled         =   0   'False
      Left            =   0
      Top             =   2760
   End
   Begin VB.TextBox TxtMove 
      Alignment       =   1  '右揃え
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Index           =   0
      Left            =   4440
      TabIndex        =   4
      Text            =   "0"
      Top             =   1200
      Width           =   2175
   End
   Begin VB.CommandButton CmdHome 
      Caption         =   "Homing"
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
      Left            =   240
      TabIndex        =   3
      Top             =   360
      Width           =   2175
   End
   Begin VB.CommandButton CmdGo 
      Caption         =   "Absolute Positioning"
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
      Index           =   0
      Left            =   240
      TabIndex        =   0
      Top             =   1200
      Width           =   2175
   End
   Begin VB.Label LblMove 
      BackColor       =   &H8000000D&
      Caption         =   "  Distance to go"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H8000000E&
      Height          =   375
      Index           =   1
      Left            =   2640
      TabIndex        =   12
      Top             =   1800
      Width           =   1815
   End
   Begin VB.Label Label4 
      Caption         =   "[count]"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   1
      Left            =   6840
      TabIndex        =   11
      Top             =   1920
      Width           =   735
   End
   Begin VB.Shape ShapeHomeON 
      BackColor       =   &H0080FF80&
      BackStyle       =   1  '不透明
      Height          =   255
      Left            =   5400
      Shape           =   3  '円
      Top             =   360
      Width           =   255
   End
   Begin VB.Shape ShapeHomeOFF 
      BackColor       =   &H00008000&
      BackStyle       =   1  '不透明
      Height          =   255
      Left            =   5400
      Shape           =   3  '円
      Top             =   360
      Width           =   255
   End
   Begin VB.Label LblHome 
      BackColor       =   &H8000000D&
      Caption         =   "   Homing is completed"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H8000000E&
      Height          =   375
      Left            =   2640
      TabIndex        =   8
      Top             =   360
      Width           =   3015
   End
   Begin VB.Label Label5 
      Caption         =   "[count]"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   6720
      TabIndex        =   7
      Top             =   2880
      Width           =   735
   End
   Begin VB.Label Label4 
      Caption         =   "[count]"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   0
      Left            =   6720
      TabIndex        =   6
      Top             =   1320
      Width           =   735
   End
   Begin VB.Label LblPosData 
      Alignment       =   1  '右揃え
      BackColor       =   &H80000005&
      BorderStyle     =   1  '実線
      Caption         =   "0"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   4440
      TabIndex        =   5
      Top             =   2760
      Width           =   2175
   End
   Begin VB.Label LblPosition 
      BackColor       =   &H8000000D&
      Caption         =   "   Actual Position"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H8000000E&
      Height          =   375
      Left            =   2640
      TabIndex        =   2
      Top             =   2760
      Width           =   1815
   End
   Begin VB.Label LblMove 
      BackColor       =   &H8000000D&
      Caption         =   "  Position to go"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H8000000E&
      Height          =   375
      Index           =   0
      Left            =   2640
      TabIndex        =   1
      Top             =   1200
      Width           =   1815
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Const AxisNo As Long = 0  'Axis Number
Const ZRTCode As Long = 7 'Kind of homeing（7 = With reverse side stopper）
Const PosRdAddr As Long = &H7400 'The address of current position
Private HomingRequest As Boolean
Private PositioningRequest As Boolean
Private FeedRequest As Boolean

Private Sub CmdGo_Click(Index As Integer)
    
    If Index = 0 Then
        PositioningRequest = True
    Else
        FeedRequest = True
    End If

End Sub

Private Sub CmdHome_Click()
    HomingRequest = True
End Sub

Private Sub Form_Load()
    Dim i As Long
    Dim axes_info(16) As Long
    
    For i = 0 To 16
        axes_info(i) = -1
    Next i
    
    axes_info(AxisNo) = 0    'AxisNo is used
    
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
        ShapeHomeON.Visible = False
        ShapeHomeOFF.Visible = True
        CmdGo(0).Enabled = False
        Timer1.Interval = 50 'msec
        Timer1.Enabled = True
    Else
        MsgBox ("Termi-BUS isn't running!!!")
    End If
    
End Sub

Private Sub Form_Unload(Cancel As Integer)
    close_tmbs
End Sub

Private Sub Timer1_Timer()
    Dim RetVal As Long
    Dim position As Long
    Dim StatusByte As Byte
    
    If HomingRequest Then
        
        If move_org(AxisNo, ZRTCode) = SIO_DONE Then
            HomingRequest = False
        End If
        
    ElseIf PositioningRequest Then
        
        If move_abs(AxisNo, CLng(TxtMove(0).Text)) = SIO_DONE Then
            PositioningRequest = False
        End If
        
    ElseIf FeedRequest Then
        
        If move_inc(AxisNo, CLng(TxtMove(1).Text)) = SIO_DONE Then
            FeedRequest = False
        End If
        
    ElseIf check_status(AxisNo) = SIO_DONE Then
    
        If check_org(AxisNo) = SIO_DONE Then
            ShapeHomeON.Visible = True
            ShapeHomeOFF.Visible = False
            CmdGo(0).Enabled = True  'move_abs can execute when homing is completed
        Else
            ShapeHomeON.Visible = False
            ShapeHomeOFF.Visible = True
            CmdGo(0).Enabled = False
        End If
    
    End If
    
    If read_svmem(AxisNo, PosRdAddr, position) = SIO_DONE Then
        LblPosData.Caption = Str(position)
    End If

End Sub
