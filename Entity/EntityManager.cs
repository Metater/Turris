using BitManipulation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [SerializeField] private GameObject otherPlayerPrefab;
    [SerializeField] private Transform playerSpawn;
    [SerializeField] private Transform playersParent;

    private Dictionary<int, OtherPlayerHandler> otherPlayers = new Dictionary<int, OtherPlayerHandler>();

    private void Start()
    {

    }

    private void Update()
    {

    }

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

    public void SpawnEntity()
    {

    }

    public void UpdateEntity()
    {

    }
}
