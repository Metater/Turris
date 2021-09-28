using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherPlayerHandler : EntityHandler
{
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject hands;
    [SerializeField] private TextMesh playerName;

    [SerializeField] private Rigidbody rb;

    public override void Start()
    {
        
    }

    public override void Update()
    {
        AimTextAtPlayer();
    }

    public void SetName(string name)
    {
        playerName.text = name;
    }

    public void UpdatePlayer(Vector3 pos, float rot, float pitch)
    {
        rb.MovePosition(pos); // still need lerp
        rb.MoveRotation(Quaternion.Euler(new Vector3(0, rot, 0)));
        hands.transform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }

    public void DestroyPlayer()
    {
        Destroy(gameObject);
    }

    private void AimTextAtPlayer()
    {
        Vector3 playerPos = GameManager.I.player.transform.position;
        var delta = playerPos - transform.position;
        var angle = Mathf.Atan2(delta.x, delta.z) * Mathf.Rad2Deg;
        playerName.transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
