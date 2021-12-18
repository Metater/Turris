using BitManipulation;
using LiteNetLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityManager : MonoBehaviour
{
    [SerializeField] private Transform uncontrolledEntitiesParent;
    [SerializeField] private List<GameObject> uncontrolledEntityPrefabs = new List<GameObject>();

    [SerializeField] protected ControlledEntityHandler player;

    protected Dictionary<int, UncontrolledEntityHandler> uncontrolledEntities = new Dictionary<int, UncontrolledEntityHandler>();

    protected uint nextSequenceNumber = 0;

    #region UncontrolledEntityMethods
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
