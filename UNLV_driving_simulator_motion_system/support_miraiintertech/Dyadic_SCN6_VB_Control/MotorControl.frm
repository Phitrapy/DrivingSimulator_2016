VERSION 5.00
Begin VB.Form frmMotorControl 
   AutoRedraw      =   -1  'True
   Caption         =   "Motor Command and Control using TMBSCOM.DLL Jack P. Callaghan April 2005"
   ClientHeight    =   5250
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   8850
   Icon            =   "MotorControl.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   5250
   ScaleWidth      =   8850
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame Frame4 
      Caption         =   "Change Speed + Acceleration"
      Height          =   2055
      Left            =   4440
      TabIndex        =   23
      Top             =   960
      Width           =   3855
      Begin VB.TextBox txtAccel1 
         Height          =   285
         Left            =   2400
         TabIndex        =   32
         Text            =   "Text1"
         Top             =   960
         Width           =   1095
      End
      Begin VB.TextBox txtSpeed1 
         Height          =   285
         Left            =   840
         TabIndex        =   31
         Text            =   "Text1"
         Top             =   960
         Width           =   1095
      End
      Begin VB.TextBox txtAccel0 
         Height          =   285
         Left            =   2400
         TabIndex        =   27
         Text            =   "Text1"
         Top             =   600
         Width           =   1095
      End
      Begin VB.TextBox txtSpeed0 
         Height          =   285
         Left            =   840
         TabIndex        =   26
         Text            =   "Text1"
         Top             =   600
         Width           =   1095
      End
      Begin VB.CommandButton cmdAccel 
         Caption         =   "Change Acceleration"
         Height          =   495
         Left            =   2040
         TabIndex        =   25
         Top             =   1440
         Width           =   1215
      End
      Begin VB.CommandButton cmdSpeed 
         Caption         =   "Change Speed"
         Height          =   495
         Left            =   600
         TabIndex        =   24
         Top             =   1440
         Width           =   1215
      End
      Begin VB.Label Label9 
         AutoSize        =   -1  'True
         Caption         =   "Axis1"
         Height          =   195
         Left            =   240
         TabIndex        =   33
         Top             =   1005
         Width           =   375
      End
      Begin VB.Label Label8 
         AutoSize        =   -1  'True
         Caption         =   "Axis0"
         Height          =   195
         Left            =   240
         TabIndex        =   30
         Top             =   645
         Width           =   375
      End
      Begin VB.Label Label7 
         AutoSize        =   -1  'True
         Caption         =   "Acceleration  m/s^2"
         Height          =   195
         Left            =   2280
         TabIndex        =   29
         Top             =   360
         Width           =   1425
      End
      Begin VB.Label Label6 
         AutoSize        =   -1  'True
         Caption         =   "Velocity  mm/s"
         Height          =   195
         Left            =   840
         TabIndex        =   28
         Top             =   360
         Width           =   1035
      End
   End
   Begin VB.Frame Frame3 
      Caption         =   "Collect Continuous Data"
      Height          =   1215
      Left            =   360
      TabIndex        =   16
      Top             =   3000
      Width           =   3735
      Begin VB.TextBox txtSampleRate 
         Height          =   285
         Left            =   1680
         TabIndex        =   19
         Text            =   "5"
         Top             =   360
         Width           =   975
      End
      Begin VB.CommandButton cmdStart 
         BackColor       =   &H0000C000&
         Caption         =   "Start Data Collection"
         Height          =   375
         Left            =   120
         Style           =   1  'Graphical
         TabIndex        =   18
         Top             =   720
         Width           =   1695
      End
      Begin VB.CommandButton cmdStop 
         BackColor       =   &H000000FF&
         Caption         =   "Stop Data Collection"
         Height          =   375
         Left            =   1920
         Style           =   1  'Graphical
         TabIndex        =   17
         Top             =   720
         Width           =   1695
      End
      Begin VB.Label Label2 
         AutoSize        =   -1  'True
         Caption         =   "Sample Rate (Hz)"
         Height          =   195
         Left            =   240
         TabIndex        =   20
         Top             =   360
         Width           =   1245
      End
   End
   Begin VB.Frame Frame2 
      Caption         =   "Absolute Movement"
      Height          =   2055
      Left            =   360
      TabIndex        =   12
      Top             =   840
      Width           =   2295
      Begin VB.CommandButton cmdGotoPosition 
         Caption         =   "Goto Position"
         Height          =   375
         Left            =   120
         TabIndex        =   14
         Top             =   1320
         Width           =   2055
      End
      Begin VB.TextBox txtTarget 
         Height          =   285
         Left            =   600
         TabIndex        =   13
         Text            =   "0"
         Top             =   960
         Width           =   1095
      End
      Begin VB.Label Label3 
         AutoSize        =   -1  'True
         Caption         =   "Enter Target in mm (max stroke is 150 mm)"
         Height          =   585
         Left            =   360
         TabIndex        =   15
         Top             =   480
         Width           =   1665
         WordWrap        =   -1  'True
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "Current Position"
      Height          =   1095
      Left            =   4440
      TabIndex        =   7
      Top             =   3240
      Width           =   3975
      Begin VB.TextBox txtPositionA1 
         Height          =   285
         Left            =   2160
         TabIndex        =   9
         Top             =   600
         Width           =   1335
      End
      Begin VB.TextBox txtPositionA0 
         Height          =   285
         Left            =   360
         TabIndex        =   8
         Top             =   600
         Width           =   1455
      End
      Begin VB.Label Label5 
         AutoSize        =   -1  'True
         Caption         =   "Axis 1 (mm)"
         Height          =   195
         Left            =   2160
         TabIndex        =   11
         Top             =   240
         Width           =   795
      End
      Begin VB.Label Label4 
         AutoSize        =   -1  'True
         Caption         =   "Axis 0 (mm)"
         Height          =   195
         Left            =   360
         TabIndex        =   10
         Top             =   240
         Width           =   795
      End
   End
   Begin VB.CommandButton cmdRead 
      Caption         =   "Read Current Position"
      Height          =   615
      Left            =   6600
      TabIndex        =   6
      Top             =   4560
      Width           =   1455
   End
   Begin VB.CommandButton cmdHome 
      Caption         =   "Home"
      Height          =   615
      Left            =   4800
      TabIndex        =   5
      Top             =   4560
      Width           =   1575
   End
   Begin VB.CommandButton cmdJogIn 
      Caption         =   "Jog In"
      Height          =   375
      Left            =   4200
      TabIndex        =   4
      Top             =   240
      Width           =   2175
   End
   Begin VB.CommandButton cmdJogOut 
      Caption         =   "Jog Out"
      Height          =   375
      Left            =   6480
      TabIndex        =   3
      Top             =   240
      Width           =   2175
   End
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
      ItemData        =   "MotorControl.frx":0442
      Left            =   1440
      List            =   "MotorControl.frx":047C
      TabIndex        =   1
      Text            =   "0"
      Top             =   240
      Width           =   612
   End
   Begin VB.CommandButton cmdGo 
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
   Begin VB.Label lblStatusFinished 
      AutoSize        =   -1  'True
      Caption         =   "Data Collection Finished"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   360
      TabIndex        =   22
      Top             =   4320
      Visible         =   0   'False
      Width           =   3090
   End
   Begin VB.Label lblStatusStart 
      AutoSize        =   -1  'True
      Caption         =   "Data Collection in Progress"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   360
      TabIndex        =   21
      Top             =   4320
      Visible         =   0   'False
      Width           =   3420
   End
   Begin VB.Label Label1 
      Alignment       =   1  'Right Justify
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
Attribute VB_Name = "frmMotorControl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub cmdAccel_Click()
    Dim RetVal As Long
    Dim dst0 As COMPACK
    Dim dst1 As COMPACK
    Dim i As Long
    
    For i = 0 To 31
        dst0.address(i) = -1
        dst0.data(i) = -1
        dst1.address(i) = -1
        dst1.data(i) = -1
    Next i
    For i = 2 To 2
        dst0.address(i) = 0
        dst0.data(i) = 0
        dst1.address(i) = 0
        dst1.data(i) = 0
    Next i
        
    'Write New Acceleration
        dst0.address(2) = &HF&
        dst0.data(2) = Val(txtAccel0.Text) / 9.81 * 2000
        dst1.address(2) = &HF&
        dst1.data(2) = Val(txtAccel1.Text) / 9.81 * 2000
        
       
        RetVal = write_param(0, dst0)
        RetVal = write_param(1, dst1)
        
        For i = 1 To 2
        dst0.address(i) = 0
        dst0.data(i) = 0
        dst1.address(i) = 0
        dst1.data(i) = 0
        Next i
        
        RetVal = load_param(0)
        RetVal = load_param(1)
        
        dst0.address(1) = &HE& 'Velocity
        dst0.address(2) = &HF& 'acceleration

        dst1.address(1) = &HE& 'Velocity
        dst1.address(2) = &HF& 'acceleration

    RetVal = read_param(0, dst0)
    RetVal = read_param(1, dst1)


        txtSpeed0.Text = dst0.data(1) / 100
        txtSpeed1.Text = dst1.data(1) / 100

        txtAccel0.Text = dst0.data(2) / 2000 * 9.81
        txtAccel1.Text = dst1.data(2) / 2000 * 9.81

