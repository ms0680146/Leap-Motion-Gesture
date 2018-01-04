import Dictionary;
class controller
{
    private SerialPort comport = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
    public void start(){
		//comport = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
		Byte[] buffer = { 0x00 };
        this.call(comport, buffer);
    }
    public void light(){
        //comport = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
		Byte[] buffer = { 0x01 };
        this.call(comport, buffer);
    }

    public void windLevel(){
        //comport = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
		Byte[] buffer = { 0x02 };
        this.call(comport, buffer);
    }

    public void trunOff(){
        //comport = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
		Byte[] buffer = { 0x03 };
        this.call(comport, buffer);
    }

    private void call(SerialPort comport, Byte[] buffer){
        if (!comport.IsOpen){
		    comport.Open();
		}
		comport.Write(buffer, 0, buffer.Length);
		comport.Close();
    }
}