using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1033C.Drones {
    public class MyDronePacket {

        internal MyDronePacket() { }

        internal MyDronePacket( MyDronePacketContent cid ) {
            this.ContentID = cid;
        }
        internal MyDronePacket( MyDronePacketContent cid, byte[] content ) {
            this.ContentID = cid;
            this.Data = content;
        }

        public static int HeaderSize => 4;


        public ushort CRC { get; set; }

        public MyDronePacketContent ContentID { get; private set; }

        public byte[] Data { get; private set; }

        public byte[] Serialize() {
            if(this.Data == null ) {
                return BitConverter.GetBytes( this.CRC ).Concat( BitConverter.GetBytes( ( ushort ) this.ContentID ) ).ToArray();
            } 
            return BitConverter.GetBytes( this.CRC ).Concat( BitConverter.GetBytes( ( ushort ) this.ContentID ) ).Concat( this.Data ).ToArray();
        }

        public static MyDronePacket DeSerialize( byte[] data ) {
            if ( null == data ) throw new ArgumentNullException( nameof( data ) );

            MyDronePacket mdp = new MyDronePacket();
            mdp.CRC = BitConverter.ToUInt16( data, 0 );
            mdp.ContentID = ( MyDronePacketContent ) BitConverter.ToUInt16( data, 2 );
            mdp.Data = data.Skip( 4 ).ToArray();
            
            return mdp;
        }
        
    }
}
