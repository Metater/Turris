using BitManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameInfoPacket : Packet
{
    public readonly GameInfoType gameInfoType;

    public GameInfoPacket(bool isLeader, ushort clientId, string name)
    {
        packetType = PacketType.GameInfo;
        gameInfoType = GameInfoType.SelfJoin;
    }

    public GameInfoPacket(ushort clientId, string name)
    {
        packetType = PacketType.GameInfo;
        gameInfoType = GameInfoType.Join;
    }

    public GameInfoPacket(ushort clientId)
    {
        packetType = PacketType.GameInfo;
        gameInfoType = GameInfoType.Leave;
    }

    public override void WriteOut(BitWriter bitWriter)
    {
        bitWriter.Put(packetType);
        bitWriter.Put(gameInfoType);

    }

    public GameInfoPacket(BitReader bitReader)
    {
        packetType = PacketType.GameInfo;

    }
}