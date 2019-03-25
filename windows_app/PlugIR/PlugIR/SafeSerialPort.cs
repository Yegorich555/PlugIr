using System.Collections.Generic;
using System.IO.Ports;

namespace PlugIR
{
    public enum BaudRates
    {
        _2400 = 2400, _4800 = 4800, _9600 = 9600, _14400 = 14400, _19200 = 19200,
        _28800 = 28800, _38400 = 38400, _57600 = 57600, _76800 = 76800, _115200 = 115200, _230400 = 230400
    }

    public enum DataBits { b5 = 5, b6 = 6, b7 = 7, b8 = 8 }

    public class SafeSerialPort : SerialPort
    {
        // private Stream theBaseStream;

        public SafeSerialPort(string portName, BaudRates baudRate, DataBits dataBits, Parity parity, StopBits stopBits)
            : base(portName, (int)baudRate, parity, (int)dataBits, stopBits)
        {
            // SerialPort.DataBits
        }

        public SafeSerialPort()
        {

        }

        List<char> tempBuffer = new List<char>(50);
        public string TryReadLine()
        {
            for (int i = 0; i < BytesToRead; ++i)
                tempBuffer.Add((char)ReadByte());

            var str = string.Concat(tempBuffer);
            if (str.Contains(NewLine))
            {
                tempBuffer.Clear();
                return str;
            }

            return null;
        }

        /* public new void Open()
         {
             base.Open();
             theBaseStream = BaseStream;
             GC.SuppressFinalize(BaseStream);
         }*/

        //public new void Dispose()
        //{
        //    Dispose(true);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (Container != null))
        //        Container.Dispose();

        //    try
        //    {

        //        if (theBaseStream != null && theBaseStream.CanRead)
        //        {
        //            theBaseStream.Close();
        //            GC.ReRegisterForFinalize(theBaseStream);
        //        }
        //    }
        //    catch (Exception ex) { MessageBox.Show("Error " + GetType().FullName + ": " + MethodBase.GetCurrentMethod().Name + "()\n" + ex.ToString()); }
        //    base.Dispose(disposing);
        //}
    }
}
