using System;
using System.Collections.Generic;
using System.IO.Ports;

class FanController
{
    private static SerialPort comport = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
    private static readonly Dictionary<string, Byte[]> COMMANDS = new Dictionary<string, Byte[]> {
        {"WindLevel", new Byte[]{ 0x00} },
        {"Light", new Byte[]{ 0x01} },
        {"Off", new Byte[]{ 0x02} },
    };
    public static void WindLevel(){
		//comport = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
		Byte[] buffer = COMMANDS["WindLevel"];
        Call(comport, buffer);
    }
    public static void LightOn(){
        //comport = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
		Byte[] buffer = COMMANDS["Light"];
        Call(comport, buffer);
    }

    public static void PowerOff(){
        //comport = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
        Byte[] buffer = COMMANDS["Off"];
        Call(comport, buffer);
    }

    private static void Call(SerialPort Comport, Byte[] Buffer){
        if (!Comport.IsOpen){
		    Comport.Open();
		}
		Comport.Write(Buffer, 0, Buffer.Length);
		Comport.Close();
    }
}