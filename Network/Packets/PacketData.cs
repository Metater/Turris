using BitManipulation;
using UnityEngine;

public class PositionData
{
    public static void Serialize(BitWriter bitWriter, Vector3 value)
    {
        bitWriter.Put((int)((value.x * 256f) + 8192), 14);
        bitWriter.Put((int)(value.y * 256f), 14);
        bitWriter.Put((int)((value.z * 256f) + 8192), 14);
    }
    public static Vector3 Deserialize(BitReader bitReader)
    {
        return new Vector3((bitReader.GetInt(14) - 8192) / 256f, (bitReader.GetInt(14)) / 256f, (bitReader.GetInt(14) - 8192) / 256f);
    }
}