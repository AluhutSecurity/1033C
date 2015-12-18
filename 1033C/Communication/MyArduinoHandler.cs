using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1033C.Communication {
    public class MyArduinoHandler {
        private System.IO.Ports.SerialPort port;
        private readonly object sync;
        private System.Threading.Thread portReader;

        /// <summary>
        /// gets all available port names
        /// </summary>
        public string[] PortNames => System.IO.Ports.SerialPort.GetPortNames();

        public MyArduinoHandler() {
            this.port = new System.IO.Ports.SerialPort();
            this.sync = new object();

            //Hier richtigen Port wählen
            this.port.BaudRate = 115200;
            this.port.RtsEnable = true; //?
            this.port.DtrEnable = true; //?
        }

        /// <summary>
        /// sets/gets the name of the used port
        /// set:
        /// value is only valid if PortNames contains it
        /// if the port is already open, the port will be closed and reopened
        /// </summary>
        public string PortName {
            get {
                return this.port.PortName;
            }
            set {
                if ( !this.PortNames.Contains( value ) ) throw new ArgumentException( "invalid portname" );
                if ( this.port.IsOpen ) {
                    this.Close();
                    this.port.PortName = value;
                    this.Open();
                } else {
                    this.port.PortName = value;
                }
            }
        }

        /// <summary>
        /// opens serial port
        /// starts reader thread
        /// </summary>
        public void Open() {
            this.port.Open();
            this.portReader = new System.Threading.Thread( this.PortReader );
            this.portReader.Name = "MyArduinoHandler::PortReader";
            this.portReader.IsBackground = true;
            this.portReader.Start();
        }

        /// <summary>
        /// closes port
        /// terminates reader thread
        /// </summary>
        public void Close() {
            if ( this.portReader != null && this.portReader.IsAlive ) this.portReader.Abort();
            this.port.Close();
        }

        /// <summary>
        /// sends data to the arduino 
        /// using the opened port
        /// </summary>
        /// <param name="command">commandidentifier</param>
        /// <param name="value">appended value, is ignored if channel < MyArduinoCommand.Stop</param>
        public void Send( MyArduinoCommand channel, byte value ) {
            byte[] buffer = new byte[2] { ( byte )channel, value };

            lock ( this.sync ) {
                this.port.Write( buffer, 0, 1 );

                if ( ( byte )channel >= ( byte )MyArduinoCommand.Stop ) return;

                this.port.Write( buffer, 1, 1 );
            }
        }

        /// <summary>
        /// port reader thread
        /// waits for data from arduino
        /// calls OnDataReceived
        /// </summary>
        private void PortReader() {
            byte[] buffer = new byte[1024];
            while ( true ) {
                int read = this.port.Read( buffer, 0, buffer.Length );
                if ( read == 0 ) {
                    this.Close();
                    return;
                }

                if ( this.OnDataReceived != null ) this.OnDataReceived( this, buffer.Take( read ).ToArray() );
            }
        }

        /// <summary>
        /// gets called when the arduino sends data to the nuc
        /// </summary>
        public event Utils.Delegates.DataReceivedEventHandler OnDataReceived;
    }

    public enum MyArduinoCommand : byte {
        //valued commands [command|value]
        Yaw,        //rechts / links
        Pitch,      //vorne / hinten
        Roll,       //neigen rechts / links
        Throttle,   //hoch / runter

        /**********************************************/
        //single byte commands [command|]

        Stop,       //stehen bleiben (position halten)
        Control,    //auf nuc befehle reagieren
        StartPath,  //flightpath abfliegen
        TakePackage //paket aufnehmen
    }
}
