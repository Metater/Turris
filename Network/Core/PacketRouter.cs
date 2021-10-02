using BitManipulation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketRouter
{
    private Dictionary<PacketType, PacketHandler> packetHandlers = new Dictionary<PacketType, PacketHandler>();

    public PacketRouter()
    {
        packetHandlers.Add(PacketType.SpawnEntity, new SpawnEntityPacketHandler());
        packetHandlers.Add(PacketType.DespawnEntity, new DespawnEntityPacketHandler());
        packetHandlers.Add(PacketType.EntitySnapshot, new EntitySnapshotPacketHandler());
        //packetHandlers.Add(PacketType.EntitySnapshot, new EntitySnapshotPacketHandler());
        //packetHandlers.Add(PacketType.EntityEvent, new EntityEventPacketHandler());
    }

    public bool Route(BitReader bitReader)
    {
        PacketType packetType = bitReader.GetPacketType();
        if (packetHandlers.TryGetValue(packetType, out PacketHandler packetHandler))
            packetHandler.Handle(bitReader);
        else
            return false;
        return true;
    }
}
