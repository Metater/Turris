using UnityEngine;

public static class GameActions
{
    public static void SpawnEntity(ushort entityId, EntityType entityType, Vector3 spawnPosition)
    {
        GameManager.I.entityManager.SpawnUncontrolledEntity(entityId, entityType, spawnPosition);
    }

    public static void DespawnEntity(ushort entityId)
    {
        GameManager.I.entityManager.DespawnUncontrolledEntity(entityId);
    }
}
