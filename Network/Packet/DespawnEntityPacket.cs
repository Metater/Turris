using BitManipulation;

public class DespawnEntityPacket : Packet
{
    public readonly ushort entityId;

    public DespawnEntityPacket(ushort entityId)
    {
        packetType = PacketType.DespawnEntity;

        this.entityId = entityId;
    }

    public override void WriteOut(BitWriter bitWriter)
    {
        bitWriter.Put(packetType);
        bitWriter.Put(entityId);
    }

    public DespawnEntityPacket(BitReader bitReader)
    {
        packetType = PacketType.DespawnEntity;

        entityId = bitReader.GetUShort();
    }
}
