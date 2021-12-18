using BitManipulation;

public abstract class Packet : IWritable
{
    public PacketType packetType;

    public abstract void WriteOut(BitWriter bitWriter);
}
