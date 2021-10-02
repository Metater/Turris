using BitManipulation;
using LiteNetLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [SerializeField] private Transform entitiesParent;
    [SerializeField] private List<GameObject> controlledEntityPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> entityPrefabs = new List<GameObject>();

    // may actually not want to separate these for it just making more sense as a whole,
    // just do checks for the enttity type

    // not part of entites bc, these are only updated by incomming packets
    //private Dictionary<int, EntityHandler> otherPlayers = new Dictionary<int, EntityHandler>();

    // these are only moved by the leader player, all other players just receive packets on entity info
    private List<ControlledEntityHandler> controlledEntities = new List<ControlledEntityHandler>();
    private Dictionary<int, UncontrolledEntityHandler> uncontrolledEntities = new Dictionary<int, UncontrolledEntityHandler>();

    private uint nextSequenceNumber = 0;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        SendEntitySnapshot();
        nextSequenceNumber++;
    }

    private void SendEntitySnapshot()
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

    public void SpawnUncontrolledEntity(ushort entityId, EntityType entityType, Vector3 spawnPosition)
    {
        GameObject entityGO = Instantiate(entityPrefabs[(int)entityType], spawnPosition, Quaternion.identity, entitiesParent);
        UncontrolledEntityHandler entityHandler = entityGO.GetComponent<UncontrolledEntityHandler>(); // double check that it grabs right component
        entityHandler.Init(entityId, entityType);
        switch (entityType)
        {
            case EntityType.Player:
                OtherPlayerHandler otherPlayerHandler = (OtherPlayerHandler)entityHandler;
                break;
            case EntityType.BoomBox:
                //WalkingBoxHandler walkingBoxHandler = (WalkingBoxHandler)entityHandler;
                break;
            default:
                return;
        }
        uncontrolledEntities.Add(entityId, entityHandler);
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
}
