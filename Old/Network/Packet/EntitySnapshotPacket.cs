using BitManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EntitySnapshotPacket : Packet
{
    public readonly uint sequenceNumber;
    public readonly List<EntitySnapshot> snapshots = new List<EntitySnapshot>();

    public EntitySnapshotPacket(uint sequenceNumber)
    {
        packetType = PacketType.EntitySnapshot;

        this.sequenceNumber = sequenceNumber;
    }

    public void AddSnapshot(EntitySnapshot snapshot)
    {
        snapshots.Add(snapshot);
    }

    public override void WriteOut(BitWriter bitWriter)
    {
        bitWriter.Put(PacketType.EntitySnapshot);
        bitWriter.Put(sequenceNumber);
        bitWriter.Put((ushort)snapshots.Count);
        foreach (EntitySnapshot snapshot in snapshots)
            snapshot.WriteOut(bitWriter);
    }

    public EntitySnapshotPacket(BitReader bitReader, Func<ushort, EntityType> getEntityType)
    {
        packetType = PacketType.EntitySnapshot;

        sequenceNumber = bitReader.GetUInt();
        ushort snapshotCount = bitReader.GetUShort();

        for (int i = 0; i < snapshotCount; i++)
        {
            EntitySnapshot snapshot = EntitySnapshot.GetEntitySnapshot(sequenceNumber, bitReader, getEntityType);
            if (snapshot != null)
                snapshots.Add(snapshot);
        }
    }
}
