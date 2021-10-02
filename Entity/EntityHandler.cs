using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityHandler : MonoBehaviour
{
    public ushort EntityId { get; private set; }
    public EntityType EntityType { get; private set; }
    public bool IsControlledEntity { get; private set; }

    public void Init(ushort entityId, EntityType entityType, bool isControlledEntity = false)
    {
        EntityId = entityId;
        EntityType = entityType;
        IsControlledEntity = isControlledEntity;
    }

    public void DestroyEntity()
    {
        Destroy(gameObject);
    }
}
