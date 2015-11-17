using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1033C.Drones {
    public class MyDroneServer {
        private BlakcMakiga.Net.IBlakcServer server;
        private List<MyDrone> drones;
        private ushort sequenceCount;
        private MyDronePacketGenerator packetGenerator;

        public int Port { get; set; }
        public System.Net.IPAddress LocalEndPoint { get; set; }

        public event EventHandler<MyDrone> DroneStatusChanged;

        public MyDroneServer() {
            this.server = BlakcMakiga.Abrakadubra.CreateInstance( BlakcMakiga.BlakcInstanceIdentifier.BlakcServer ) as BlakcMakiga.Net.IBlakcServer;
            this.server.ClientStatusChanged += Server_ClientStatusChanged;
            this.server.ClientDataReceived += Server_ClientDataReceived;

            this.Port = Defines.DEFAULT_DRONESERVER_LOCALPORT;
            this.LocalEndPoint = Defines.DEFAULT_DRONESERVER_LISTENERIP;

            this.packetGenerator = new MyDronePacketGenerator( BlakcMakiga.Utils.CRC.CRCMode.CRC16 );

            this.drones = new List<MyDrone>( 2 );

            this.sequenceCount = 0;
        }

        private void Server_ClientStatusChanged( object sender, BlakcMakiga.Net.BlakcClientStatus e ) {
            MyDrone affectedDrone;
            BlakcMakiga.Net.IBlakcClient client = ( sender as BlakcMakiga.Net.IBlakcClient );

            affectedDrone = this.drones.FirstOrDefault( x => x.UID == client.UID );
            switch ( e ) {
                case BlakcMakiga.Net.BlakcClientStatus.Connected:
                    break;
                case BlakcMakiga.Net.BlakcClientStatus.Disconnected:
                    if ( null == affectedDrone ) return;

                    affectedDrone.State = MyDroneState.Offline;
                    if ( this.DroneStatusChanged != null ) this.DroneStatusChanged( this, affectedDrone );
                    break;
                case BlakcMakiga.Net.BlakcClientStatus.Online:
                    if ( null == affectedDrone ) {
                        affectedDrone = new MyDrone( client.UID, MyDroneState.Online );
                        this.drones.Add( affectedDrone );
                    }
                    if ( this.DroneStatusChanged != null ) this.DroneStatusChanged( this, affectedDrone );
                    break;
                case BlakcMakiga.Net.BlakcClientStatus.Unknown:
                    if ( null == affectedDrone ) {
                        affectedDrone = new MyDrone( client.UID, MyDroneState.Unknown );
                        this.drones.Add( affectedDrone );
                    }
                    if ( this.DroneStatusChanged != null ) this.DroneStatusChanged( this, affectedDrone );
                    break;
            }
        }

        private bool Server_ClientDataReceived( object sender, KeyValuePair<ushort, byte[]> data ) {
            BlakcMakiga.Net.IBlakcClient client = sender as BlakcMakiga.Net.IBlakcClient;
            MyDronePacket packet = MyDronePacket.DeSerialize( data.Value );
            MyDrone droneSender = this.drones.FirstOrDefault( x => x.UID == ( ( BlakcMakiga.Net.IBlakcClient ) sender ).UID );
            if ( droneSender == null ) return true;

            if ( !this.packetGenerator.ValidatePacket( packet ) ) return true;


            switch ( packet.ContentID ) {
                case MyDronePacketContent.StatusUpdate:
                    droneSender.State = ( MyDroneState ) BitConverter.ToUInt32( packet.Data, 0 );
                    if ( this.DroneStatusChanged != null ) this.DroneStatusChanged( this, droneSender );
                    return true;
                case MyDronePacketContent.LocationUpdate:
                    droneSender.Position = Location.MyGPSPosition.DeSerialize( packet.Data );
                    return true;
            }

            return false;
        }

        public void Start() {
            this.server.Start( this.Port, this.LocalEndPoint );
        }

        public void Stop() {
            this.server.Stop();
        }

        public MyDrone GetDroneByUID( ulong uid ) {
            return this.drones.FirstOrDefault( x => x.UID == uid );
        }

        public bool RequestPackagePickup( Delivery.MyPackage package ) {
            var availableDrones = this.drones.Where( x => x.State == MyDroneState.Ready ).OrderBy( x => x.Capacity );
            MyDrone selectedDrone = null;
            int dronecount;

            switch ( dronecount = availableDrones.Count() ) {
                case 0:
                    return false;
                case 1:
                    selectedDrone = availableDrones.ElementAt( 0 );
                    if ( selectedDrone.Capacity < package.Weight ) return false;
                    break;
                default:
                    for ( int i = 0; i < dronecount; i++ ) {
                        if ( availableDrones.ElementAt( i ).Capacity >= package.Weight ) {
                            selectedDrone = availableDrones.ElementAt( i );
                            break;
                        }
                    }
                    break;
            }

            if ( selectedDrone == null ) return false;

            var client = this.server.Clients.First( x => x.UID == selectedDrone.UID );
            ushort sid = ++this.sequenceCount;
            int transmissionCount = 0;

            do {
                if ( transmissionCount++ == 3 ) return false;

                client.Send( sid, this.packetGenerator.GeneratePacket( MyDronePacketContent.PackagePickup, BitConverter.GetBytes( package.UID ) ).Serialize() );
                var response = client.Receive( sid, 2000 );

                if ( response == null ) continue;
                MyDronePacket packetresponse = MyDronePacket.DeSerialize( response );
                if ( !this.packetGenerator.ValidatePacket( packetresponse ) ) continue;
                if ( packetresponse.ContentID == MyDronePacketContent.Invalid ) continue;
                return true;
            } while ( true );
        }

        public void HaltDrones() {
            this.server.Broadcast( ++this.sequenceCount, this.packetGenerator.GeneratePacket( MyDronePacketContent.SignalHalt ).Serialize() );
        }
    }
}
