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

	public PlayerSnapshot(uint sequenceNumber, BitReader bitReader)
	{
		this.sequenceNumber = sequenceNumber;
		entityType = EntityType.Player;

		entityId = bitReader.GetUShort();
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