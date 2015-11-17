using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1033C.Drones {
    internal class MyDronePacketGenerator {
        private BlakcMakiga.Utils.CRC.IBlakcCRCProvider crcProvider;
        private BlakcMakiga.Utils.CRC.CRCMode crcMode;

        internal MyDronePacketGenerator( BlakcMakiga.Utils.CRC.CRCMode crcmode ) {
            this.crcProvider = BlakcMakiga.Abrakadubra.CreateInstance( BlakcMakiga.BlakcInstanceIdentifier.BlakcCRCProvider ) as BlakcMakiga.Utils.CRC.IBlakcCRCProvider;
            this.crcMode = crcmode;
        }

        internal MyDronePacket GeneratePacket( MyDronePacketContent contentid ) {
            MyDronePacket packet = new MyDronePacket( contentid );
            packet.CRC = ( ushort ) this.crcProvider.CalculateCRC( this.crcMode, BitConverter.GetBytes( ( ushort ) contentid ), 0, 2 );
            return packet;
        }

        internal MyDronePacket GeneratePacket( MyDronePacketContent contentid, byte[] content ) {
            MyDronePacket packet = new MyDronePacket( contentid, content );
            var blob = BitConverter.GetBytes( ( ushort ) contentid ).Concat( content ).ToArray();
            packet.CRC = ( ushort ) this.crcProvider.CalculateCRC( this.crcMode, blob, 0, blob.Length );
            return packet;
        }

        internal bool ValidatePacket( MyDronePacket packet ) {
            var blob = BitConverter.GetBytes( ( ushort ) packet.ContentID ).Concat( packet.Data ).ToArray();
            return ( ushort ) this.crcProvider.CalculateCRC( this.crcMode, blob, 0, blob.Length ) == packet.CRC;
        }
    }
}
