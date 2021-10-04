using BitManipulation;
using LiteNetLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [SerializeField] private Transform controlledEntitiesParent;
    [SerializeField] private Transform uncontrolledEntitiesParent;
    [SerializeField] private List<GameObject> controlledEntityPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> uncontrolledEntityPrefabs = new List<GameObject>();

    [SerializeField] private ControlledEntityHandler player;

    private List<ControlledEntityHandler> controlledEntities = new List<ControlledEntityHandler>();
    private Dictionary<int, UncontrolledEntityHandler> uncontrolledEntities = new Dictionary<int, UncontrolledEntityHandler>();

    private ushort nextEntityId = 0;
    private uint nextSequenceNumber = 0;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        SendControlledEntitySnapshot();
        nextSequenceNumber++;
    }

    #region ControlledEntityMethods
    // Client server only!
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

    public void SpawnClientControlledPlayer(ushort entityId, Vector3 spawnPosition)
    {
        GameObject entityGO = Instantiate(controlledEntityPrefabs[(int)EntityType.Player], spawnPosition, Quaternion.identity, controlledEntitiesParent);
        PlayerHandler playerHandler = entityGO.GetComponent<PlayerHandler>();
        playerHandler.Init(entityId, EntityType.Player, true);
        controlledEntities.Add(playerHandler);
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
    #endregion ControlledEntityMethods

    #region UncontrolledEntityMethods
    // Client server only! Saves client server having to send and receive a spawn player command that it previously sent.
    public ushort SpawnServerUncontrolledPlayer(Vector3 spawnPosition)
    {
        ushort entityId = nextEntityId;
        nextEntityId++;
        SpawnUncontrolledEntity(entityId, EntityType.Player, spawnPosition);
        return entityId;
    }

    public void SpawnUncontrolledEntity(ushort entityId, EntityType entityType, Vector3 spawnPosition)
    {
        GameObject entityGO = Instantiate(uncontrolledEntityPrefabs[(int)entityType], spawnPosition, Quaternion.identity, uncontrolledEntitiesParent);
        UncontrolledEntityHandler uncontrolledEntityHandler = entityGO.GetComponent<UncontrolledEntityHandler>();
        uncontrolledEntityHandler.Init(entityId, entityType);
        switch (entityType)
        {
            case EntityType.Player:
                OtherPlayerHandler otherPlayerHandler = (OtherPlayerHandler)uncontrolledEntityHandler;
                break;
            case EntityType.BoomBox:
                //WalkingBoxHandler walkingBoxHandler = (WalkingBoxHandler)entityHandler;
                break;
            default:
                return;
        }
        uncontrolledEntities.Add(entityId, uncontrolledEntityHandler);
    }

    public void DespawnUncontrolledEntity(ushort entityId)
    {
        if (uncontrolledEntities.TryGetValue(entityId, out UncontrolledEntityHandler entity))
        {
            uncontrolledEntities.Remove(entityId);
            entity.DestroyEntity();
        }
    }

    public void HandleEntitySnapshots(List<EntitySnapshot> entitySnapshots)
    {
        foreach (EntitySnapshot snapshot in entitySnapshots)
            if (uncontrolledEntities.TryGetValue(snapshot.entityId, out UncontrolledEntityHandler entity))
                entity.HandleSnapshot(snapshot);
    }

    public EntityType GetEntityType(ushort entityId)
    {
        if (uncontrolledEntities.TryGetValue(entityId, out UncontrolledEntityHandler entity))
            return entity.EntityType;
        return EntityType.None;
    }
    #endregion UncontrolledEntityMethods
}
