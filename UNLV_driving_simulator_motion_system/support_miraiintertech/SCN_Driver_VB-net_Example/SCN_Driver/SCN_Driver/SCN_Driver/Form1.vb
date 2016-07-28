Public Class Form1
    Dim SCN1 As SCN_Driver

    Private Sub Form1_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SCN1.ClosePort()
    End Sub

    Dim Center As Integer
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        SCN1 = New SCN_Driver
        Dim IsOpen As Integer = SCN1.OpenPort("COM5")

        If IsOpen = SCN_Driver.TMBS_RUNNING Then
            MsgBox("Termi-BUS is now running")
        Else
            MsgBox("Termi-BUS isn't running!!!")
        End If

        Center = SCN1.Home()

        'set velocity of the actuator (actuator, 20000 max, 1000 max)
        SCN1.WriteVelocity(0, 10000, 500)

        'lets wiggle the actuator
        SCN1.MoveInc(0, 100)
        SCN1.MoveInc(0, -100)
        SCN1.MoveInc(0, 100)
        SCN1.MoveInc(0, -100)
        SCN1.MoveInc(0, 100)
        SCN1.MoveInc(0, -100)
        SCN1.MoveInc(0, 100)
        SCN1.MoveInc(0, -100)

        'close the actuator
        Dim closed As Integer = SCN1.ClosePort()

        'Now lets re-open the port and wiggle the actuator.
        SCN1 = New SCN_Driver
        SCN1.OpenPort("COM5")
        SCN1.MoveInc(0, 100)
        SCN1.MoveInc(0, -100)
        SCN1.MoveInc(0, 100)
        SCN1.MoveInc(0, -100)
        SCN1.MoveInc(0, 100)
        SCN1.MoveInc(0, -100)
        SCN1.MoveInc(0, 100)
        SCN1.MoveInc(0, -100)
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'move to one end of actuator
        SCN1.MoveAbs(0, 0)
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        'move to other end of actuator
        SCN1.MoveAbs(0, Center * 2)
    End Sub
End Class

