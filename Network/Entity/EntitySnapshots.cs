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

    public abstract void WriteOut(BitWriter bitWriter);
}

public class PlayerSnapshot : EntitySnapshot
{
	public readonly ushort entityId;
	public readonly Vector3 position;
	public readonly float rotation, pitch;

	public PlayerSnapshot(uint sequenceNumber, ushort entityId, Vector3 position, float rotation, float pitch)
    {
		entityType = EntityType.Player;

		this.sequenceNumber = sequenceNumber;
		this.entityId = entityId;
		this.position = position;
		this.rotation = rotation;
		this.pitch = pitch;
    }

	public PlayerSnapshot(BitReader bitReader)
	{
		entityType = EntityType.Player;

		sequenceNumber = bitReader.GetUInt();
		entityId = bitReader.GetUShort();
		position = bitReader.GetPosition();
		rotation = bitReader.GetDegree();
		pitch = bitReader.GetDegree(180);
	}

    public override void WriteOut(BitWriter bitWriter)
    {
		bitWriter.Put(sequenceNumber);
		bitWriter.Put(entityId);
		bitWriter.Put(position);
		bitWriter.Put(rotation);
		bitWriter.PutDegree(pitch, 180);
	}
}