End Sub

Private Sub cmdGotoPosition_Click()
    Dim RetVal As Long
    Dim Target As Long
    Dim bank1 As Long
    Dim bank2 As Long
    Dim dataloc As Long
    dataloc = &H7400&
    bank1 = &H29&
    bank2 = &H29&
    
    Target = Val(txtTarget.Text)
    Target = Target * 40000 / 150 * -1 'convert to mm)

    RetVal = move_abs(0, Target)
    RetVal = move_abs(1, Target)

    If CheckStop = 1 Then
    Else
        Do While bank1 <> Target
            RetVal = read_svmem(0, dataloc, bank1)
            RetVal = read_svmem(1, dataloc, bank2)
                txtPositionA0.Text = bank1 / 40000 * -150
                txtPositionA1.Text = bank2 / 40000 * -150
            DoEvents
        Loop
    End If

End Sub

Private Sub cmdHome_Click()
    Dim RetVal As Long
    
    RetVal = move_abs(0, 0)  ' Negative is Out +ve is in
    RetVal = move_abs(1, 0)
End Sub

Private Sub cmdJogOut_Click()
    Dim RetVal As Long

    RetVal = move_jog(0, -1000)  ' Negative is Out +ve is in
    RetVal = move_jog(1, -1000)

End Sub


