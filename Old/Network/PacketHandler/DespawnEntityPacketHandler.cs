using BitManipulation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnEntityPacketHandler : PacketHandler
{
    public override void Handle(BitReader bitReader)
    {
        DespawnEntityPacket p = new DespawnEntityPacket(bitReader);
        GameActions.DespawnEntity(p.entityId);
    }
}
