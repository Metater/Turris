using BitManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EntitySnapshotPacketHandler : PacketHandler
{
    public override void Handle(BitReader bitReader)
    {
        EntitySnapshotPacket p = new EntitySnapshotPacket(bitReader, GameManager.I.entityManager.GetEntityType);
        GameManager.I.entityManager.HandleEntitySnapshots(p.snapshots);
    }
}