Private Sub cmdJogIn_Click()
    Dim RetVal As Long
    RetVal = move_jog(0, 1000)  ' Negative is Out +ve is in
    RetVal = move_jog(1, 1000)

End Sub

Private Sub cmdRead_Click()
'Public Declare Function read_param Lib "TMBSCOM.DLL" Alias "_read_param@8" _
'    (ByVal axis As Long, _
'    dst As COMPACK) As Long
    
'   Type COMPACK
'    address(31) As Long
'    data(31) As Long
'    End Type
'
    
'    Dim dst As COMPACK
'    Dim i As Long
'    Dim j As Integer
    
'    For i = 0 To 31
'        dst.address(i) = 0
'        dst.data(i) = 0
'        'address(i) = 0
'        'data(i) = 0
'    Next i
'        'dst.address(29) = 0
'        'dst.data(1) = 0
'
'    RetVal = read_param(0, dst)

'Open "JackMotorOutRP.csv" For Output As #1
'For i = 0 To 31
'    For j = 0 To 31
'        Write #1, i, j, dst.address(i), dst.data(j)
'    Next j
'Next i
'Close #1


'This is the key to reading in the COMPACK or Memory Bank Data
'Both the Bank and Address must be in HEX to hand the location to the
'Motors/Amp.
    Dim RetVal As Long
    Dim crap As Long

Dim bank1 As Long
Dim bank2 As Long
bank1 = &H29&
bank2 = &H29&

Dim dataloc As Long
dataloc = &H7400&

RetVal = read_svmem(0, dataloc, bank1)
RetVal = read_svmem(1, dataloc, bank2)

txtPositionA0.Text = bank1 / 40000 * -150
txtPositionA1.Text = bank2 / 40000 * -150

'Open "JackMotorOutSV.csv" For Output As #1
'
''delay = 0.5
'For i = 0 To 31
'    For j = 0 To 31
'        bank = i
'        dataloc = j
'       'start = Timer
'        RetVal = read_svmem(0, dataloc, bank)
'        Write #1, i, j, bank, dataloc
'        'Do While Timer < start + delay
'        'Loop
'    Next j
'Next i
'Close #1
'
crap = bank1

'Dim param As Byte
'param = 240

'Public Declare Function get_status Lib "TMBSCOM.DLL" Alias "_get_status@8" _
'    (ByVal axis As Long, _
'    param As Byte) As Long

'RetVal = get_status(1, param)

'crap3 = param


End Sub

Private Sub cmdSpeed_Click()
    Dim RetVal As Long
    Dim dst0 As COMPACK
    Dim dst1 As COMPACK
    Dim i As Long
    
    For i = 0 To 31
        dst0.address(i) = -1
        dst0.data(i) = -1
        dst1.address(i) = -1
        dst1.data(i) = -1
    Next i
    For i = 1 To 1
        dst0.address(i) = 0
        dst0.data(i) = 0
        dst1.address(i) = 0
        dst1.data(i) = 0
    Next i
        
    'Write New velocity
        dst0.address(1) = &HE&
        dst0.data(1) = Val(txtSpeed0.Text) * 100
        dst1.address(1) = &HE&
        dst1.data(1) = Val(txtSpeed1.Text) * 100
        
       
        RetVal = write_param(0, dst0)
        RetVal = write_param(1, dst1)
        
        For i = 1 To 2
        dst0.address(i) = 0
        dst0.data(i) = 0
        dst1.address(i) = 0
        dst1.data(i) = 0
        Next i
        
        RetVal = load_param(0)
        RetVal = load_param(1)
        
        dst0.address(1) = &HE& 'Velocity
        dst0.address(2) = &HF& 'acceleration

        dst1.address(1) = &HE& 'Velocity
        dst1.address(2) = &HF& 'acceleration

        RetVal = read_param(0, dst0)
        RetVal = read_param(1, dst1)


        txtSpeed0.Text = dst0.data(1) / 100
        txtSpeed1.Text = dst1.data(1) / 100

        txtAccel0.Text = dst0.data(2) / 2000 * 9.81
        txtAccel1.Text = dst1.data(2) / 2000 * 9.81
        



