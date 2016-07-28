using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class UART1 : MonoBehaviour
{

    //debug
    public string hexaIn;
    float t;

    public string dataSent;
    public bool readyToSend = false;

    SerialPort serial1;
    int bufSize = 16;
    public byte[] buf = new byte[16];
    public int bufCount = 0;
    public int a, b, sum;

    // Use this for initialization
    void Start()
    {
        serial1 = new SerialPort();
        serial1.PortName = "COM3";
        serial1.Parity = Parity.None;
        serial1.BaudRate = 9600;
        serial1.DataBits = 8;
        serial1.ReadTimeout = 40;
        serial1.WriteTimeout = 40;
        serial1.StopBits = StopBits.One;

        //try { serial1.Close(); } catch { }
        serial1.Open();
        bufCount = 0;
        a = 0;
        b = 0;

        //readyToSend = true;
    }

    //What happens when the application closes
    void OnApplicationQuit()
    {
        serial1.Close();
    }

    //Update is called once per frame
    void FixedUpdate()
    {
        if (readyToSend)
        {
            if (dataSent != "")
            {
                t = 0;
                hexaIn = "";
                serial1.Write(dataSent);
                ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
                System.Threading.Thread.Sleep(1000);
                hexaIn = "";
                serial1.Write(dataSent);
                ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
                System.Threading.Thread.Sleep(1000);
                hexaIn = "";
                StartCoroutine(SendReadData(dataSent));
            }
        }
    }



    void ReadSerialData()
    {
        buf[15] = 0;
        bufCount = 0;
        sum = 0;
        bufCount = serial1.Read(buf, sum, 16 - sum);

        Debug.Log(buf);

    }

    static string ByteToHexBitFiddle(byte[] bytes)
    {
        char[] c = new char[bytes.Length * 2];
        int b;
        for (int i = 0; i < bytes.Length; i++)
        {
            b = bytes[i] >> 4;
            c[i * 2] = (char)(55 + b + (((b - 10) >> 31) & -7));
            b = bytes[i] & 0xF;
            c[i * 2 + 1] = (char)(55 + b + (((b - 10) >> 31) & -7));
        }
        return new string(c);
    }

    IEnumerator SendReadData(string data) {
        while (!readyToSend) { }
        readyToSend = false;
        hexaIn = "";
        //System.Threading.Thread.Sleep(40);
        while ((hexaIn[30] != '0' && hexaIn[31] != '3') ) {
            try
            {
                System.Threading.Thread.Sleep(40);
                serial1.Write(dataSent);
                ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
            }
            catch
            {
                Debug.Log("Not received yet!");
            }
            //System.Threading.Thread.Sleep(20);
        };
        readyToSend = true;
        StartCoroutine(SendReadData(data));
        yield return new WaitForSeconds(.1f);
    }



    //Update is called once per frame
    //void Update()
    //{
    //    if (sendIt)
    //    {
    //        if (dataSent != "")
    //        {
    //            serial1.Write(dataSent);
    //            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
    //            System.Threading.Thread.Sleep(2000);
    //            serial1.Write(dataSent);
    //            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
    //            System.Threading.Thread.Sleep(2000);
    //            serial1.Write(dataSent);
    //            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
    //            sendIt = false;
    //        }
    //    }
    //}

    //void FixedUpdate()
    //{
    //    t += Time.fixedDeltaTime;
    //    if (sendIt)
    //    {
    //        if (dataSent != "")
    //        {
    //            t = 0;
    //            hexaIn = "";
    //            serial1.Write(dataSent);
    //            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
    //            System.Threading.Thread.Sleep(1000);
    //            hexaIn = "";
    //            serial1.Write(dataSent);
    //            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
    //            System.Threading.Thread.Sleep(1000);
    //            hexaIn = "";
    //            serial1.Write(dataSent);
    //            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
    //            Debug.Log(hexaIn[30]); Debug.Log(hexaIn[31]);
    //            while ((hexaIn[30] != '0' && hexaIn[31] != '3') && t < 0.050f) { Debug.Log("Wainting for 'U'"); };
    //            hexaIn = "";
    //            serial1.Write(dataSent);
    //            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
    //            sendIt = false;
    //        }
    //    }
    //}

    //void FixedUpdate()
    //{
    //    if (readyToSend)
    //    {
    //        if (dataSent != "")
    //        {
    //            t = 0;
    //            hexaIn = "";
    //            serial1.Write(dataSent);
    //            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
    //            System.Threading.Thread.Sleep(1000);
    //            hexaIn = "";
    //            serial1.Write(dataSent);
    //            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
    //            System.Threading.Thread.Sleep(1000);
    //            hexaIn = "";
    //            if (readyToSend)
    //            {
    //                StartCoroutine(SendReadData(dataSent));
    //            }
    //            if (readyToSend)
    //            {
    //                StartCoroutine(SendReadData(dataSent));
    //            }
    //            readyToSend = false;
    //        }
    //    }
    //}
}
