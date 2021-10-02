using BitManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class EntitySnapshot : IWritable
{
	public uint sequenceNumber; // Could add smaller sequence number with wrap around if too much bandwidth
	public EntityType entityType;
	public ushort entityId;

	public abstract void WriteOut(BitWriter bitWriter);

	public static EntitySnapshot GetEntitySnapshot(uint sequenceNumber, BitReader bitReader, Func<ushort, EntityType> getEntityType)
    {
		ushort entityId = bitReader.GetUShort();
		switch (getEntityType(entityId))
        {
			case EntityType.Player:
				return new PlayerSnapshot(sequenceNumber, entityId, bitReader);
			//case EntityType.BoomBox:
				//break;
			default:
				return null;
        }
    }
}

public class PlayerSnapshot : EntitySnapshot
{
	public readonly Vector3 position;
	public readonly float rotation, pitch;

	public PlayerSnapshot(ushort entityId, Vector3 position, float rotation, float pitch)
    {
		entityType = EntityType.Player;

		this.entityId = entityId;
		this.position = position;
		this.rotation = rotation;
		this.pitch = pitch;
    }

	public PlayerSnapshot(uint sequenceNumber, ushort entityId, BitReader bitReader)
	{
		this.sequenceNumber = sequenceNumber;
		entityType = EntityType.Player;
		this.entityId = entityId;

		position = bitReader.GetPosition();
		rotation = bitReader.GetDegree();
		pitch = bitReader.GetDegree(180f) - 90f;
	}

    public override void WriteOut(BitWriter bitWriter)
    {
		bitWriter.Put(entityId);
		bitWriter.Put(position);
		bitWriter.PutDegree(rotation);
		bitWriter.PutDegree(pitch + 90f, 180f);
	}
}