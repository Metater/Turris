using BitManipulation;
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
        SendFullSnapshot();
        nextSequenceNumber++;
    }

    private void SendFullSnapshot()
    {
        if (controlledEntities.Count == 0) return;
        FullSnapshot fullSnapshot = new FullSnapshot(nextSequenceNumber);
        foreach (ControlledEntityHandler controlledEntity in controlledEntities)
        {
            fullSnapshot.
        }
    }

    public void SpawnUncontrolledEntity(ushort entityId, EntityType entityType, Vector3 spawnPosition)
    {
        GameObject entityGO = Instantiate(entityPrefabs[(int)entityType], spawnPosition, Quaternion.identity, entitiesParent);
        UncontrolledEntityHandler entityHandler = entityGO.GetComponent<UncontrolledEntityHandler>(); // double check that it grabs right component
        entityHandler.Init(entityId, entityType);
        switch (entityType)
        {
            case EntityType.Player:
                //PlayerHandler playerHandler = (PlayerHandler)entityHandler;
                break;
            case EntityType.OtherPlayer:
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

    public void HandleUncontrolledEntitySnapshot(EntitySnapshot snapshot)
    {
        if (uncontrolledEntities.TryGetValue(snapshot.entityId, out UncontrolledEntityHandler entity))
            entity.HandleSnapshot(snapshot);
    }

    public void DespawnUncontrolledEntity(ushort entityId)
    {
        if (uncontrolledEntities.TryGetValue(entityId, out UncontrolledEntityHandler entity))
        {
            uncontrolledEntities.Remove(entityId);
            entity.DestroyEntity();
        }
    }
}
