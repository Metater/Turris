using BitManipulation;
using LiteNetLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ServerEntityManager : EntityManager
{
    [SerializeField] private Transform controlledEntitiesParent;
    [SerializeField] private List<GameObject> controlledEntityPrefabs = new List<GameObject>();

    private List<ControlledEntityHandler> controlledEntities = new List<ControlledEntityHandler>();

    private ushort nextEntityId = 0;

    public ushort SpawnServerUncontrolledPlayer(Vector3 spawnPosition)
    {
        ushort entityId = nextEntityId;
        nextEntityId++;
        SpawnUncontrolledEntity(entityId, EntityType.Player, spawnPosition);
        return entityId;
    }

    public ushort SpawnServerControlledEntity(EntityType entityType, Vector3 spawnPosition)
    {
        ushort entityId = nextEntityId;
        nextEntityId++;
        ControlledEntityHandler controlledEntityHandler;
        if (entityType == EntityType.Player)
        {
            player.gameObject.transform.position = spawnPosition;
            controlledEntityHandler = player.GetComponentInParent<ControlledEntityHandler>();
            controlledEntityHandler.enabled = true;
            controlledEntityHandler.Init(entityId, entityType, true);
        }
        else
        {
            GameObject entityGO = Instantiate(controlledEntityPrefabs[(int)entityType], spawnPosition, Quaternion.identity, controlledEntitiesParent);
            controlledEntityHandler = entityGO.GetComponent<ControlledEntityHandler>();
            controlledEntityHandler.Init(entityId, entityType, true);
            switch (entityType)
            {
                case EntityType.BoomBox:
                    break;
                case EntityType.CannonTower:
                    break;
            }
        }

        controlledEntities.Add(controlledEntityHandler);

        SpawnEntityPacket spawnEntityPacket = new SpawnEntityPacket(entityId, entityType, spawnPosition);
        BitWriter bitWriter = new BitWriter();
        bitWriter.Put(PacketRoutingType.BroadcastButToSender);
        spawnEntityPacket.WriteOut(bitWriter);
        GameManager.I.Send(bitWriter.Assemble(), DeliveryMethod.ReliableOrdered);
        return entityId;
    }

    private void SendControlledEntitySnapshot()
    {
        if (controlledEntities.Count == 0) return;
        EntitySnapshotPacket entitySnapshotPacket = new EntitySnapshotPacket(nextSequenceNumber);
        foreach (ControlledEntityHandler controlledEntity in controlledEntities)
            entitySnapshotPacket.AddSnapshot(controlledEntity.GetEntitySnapshot());
        BitWriter bitWriter = new BitWriter(10, 10);
        bitWriter.Put(PacketRoutingType.BroadcastButToSender);
        entitySnapshotPacket.WriteOut(bitWriter);
        GameManager.I.Send(bitWriter.Assemble(), DeliveryMethod.Sequenced);
    }


}