End Sub

Private Sub cmdStart_Click()
    Dim start As Double
    Dim Start1 As Double
    Dim Samprate As Double
    Dim TimeSamp As Double
    Dim RetVal As Long
    Dim Target As Long
    Dim bank1 As Long
    Dim bank2 As Long
    Dim dataloc As Long
    dataloc = &H7400&
    bank1 = &H29&
    bank2 = &H29&
    
    CheckStop = 1
    Samprate = 1 / Val(txtSampleRate.Text)
    
    Open "DataStream.csv" For Output As #1
    Write #1, "Real Time", "Time (s)", "Axis0 (mm)", "Axis1 (mm)"
    lblStatusStart.Visible = True
    TimeSamp = 0
    start = Timer
    Start1 = start
    RetVal = read_svmem(0, dataloc, bank1)
    RetVal = read_svmem(1, dataloc, bank2)
    Write #1, start - Start1, TimeSamp, bank1 / 40000 * -150, bank2 / 40000 * -150
    
    Do While CheckStop = 1
        Do While Timer < start + Samprate
        Loop
        start = Timer
        RetVal = read_svmem(0, dataloc, bank1)
        RetVal = read_svmem(1, dataloc, bank2)
        TimeSamp = TimeSamp + Samprate
        Write #1, start - Start1, TimeSamp, bank1 / 40000 * -150, bank2 / 40000 * -150
        DoEvents
    Loop
    
    Close #1
    lblStatusStart.Visible = False
    lblStatusFinished.Visible = True
   

End Sub

Private Sub cmdStop_Click()
CheckStop = 2
End Sub

Private Sub CmdGo_Click()
    Dim RetVal As Long
    
    RetVal = move_point(0, Combo1(0).ListIndex)
    RetVal = move_point(1, Combo1(0).ListIndex)

End Sub

Private Sub Command1_Click()
End Sub

Private Sub Command2_Click()
End Sub

Private Sub Form_Load()
    Dim i As Long
    Dim axes_info(16) As Long
    
    For i = 0 To 16
        axes_info(i) = -1
    Next i
    
    axes_info(0) = 0    'Axis#0 is used
    axes_info(1) = 0    'Axis#1 is used
    
    Do While (init_tmbs_config("COM1", TMBS_BAUD_115200, 3, False, False, axes_info(0)) <> SIO_DONE)
    
        If get_tmbs_state = TMBS_INIT_ERROR Then
            frmMotorControl.Print "Initializing Axis 0 was failed!!!"
            DoEvents
            Exit Do
        End If
    Loop
    
    Do While (init_tmbs_config("COM1", TMBS_BAUD_115200, 3, False, False, axes_info(1)) <> SIO_DONE)
    
        If get_tmbs_state = TMBS_INIT_ERROR Then
            frmMotorControl.Print "Initializing Axis 1 was failed!!!"
            DoEvents
            Exit Do
        End If
    Loop
    
    If get_tmbs_state = TMBS_RUNNING Then
        MsgBox ("Termi-BUS is now running with 2 Motors Active")
    
        Dim RetVal As Long
        Dim dst0 As COMPACK
        Dim dst1 As COMPACK
    
        For i = 0 To 31
            dst0.address(i) = -1
            dst0.data(i) = -1
            dst1.address(i) = -1
            dst1.data(i) = -1
        Next i
        For i = 1 To 2
            dst0.address(i) = 0
            dst0.data(i) = 0
            dst0.address(1) = 0
            dst0.data(1) = 0
        Next i
            dst0.address(1) = &HE& 'Velocity
            dst0.address(2) = &HF& 'acceleration
    
            dst1.address(1) = &HE& 'Velocity
            dst1.address(2) = &HF& 'acceleration

        RetVal = read_param(0, dst0)
        RetVal = read_param(1, dst1)

        txtSpeed0.Text = dst0.data(1) / 100
        txtSpeed1.Text = dst1.data(1) / 100

        txtAccel0.Text = dst0.data(2) / 2000 * 9.81
        txtAccel1.Text = dst1.data(2) / 2000 * 9.81
        
        
    Else
        MsgBox ("Termi-BUS isn't running!!!")
    End If
    



End Sub

Private Sub Form_Unload(Cancel As Integer)
    close_tmbs
End Sub

