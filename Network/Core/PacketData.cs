using BitManipulation;
using UnityEngine;

public static class PacketData
{
    #region Position
    public static void Put(this BitWriter bitWriter, Vector3 value)
    {
        bitWriter.Put((int)((value.x * 256f) + 8192), 14);
        bitWriter.Put((int)(value.y * 256f), 14);
        bitWriter.Put((int)((value.z * 256f) + 8192), 14);
    }
    public static Vector3 GetPosition(this BitReader bitReader)
    {
        return new Vector3((bitReader.GetInt(14) - 8192) / 256f, (bitReader.GetInt(14)) / 256f, (bitReader.GetInt(14) - 8192) / 256f);
    }
    #endregion Position

    #region Degree
    public static void PutDegree(this BitWriter bitWriter, float value, float largestValue = 360)
    {
        bitWriter.Put((byte)(value * (byte.MaxValue / largestValue)));
    }
    public static float GetDegree(this BitReader bitReader, float largestValue = 360)
    {
        return bitReader.GetByte() * (largestValue / byte.MaxValue);
    }
    #endregion Degree

    #region EntityType
    public static void Put(this BitWriter bitWriter, EntityType value)
    {
        bitWriter.Put((byte)value);
    }
    public static EntityType GetEntityType(this BitReader bitReader)
    {
        return (EntityType)bitReader.GetByte();
    }
    #endregion EntityType

    #region PacketType
    public static void Put(this BitWriter bitWriter, PacketType value)
    {
        bitWriter.Put((byte)value);
    }
    public static PacketType GetPacketType(this BitReader bitReader)
    {
        return (PacketType)bitReader.GetPacketRoutingType();
    }
    #endregion PacketType

    #region PacketRoutingType
    public static void Put(this BitWriter bitWriter, PacketRoutingType value)
    {
        bitWriter.Put((byte)value);
    }
    public static PacketRoutingType GetPacketRoutingType(this BitReader bitReader)
    {
        return (PacketRoutingType)bitReader.GetByte();
    }
    #endregion PacketRoutingType

    #region GameInfoType
    public static void Put(this BitWriter bitWriter, GameInfoType value)
    {
        bitWriter.Put((byte)value);
    }
    public static GameInfoType GetGameInfoType(this BitReader bitReader)
    {
        return (GameInfoType)bitReader.GetByte();
    }
    #endregion GameInfoType
}
