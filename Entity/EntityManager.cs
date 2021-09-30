using BitManipulation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [SerializeField] private Transform entitiesParent;
    [SerializeField] private List<GameObject> entityPrefabs = new List<GameObject>();

    // may actually not want to separate these for it just making more sense as a whole,
    // just do checks for the enttity type

    // not part of entites bc, these are only updated by incomming packets
    //private Dictionary<int, EntityHandler> otherPlayers = new Dictionary<int, EntityHandler>();

    // these are only moved by the leader player, all other players just receive packets on entity info
    private Dictionary<int, EntityHandler> entities = new Dictionary<int, EntityHandler>();

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        // possibly make enitity network manager?? Use packet handlers for now, and world snapshot

        // round robin entity updater

        // send player update, combine with normal snapshot if leader
        //if (!GameManager.I.IsLeader) return;
        foreach (KeyValuePair<int, EntityHandler> entity in entities)
        {

        }
    }

    public void SpawnEntity(ushort entityId, EntityType entityType, Vector3 spawnPosition)
    {
        GameObject entityGO = Instantiate(entityPrefabs[(int)entityType], spawnPosition, Quaternion.identity, entitiesParent);
        EntityHandler entityHandler = entityGO.GetComponent<EntityHandler>(); // double check that it grabs right component
        entityHandler.Init(entityId, entityType);
        switch (entityType)
        {
            case EntityType.Player:
                PlayerHandler playerHandler = (PlayerHandler)entityHandler;
                break;
            case EntityType.OtherPlayer:
                OtherPlayerHandler otherPlayerHandler = (OtherPlayerHandler)entityHandler;
                break;
            case EntityType.WalkingBox:
                //WalkingBoxHandler walkingBoxHandler = (WalkingBoxHandler)entityHandler;
                break;
            default:
                return;
        }
        entities.Add(entityId, entityHandler);
    }

    public void DespawnEntity(ushort entityId)
    {
        if (entities.TryGetValue(entityId, out EntityHandler entity))
        {
            entities.Remove(entityId);
            Destroy(entity.gameObject);
        }
    }

    /*
    public void NewPlayer(BitReader br)
    {
        int id = br.GetInt(8);
        Debug.Log("New Player: " + id);
        if (otherPlayers.ContainsKey(id)) return; // the thing that cuases this is bad practice only send new ones on server
        GameObject otherPlayerGO = Instantiate(otherPlayerPrefab, playerSpawn.position, Quaternion.identity, playersParent);
        OtherPlayerHandler otherPlayer = otherPlayerGO.GetComponent<OtherPlayerHandler>();
        otherPlayer.SetName($"Player {id}");
        otherPlayers.Add(id, otherPlayer);
    }
    */

    /*
    public void UpdatePlayer(BitReader br)
    {
        int id = br.GetInt(8);
        Debug.Log("Update player id: " + id);
        if (otherPlayers.TryGetValue(id, out OtherPlayerHandler otherPlayer))
        {
            float xPos = (br.GetInt(14) - 8192) / 256f;
            float yPos = (br.GetInt(14)) / 256f;
            float zPos = (br.GetInt(14) - 8192) / 256f;
            float rot = (br.GetInt(8) * 1.41176470588f);
            float pitch = (br.GetInt(8) * 1.41176470588f);
            Vector3 pos = new Vector3(xPos, yPos, zPos);
            otherPlayer.UpdatePlayer(pos, rot, pitch);
            Debug.Log($"Update Player pos: {pos}, rot: {rot}, pitch: {pitch}");
        }
    }
    */
}
