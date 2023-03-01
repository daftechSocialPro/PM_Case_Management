using GsmComm.GsmCommunication;
using GsmComm.PduConverter;
using System.IO.Ports;

namespace PM_Case_Managemnt_API.Helpers
{
    public class SMSSerial
    {
        private SerialPort serialPort;

        //Initialize the Port
        public SMSSerial(string comPort, int BaudRate)
        {
            this.serialPort = new SerialPort();
            this.serialPort.PortName = comPort;
            this.serialPort.BaudRate = 9600;
            this.serialPort.Parity = Parity.None;
            this.serialPort.DataBits = 8;
            this.serialPort.StopBits = StopBits.One;
            this.serialPort.Handshake = Handshake.RequestToSend;
            this.serialPort.DtrEnable = true;
            this.serialPort.RtsEnable = true;
            this.serialPort.NewLine = System.Environment.NewLine;

        }

        private GsmCommMain comm;

        public bool SendSMSByDongle(string cellNo, string sms)
        {

            var timeout = 300;
            this.serialPort.PortName = "COM" + this.serialPort.PortName;
            comm = new GsmCommMain(this.serialPort.PortName, this.serialPort.BaudRate, timeout);
            try
            {
                //sms = "የኢኮኖሚና ጥናት እና ምክር አገልግሎት መምሪያOffice የኢኮኖሚና ጥናት እናአገልግሎት መምሪያ እናአገልግሎት";
                //cellNo = "+251925122620";
                // byte[] utf16Data = Encoding.Unicode.GetBytes(sms);
                // byte x = 20AC;
                if (comm.IsOpen())
                {
                    comm.Close();
                }
                comm.Open();

                SmsSubmitPdu pdu = new SmsSubmitPdu(sms, cellNo, DataCodingScheme.Class1_16Bit);
                comm.SendMessage(pdu); //debug found error here
                comm.Close();
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("SMS not send" + ex);
                throw new Exception(ex.Message);
            }
        }

        public bool sendSMS(string cellNo, string sms)
        {
            //sms = "ddddd";
            //cellNo = "+251925122620";
            Opens();
            string messages = sms;
            if (this.serialPort.IsOpen == true)
            {
                try
                {
                    this.serialPort.WriteLine("AT" + (char)(13));
                    // this.serialPort.Encoding = Encoding.UTF32;
                    Thread.Sleep(4);
                    this.serialPort.WriteLine("AT+CMGF=1" + (char)(13));
                    Thread.Sleep(5);
                    this.serialPort.WriteLine("AT+CMGS=\"" + cellNo + "\"");
                    Thread.Sleep(10);


                    this.serialPort.WriteLine("" + messages + (char)(26));
                    Thread.Sleep(10);
                    this.serialPort.Close();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                return true;
            }
            else return false;
        }

        public void Opens()
        {
            try
            {
                if (this.serialPort.IsOpen == false)
                {
                    this.serialPort.Open();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                // this.serialPort.Close();
            }
        }

    }
}
