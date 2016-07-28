Option Strict On
Option Explicit On
Imports System.Runtime.InteropServices
Imports System.Threading

Public Class SCN_Driver
    Public Shared newPacket As COMPACK
    Public axes_info As Integer()
    Private hModule As Integer = 0

    <DllImport("kernel32.dll", EntryPoint:="LoadLibrary")> _
    Private Shared Function LoadLibrary(<MarshalAs(UnmanagedType.LPStr)> lpLibFileName As String) As Integer
    End Function

    <DllImport("kernel32.dll", EntryPoint:="GetProcAddress")> _
    Private Shared Function GetProcAddress(hModule As Integer, <MarshalAs(UnmanagedType.LPStr)> lpProcName As String) As IntPtr
    End Function

    <DllImport("kernel32.dll", EntryPoint:="FreeLibrary")> _
    Private Shared Function FreeLibrary(hModule As Integer) As Boolean
    End Function

    Public Structure COMPACK
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=31)> _
        Public address As Long()
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=31)> _
        Public data As Long()
        Public Sub initialize()
            address = New Long(30) {}
            data = New Long(30) {}

            For i As Integer = 0 To address.Length - 1
                address(i) = i
            Next
        End Sub
    End Structure

    Public Sub New()
        hModule = LoadLibrary("TMBSCOM.dll")
        init_tmbs = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_init_tmbs@0"), GetType(DLL_INIT_TMBS)), DLL_INIT_TMBS)
        close_tmbs = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_close_tmbs@0"), GetType(DLL_CLOSE_TMBS)), DLL_CLOSE_TMBS)
        move_point = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_move_point@8"), GetType(DLL_MOVE_POINT)), DLL_MOVE_POINT)
        get_tmbs_state = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_get_tmbs_state@0"), GetType(DLL_GET_TMBS_STATE)), DLL_GET_TMBS_STATE)
        init_tmbs_config = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_init_tmbs_config@24"), GetType(DLL_INIT_TMBS_CONFIG)), DLL_INIT_TMBS_CONFIG)
        set_son = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_set_son@4"), GetType(DLL_SET_SON)), DLL_SET_SON)
        set_soff = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_set_soff@4"), GetType(DLL_SET_SOFF)), DLL_SET_SOFF)
        move_org = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_move_org@8"), GetType(DLL_MOVE_ORG)), DLL_MOVE_ORG)
        move_abs = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_move_abs@8"), GetType(DLL_MOVE_ABS)), DLL_MOVE_ABS)
        move_jog = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_move_jog@8"), GetType(DLL_MOVE_JOG)), DLL_MOVE_JOG)
        move_inc = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_move_inc@8"), GetType(DLL_MOVE_INC)), DLL_MOVE_INC)
        move_rotate = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_move_rotate@16"), GetType(DLL_MOVE_ROTATE)), DLL_MOVE_ROTATE)
        write_velocity = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_write_velocity@12"), GetType(DLL_WRITE_VELOCITY)), DLL_WRITE_VELOCITY)
        select_svparm = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_select_svparm@12"), GetType(DLL_SELECT_SVPARM)), DLL_SELECT_SVPARM)
        write_trqlim = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_write_trqlim@12"), GetType(DLL_WRITE_TRQLIM)), DLL_WRITE_TRQLIM)
        write_inpos = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_write_inpos@8"), GetType(DLL_WRITE_INPOS)), DLL_WRITE_INPOS)
        write_point = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_write_point@12"), GetType(DLL_WRITE_POINT)), DLL_WRITE_POINT)
        read_point = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_read_point@12"), GetType(DLL_READ_POINT)), DLL_READ_POINT)
        load_param = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_load_param@4"), GetType(DLL_LOAD_PARAM)), DLL_LOAD_PARAM)
        write_param = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_write_param@8"), GetType(DLL_WRITE_PARAM)), DLL_WRITE_PARAM)
        read_svmem = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_read_svmem@12"), GetType(DLL_READ_SVMEM)), DLL_READ_SVMEM)
        write_svmem = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_write_svmem@12"), GetType(DLL_WRITE_SVMEM)), DLL_WRITE_SVMEM)
        get_current_baud = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_get_current_baud@0"), GetType(DLL_GET_CURRENT_BAUD)), DLL_GET_CURRENT_BAUD)
        get_sio_error = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_get_sio_error@0"), GetType(DLL_GET_SIO_ERROR)), DLL_GET_SIO_ERROR)
        check_pfin = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_check_pfin@4"), GetType(DLL_CHECK_PFIN)), DLL_CHECK_PFIN)
        check_status = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_check_status@4"), GetType(DLL_CHECK_STATUS)), DLL_CHECK_STATUS)
        check_alrm = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_check_alrm@4"), GetType(DLL_CHECK_ALRM)), DLL_CHECK_ALRM)
        reset_alarm = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_reset_alarm@4"), GetType(DLL_RESET_ALARM)), DLL_RESET_ALARM)
        read_param = DirectCast(Marshal.GetDelegateForFunctionPointer(GetProcAddress(hModule, "_read_param@8"), GetType(DLL_READ_PARAM)), DLL_READ_PARAM)
        axes_info = New Integer(15) {}
        newPacket.initialize()
    End Sub

    <UnmanagedFunctionPointer(CallingConvention.Winapi)> _
    Public Delegate Function DLL_MOVE_POINT(axis As Integer, point As Integer) As Integer
    Private move_point As DLL_MOVE_POINT

    Public Delegate Function DLL_INIT_TMBS_CONFIG(port As String, baud As Integer, nrt As Integer, reset As Boolean, automatic As Boolean, ByRef axes_info As Integer) As Integer
    Private init_tmbs_config As DLL_INIT_TMBS_CONFIG

    Public Delegate Function DLL_CLOSE_TMBS() As Integer
    Private close_tmbs As DLL_CLOSE_TMBS

    Public Delegate Function DLL_GET_CURRENT_BAUD() As Integer
    Private get_current_baud As DLL_GET_CURRENT_BAUD

    Public Delegate Function DLL_INIT_TMBS() As Integer
    Private init_tmbs As DLL_INIT_TMBS

    Public Delegate Function DLL_GET_TMBS_STATE() As Integer
    Private get_tmbs_state As DLL_GET_TMBS_STATE

    Public Delegate Function DLL_GET_SIO_ERROR() As Integer
    Private get_sio_error As DLL_GET_SIO_ERROR

    Public Delegate Function DLL_CHECK_PFIN(axis As Integer) As Integer
    Private check_pfin As DLL_CHECK_PFIN

    Public Delegate Function DLL_CHECK_STATUS(axis As Integer) As Integer
    Private check_status As DLL_CHECK_STATUS

    Public Delegate Function DLL_CHECK_ALRM(axis As Integer) As Integer
    Private check_alrm As DLL_CHECK_ALRM

    Public Delegate Function DLL_RESET_ALARM(axis As Integer) As Integer
    Private reset_alarm As DLL_RESET_ALARM

    Public Delegate Function DLL_READ_PARAM(axis As Integer, ByRef dst As COMPACK) As Integer
    Private read_param As DLL_READ_PARAM

    Public Delegate Function DLL_SET_SON(axis As Integer) As Integer
    Private set_son As DLL_SET_SON

    Public Delegate Function DLL_SET_SOFF(axis As Integer) As Integer
    Private set_soff As DLL_SET_SOFF

    Public Delegate Function DLL_MOVE_ORG(axis As Integer, position As Integer) As Integer
    Private move_org As DLL_MOVE_ORG

    Public Delegate Function DLL_MOVE_ABS(axis As Integer, position As Integer) As Integer
    Private move_abs As DLL_MOVE_ABS

    Public Delegate Function DLL_MOVE_JOG(axis As Integer, distance As Integer) As Integer
    Private move_jog As DLL_MOVE_JOG

    Public Delegate Function DLL_MOVE_INC(axis As Integer, distance As Integer) As Integer
    Private move_inc As DLL_MOVE_INC

    Public Delegate Function DLL_MOVE_ROTATE(axis As Integer, dir As Integer, speed As Integer, accel As Integer) As Integer
    Private move_rotate As DLL_MOVE_ROTATE

    Public Delegate Function DLL_WRITE_VELOCITY(axis As Integer, vcmd As Integer, acmd As Integer) As Integer
    Private write_velocity As DLL_WRITE_VELOCITY

    Public Delegate Function DLL_SELECT_SVPARM(axis As Integer, gain_sel As Integer, svparm As Integer) As Integer
    Private select_svparm As DLL_SELECT_SVPARM

    Public Delegate Function DLL_WRITE_TRQLIM(axis As Integer, atstop As Integer, atmovement As Integer) As Integer
    Private write_trqlim As DLL_WRITE_TRQLIM

    Public Delegate Function DLL_WRITE_INPOS(axis As Integer, width As Integer) As Integer
    Private write_inpos As DLL_WRITE_INPOS

    Public Delegate Function DLL_WRITE_POINT(axis As Integer, point As Integer, ByRef src As COMPACK) As Integer
    Private write_point As DLL_WRITE_POINT

    Public Delegate Function DLL_READ_POINT(axis As Integer, point As Integer, ByRef dst As COMPACK) As Integer
    Private read_point As DLL_READ_POINT

    Public Delegate Function DLL_LOAD_PARAM(axis As Integer) As Integer
    Private load_param As DLL_LOAD_PARAM

    Public Delegate Function DLL_WRITE_PARAM(axis As Integer, ByRef src As COMPACK) As Integer
    Private write_param As DLL_WRITE_PARAM

    Public Delegate Function DLL_READ_SVMEM(axis As Integer, address As Integer, ByRef dst As Integer) As Integer
    Private read_svmem As DLL_READ_SVMEM

    Public Delegate Function DLL_WRITE_SVMEM(axis As Integer, address As Integer, ByRef dst As Integer) As Integer
    Private write_svmem As DLL_WRITE_SVMEM

    Public Function OpenPort(port As String) As Integer
        Dim i As Integer = 0

        For i = 0 To 15
            axes_info(i) = -1
        Next

        axes_info(0) = 0 ' Axis#0 is used

        While init_tmbs_config(port, TMBS_BAUD_115200, 3, False, False, axes_info(0)) <> 1
            If get_tmbs_state() = TMBS_INIT_ERROR Then
                Exit While
            End If
        End While

        Return get_tmbs_state()
    End Function

    'Returns the center location - axis movements are from 0 to -X
    Public Function Home() As Integer
        'Set random Home Position
        move_org(0, 0)
        System.Threading.Thread.Sleep(50)

        'Read Current position
        Dim old As Integer = Read_Position()

        'Home to end of axis
        move_org(0, 6)
        System.Threading.Thread.Sleep(50)

        'Look for movement stoped
        While 1 = 1
            System.Threading.Thread.Sleep(50)
            If Read_Position() = old Then
                System.Threading.Thread.Sleep(250)
                Exit While
            End If
            old = Read_Position()
        End While

        'Set random Home Position
        move_org(0, 0)
        System.Threading.Thread.Sleep(50)

        'Read Current position
        old = Read_Position()

        'Home to other end of axis
        move_org(0, 5)
        System.Threading.Thread.Sleep(50)

        'Look for movement stoped
        Dim top As Integer = 0
        While 1 = 1
            System.Threading.Thread.Sleep(50)
            'get max used value
            If old > top Then
                top = old
            End If
            If Read_Position() = old Then
                System.Threading.Thread.Sleep(250)
                Exit While
            End If
            old = Read_Position()
        End While

        'make sure max used value
        If Read_Position() > top Then
            top = Read_Position()
        End If

        'Set Center - VB stupid rounding fix
        Dim MySplit As String = CStr(top * -0.5)
        Dim WholeNumber As String() = MySplit.Split(CChar("."))
        Dim Center_SCN1 As Integer = CInt(WholeNumber(0))

        'set real home position
        move_org(0, 0)
        System.Threading.Thread.Sleep(50)

        'set velocity 1/2 speed
        write_velocity(0, 10000, 500)

        'Move to center
        move_abs(0, Center_SCN1)

        'pause to let axis center
        System.Threading.Thread.Sleep(1500)

        'return center value
        Return Center_SCN1
    End Function

    Private Function Read_Position() As Integer
        Dim value As Integer
        read_svmem(0, &H7400, value)
        Return value
    End Function

    Public Function ClosePort() As Integer
        Dim result As Integer = 0
        If hModule <> 0 Then
            result = close_tmbs()
            FreeLibrary(hModule)
            hModule = 0
        End If
        Return result
    End Function

    Public Function Get_CurrentBaud() As Integer
        Return get_current_baud()
    End Function

    Public Function GetState() As Integer
        Return get_tmbs_state()
    End Function

    Public Function GetSIOError() As Integer
        Return get_sio_error()
    End Function

    Public Function CheckPfin(axis As Integer) As Integer
        Return check_pfin(axis)
    End Function

    Public Function CheckStatus(axis As Integer) As Integer
        Return check_status(axis)
    End Function

    Public Function Check_Alarm(axis As Integer) As Integer
        Return check_alrm(axis)
    End Function

    Public Function ResetAlarm(axis As Integer) As Integer
        Return reset_alarm(axis)
    End Function

    Public Function ServoOn(axis As Integer) As Integer
        Return set_son(axis)
    End Function

    Public Function ServoOff(axis As Integer) As Integer
        Return set_soff(axis)
    End Function

    Public Function MovePoint(axis As Integer, point As Integer) As Integer
        Return move_point(axis, point)
    End Function

    Public Function MoveAbs(axis As Integer, position As Integer) As Integer
        Return move_abs(axis, position)
    End Function

    Public Function MoveInc(axis As Integer, distance As Integer) As Integer
        Return move_inc(axis, distance)
    End Function

    Public Function MoveORG(axis As Integer, mode As Integer) As Integer
        Return move_org(axis, mode)
    End Function

    Public Function MoveRotate(axis As Integer, dir As Integer, velocidad As Integer, aceleracion As Integer) As Integer
        Return move_rotate(axis, dir, velocidad, aceleracion)
    End Function

    Public Function MoveJOG(axis As Integer, distance As Integer) As Integer
        Return move_jog(axis, distance)
    End Function

    Public Function WriteVelocity(axis As Integer, velocidad As Integer, aceleracion As Integer) As Integer
        Return write_velocity(axis, velocidad, aceleracion)
    End Function

    Public Function WriteGainServo(axis As Integer, gain_sel As Integer, svparm As Integer) As Integer
        Return select_svparm(axis, gain_sel, svparm)
    End Function

    Public Function WriteTrqLim(axis As Integer, atstop As Integer, atmovement As Integer) As Integer
        Return write_trqlim(axis, atstop, atmovement)
    End Function

    Public Function WriteTolerance(axis As Integer, width As Integer) As Integer
        Return write_inpos(axis, width)
    End Function

    Public Function ReadPosition(axis As Integer) As Integer
        Dim bank1 As Integer = Convert.ToInt16("29", 16)
        Dim bank2 As Integer = Convert.ToInt16("29", 16)
        Dim dataLoc As Integer = Convert.ToInt16("7400", 16)
        read_svmem(axis, dataLoc, bank1)
        Dim retVal As Integer = bank1
        Return retVal
    End Function

    Public Function ReadCode_RZONE(axis As Integer) As Integer
        Dim bank1 As Integer = Convert.ToInt16("29", 16)
        Dim bank2 As Integer = Convert.ToInt16("12", 16)
        Dim dataLoc As Integer = Convert.ToInt16("5", 16)
        read_svmem(axis, dataLoc, bank1)
        Dim retVal As Integer = bank1
        'write_svmem(axis, dataLoc, ref bank2);
        Return retVal
    End Function

    Public Function ReadCode_FZONE(axis As Integer) As Integer
        Dim bank1 As Integer = Convert.ToInt16("29", 16)
        Dim bank2 As Integer = Convert.ToInt16("12", 16)
        Dim dataLoc As Integer = Convert.ToInt16("4", 16)
        read_svmem(axis, dataLoc, bank1)
        Dim retVal As Integer = bank1
        'write_svmem(axis, dataLoc, ref bank2);
        Return retVal
    End Function

    '// State
    Public Const TMBS_NO_EXIST As Integer = 0
    Public Const TMBS_INITIAL As Integer = 1
    Public Const TMBS_INIT_ERROR As Integer = 2
    Public Const TMBS_OPENING As Integer = 3
    Public Const TMBS_RUNNING As Integer = 4
    Public Const SIO_DONE As Integer = 1
    Public Const SIO_ERROR As Integer = 0

    '// BAUD
    Public Const TMBS_BAUD_9600 As Integer = &H4&      '  9600 bps
    Public Const TMBS_BAUD_19200 As Integer = &H5&     ' 19200 bps
    Public Const TMBS_BAUD_38400 As Integer = &H6&     ' 38400 bps
    Public Const TMBS_BAUD_14400 As Integer = &H11&    ' 14400 bps
    Public Const TMBS_BAUD_57600 As Integer = &H13&    ' 57600 bps
    Public Const TMBS_BAUD_115200 As Integer = &H14&   '115200 bps
End Class



