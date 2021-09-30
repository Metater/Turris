using BitManipulation;
using UnityEngine;

public static class PacketData
{
    public static void PutPosition(this BitWriter bitWriter, Vector3 value)
    {
        bitWriter.Put((int)((value.x * 256f) + 8192), 14);
        bitWriter.Put((int)(value.y * 256f), 14);
        bitWriter.Put((int)((value.z * 256f) + 8192), 14);
    }
    public static Vector3 GetPosition(this BitReader bitReader)
    {
        return new Vector3((bitReader.GetInt(14) - 8192) / 256f, (bitReader.GetInt(14)) / 256f, (bitReader.GetInt(14) - 8192) / 256f);
    }

    public static void PutEntityType(this BitWriter bitWriter, EntityType value)
    {
        bitWriter.Put((byte)value);
    }
    public static EntityType GetEntityType(this BitReader bitReader)
    {
        return (EntityType)bitReader.GetByte();
    }

    public static void PutPacketType(this BitWriter bitWriter, PacketType value)
    {
        bitWriter.Put((byte)value);
    }
    public static PacketType GetPacketType(this BitReader bitReader)
    {
        return (PacketType)bitReader.GetPacketRoutingType();
    }

    public static void PutPacketRoutingType(this BitWriter bitWriter, PacketRoutingType value)
    {
        bitWriter.Put((byte)value);
    }
    public static PacketRoutingType GetPacketRoutingType(this BitReader bitReader)
    {
        return (PacketRoutingType)bitReader.GetByte();
    }
}
