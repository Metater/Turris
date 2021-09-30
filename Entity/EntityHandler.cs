using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHandler : MonoBehaviour
{
    public ushort entityId { get; private set; }
    public EntityType entityType { get; private set; }

    public void Init(ushort entityId, EntityType entityType)
    {
        this.entityId = entityId;
        this.entityType = entityType;
    }

    public virtual void Awake()
    {

    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }
}
