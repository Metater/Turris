using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherPlayerHandler : UncontrolledEntityHandler
{
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject hands;
    [SerializeField] private TextMesh playerName;

    [SerializeField] private Rigidbody rb;

    public void Awake()
    {

    }

    public void Start()
    {

    }

    public void Update()
    {
        AimTextAtPlayer();
    }

    public void SetName(string name)
    {
        playerName.text = name;
    }

    public override void HandleSnapshot(EntitySnapshot s)
    {
        // DONOW: ADD LERP
        PlayerSnapshot snapshot = (PlayerSnapshot)s;
        rb.MovePosition(snapshot.position);
        rb.MoveRotation(Quaternion.Euler(0, snapshot.rotation, 0));
        hands.transform.localRotation = Quaternion.Euler(snapshot.pitch, 0, 0);
    }

    private void AimTextAtPlayer()
    {
        if (GameManager.I.player == null) return;
        Vector3 playerPos = GameManager.I.player.transform.position;
        var delta = playerPos - transform.position;
        var angle = Mathf.Atan2(delta.x, delta.z) * Mathf.Rad2Deg;
        playerName.transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
