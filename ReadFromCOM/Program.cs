using System;
using System.IO.Ports;
using System.Text;

namespace ReadFromCOM
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                //really jsut for debugging
                ConnectAndRead(new SerialPort("COM 3"));
            }
            else
            {
                if (args[0].Contains("COM"))
                {
                    //We know there's a COM port being passed
             //       ConnectAndRead(new SerialPort(args[0]));
                }
                else
                {
                    //Just break
                    throw new Exception("COM port was not passed as parameter when starting program.");
                }
            }
        }

        /// <summary>
        /// Connects to the COM port passed and begins read what is put on it
        /// </summary>
        /// <param name="port">A SerialPort to read from</param>
        static void ConnectAndRead(SerialPort port)
        {
            //We know the BDU size
            byte[] buffer = new byte[9600];
            
            //Just to print for debugging
            double messageNumber = 0;

            //Open the port
            port.Open();

            //Loop endlessly
            do
            {
                //Get the  most recent message
                var result = port.Read(buffer, 0, 9600);

                //Decode from byte array to string
                var stuffToWrite = Encoding.UTF8.GetString(buffer);
                
                //If we've gotten an error back
                if (Encoding.UTF8.GetBytes(stuffToWrite)[0] == 0)
                {
                    Console.WriteLine("There was an error");
                }
                else
                {
                    //Pretty print tbh
                    Console.WriteLine($"Message: {messageNumber} Got the value {stuffToWrite}");
                }
                
                //Sleep for two seconds
                System.Threading.Thread.Sleep(2000);
                
                //Increase messageNumber for the pretty print
                messageNumber++;
            //Dont stop looping
            } while (true);

        }
    }
}
