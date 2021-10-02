using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiteNetLib;
using System.Net;
using System.Net.Sockets;
using BitManipulation;
using UnityEngine.SceneManagement;

public class TurrisClientListener : INetEventListener
{
    public PacketRouter packetRouter = new PacketRouter();

    public void OnConnectionRequest(ConnectionRequest request)
    {

    }
    public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
    {

    }
    public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
    {

    }
    public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
    {
        byte[] data = reader.GetRemainingBytes();
        BitReader bitReader = new BitReader(data);
        PacketRoutingType packetRoutingType = bitReader.GetPacketRoutingType();
        // while (data left in bitReader) maybe?
        packetRouter.Route(bitReader);
        /*
        string output = "";
        foreach (byte b in data)
        {
            output += $"{b}, ";
        }
        Debug.Log("Got Data: " + output);
        int packetType = br.GetByte(2);
        Debug.Log("Packet Type: " + packetType);
        switch (packetType)
        {
            case 0: // New Player
                Debug.Log("New Player");
                GameManager.I.entityManager.NewPlayer(br);
                break;
            case 1: // Player Update, combine player update packets later
                Debug.Log("Update Player");
                GameManager.I.entityManager.UpdatePlayer(br);
                break;
            default:
                Debug.Log("WAT DA HECKLER?");
                break;
        }
        */

    }
    public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
    {

    }
    public void OnPeerConnected(NetPeer peer)
    {
        GameManager.I.connected = true;
        GameManager.I.entityManager.SpawnServerControlledEntity(EntityType.Player, GameManager.I.playerSpawnPosition.position);
    }
    public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        GameManager.I.connected = false;
        SceneManager.LoadScene(0);
    }
}
