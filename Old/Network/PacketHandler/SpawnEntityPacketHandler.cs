using BitManipulation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntityPacketHandler : PacketHandler
{
    public override void Handle(BitReader bitReader)
    {
        SpawnEntityPacket p = new SpawnEntityPacket(bitReader);
        GameActions.SpawnEntity(p.entityId, p.entityType, p.spawnPosition);
    }
}
