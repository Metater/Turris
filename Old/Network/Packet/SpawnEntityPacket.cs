using BitManipulation;
using UnityEngine;

public class SpawnEntityPacket : Packet
{
    public readonly ushort entityId;
    public readonly EntityType entityType;
    public readonly Vector3 spawnPosition;

    public SpawnEntityPacket(ushort entityId, EntityType entityType, Vector3 spawnPosition)
    {
        packetType = PacketType.SpawnEntity;

        this.entityId = entityId;
        this.entityType = entityType;
        this.spawnPosition = spawnPosition;
    }

    public override void WriteOut(BitWriter bitWriter)
    {
        bitWriter.Put(packetType);
        bitWriter.Put(entityId);
        bitWriter.Put(entityType);
        bitWriter.Put(spawnPosition);
    }

    public SpawnEntityPacket(BitReader bitReader)
    {
        packetType = PacketType.SpawnEntity;

        entityId = bitReader.GetUShort();
        entityType = bitReader.GetEntityType();
        spawnPosition = bitReader.GetPosition();
    }
}
