using BitManipulation;
using LiteNetLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	// add the stuff from a while ago, the world stuff, towers and tiles


	private static GameManager instance;
	public static GameManager I { get { return instance; } }

	public TurrisClientListener listener;
	public NetManager client;
	public EntityManager entityManager;

	public GameObject player;

	public bool connected = false;

	public bool IsLeader { get; private set; } = false;

	private void Awake()
	{
		if (instance != null && instance != this)
			Destroy(this.gameObject);
		else
			instance = this;
	}

	private void Start()
	{
		listener = new TurrisClientListener();
		client = new NetManager(listener);
		client.Start();
		client.Connect("75.0.193.55", 7777, "Turris");
	}

	private void Update()
	{
		client.PollEvents();
	}

	public void Send(byte[] data, DeliveryMethod deliveryMethod)
	{
		if (!connected) return;
		client.FirstPeer.Send(data, deliveryMethod);
	}

	public void SetLeader(bool value)
	{
		IsLeader = value;
	}
}



// may want to verify if client sending main snapshot packet is client server, leader,
// if someone hacked and did that, it would not be good

// Find other situations like that where you just end the game, or kick that person for doing that


// More explicitly separate network logic and game logic, and have another peice for interaction between the two

// Game logic layer is things like, EntityHandlers
// In between layer is like worldsnapshot, it needs to be its own class, bc it has advanced game-network logic
// Packet handlers are another layer in between, but dont delegate work unless the case above
// Network Listener just reads the first few bytes for routing info, and is purely network layer

// make player a prefab and have something spawn in on OnConnected in the Listener



/*
this whole protocol will only work for a non-competitive game
// im just not trusting clients with damage numbers,
they are trusted with position all types of rot, event vectors and types of events

// could add a bool to player packet saying if its walking, and if it is, display walk particles

// maybe later dont let client send its raw position, maybe a bitflag for keys pressed, but then there would be the problem of
// client side prediction and correction

// maybe bullets should have a hit point, not a shot vector

// true client bound packet types
Entity snapshots:
  position
  rotation
Entity events:
  hurt // combine with animations in a pretty way, dont assume hit, say if hit with bool or not
  then if and only if bool is true give hit data
  {
    hurt type ids are:
      bullet hit {typeOfBullet(healthDecreaseCalculatedFromThis)}
      melee hit {typeOfMelee(healthDecreaseCalculatedFromThis)}
  }
  bullet fired {vector, type of bullet}


// client-server bound packet types
Player snapshots only, but sent as entity snapshots
  Entity snapshot
Attack
{
  attack type ids are:
    bullet fire:
    melee swing: // animation played as client faces last known direction
      typeOfMelee
      bool hit anything, following only exists if true
      hit entity id
}


// ordered, unreliable
point of snapshots, read data from bitreader,
and publicly display that info for the purpose of interpolation

// reliable ordered
point of events, read data from bitreader, display publicly

make a whole snapshot packet:
  one sequence number / tick id
  that will contain a list of all entity snapshots that are being updated

Snapshot
{
  ushort id;
  List<EntitySnapshot> snapshots

}

How will snapshots incorperate all entity types,
for the purpose of updating them?
  Base entity snapshot class:
    Have an enum entity type
    Have a sequence number of that snapshot, with wrap around
    sequence number comes from whole snapshot packet, not sent individually
  New entity snapshot classes:
    They will be made for each and every discretely different
    entity type

the interpolation buffer will contain a list of snapshots
  new get pushed on end
  the first will be the


Base entity snapshot<TEntitySnapshot>
{
  ushort sequence number
  enum entity type
  vec3 pos

  // probably a better way to use generics, or not use them, this is probably ugly
  public static TEntitySnapshot abstract Serialize();
  public static TEntitySnapshot abstract Deserialize();
}
*/







/*
 * using System.Collections.Generic;

public class InterpolationBuffer
{
  private List<>
}








public abstract class EntitySnapshot
{
	public EntityType entityType;
}
public class PlayerSnapshot
{
	public Vector3 position;
	public float rotation, pitch;

	public PlayerSnapshot(BitReader bitReader)
	{
		entityType = EntityType.Player;
		float x = (bitReader.GetInt(14) - 8192) / 256f;
		float y = bitReader.GetInt(14) / 256f;
		float z = (bitReader.GetInt(14) - 8192) / 256f;
		position = new Vector3(x, y, z);
		rotation = bitReader.GetByte() 1.41176470588f;
		pitch = bitReader.GetByte() 1.41176470588f; // represents more than necessary
	}
}











public class World
{
  private Tile[,] tilemap = new Tile[17, 17];
  private Vector2Int corePos = new Vector2Int(8, 8);

}

public class WorldManager
{
  public World world = new World();
}






















public abstract class Tile
{
  public readonly bool walkable;

}

public class NoneTile : Tile
{
  public NoneTile()
  {
    walkable = false;
  }
}


public class TowerTile : Tile
{
  public TowerTile()
  {
    walkable = true;
  }
}













public class WorldPathfinder()
{
  public WorldPathfinder(World world, Vector2Int startPos)
  {

  }

  public
}


public class














public class PacketSerializer
{

}

public class PacketDeserializer
{

}
















using System.Collections;

public class InterpolationManager
{
	// entityId, interpolationBuffer
	private Dictionary<int, InterpolationBuffer> entities = new Dictionary<int, InterpolationBuffer>();

}

public class InterpolationBuffer
{
	private Queue<EntitySnapshot> buffer = new Queue<EntitySnapshot>();

}

// have something to unpack the network data to entity snapshots

public abstract class EntitySnapshot
{
	public EntityType entityType;
}

public class PlayerSnapshot
{
	public Vector3 position;
	public float rotation, pitch;

	public PlayerSnapshot(BitReader bitReader)
	{
		entityType = EntityType.Player;
		float x = (bitReader.GetInt(14) - 8192) / 256f;
		float y = bitReader.GetInt(14) / 256f;
		float z = (bitReader.GetInt(14) - 8192) / 256f;
		position = new Vector3(x, y, z);
		rotation = bitReader.GetByte() 1.41176470588f;
		pitch = bitReader.GetByte() 1.41176470588f; // represents more than necessary
	}
}

public class WalkingBoxSnapshot()
{
	public Vector3 position;
	public float rotation;

	public WalkingBoxSnapshot(BitReader  bitReader)
	{
		entityType = EntityType.WalkingBox;
		float x = (bitReader.GetInt(14) - 8192) / 256f;
		float y = bitReader.GetInt(14) / 256f;
		float z = (bitReader.GetInt(14) - 8192) / 256f;
		position = new Vector3(x, y, z);
		rotation = bitReader.GetByte() 1.41176470588f;
	}
}

public enum EntityType
{
	Player,
	WalkingBox
}
























using System.Collections;

public class InterpolationManager
{
	// entityId, interpolationBuffer
	private Dictionary<int, InterpolationBuffer> entities = new Dictionary<int, InterpolationBuffer>();

}

public class InterpolationBuffer
{
	private Queue<EntitySnapshot> buffer = new Queue<EntitySnapshot>();

}

// have something to unpack the network data to entity snapshots

public abstract class EntitySnapshot
{
	public EntityType entityType;
}

public class PlayerSnapshot
{
	public Vector3 position;
	public float rotation, pitch;

	public PlayerSnapshot(BitReader bitReader)
	{
		entityType = EntityType.Player;
		float x = (bitReader.GetInt(14) - 8192) / 256f;
		float y = bitReader.GetInt(14) / 256f;
		float z = (bitReader.GetInt(14) - 8192) / 256f;
		position = new Vector3(x, y, z);
		rotation = bitReader.GetByte() 1.41176470588f;
		pitch = bitReader.GetByte() 1.41176470588f; // represents more than necessary
	}
}

public class WalkingBoxSnapshot()
{
	public Vector3 position;
	public float rotation;

	public WalkingBoxSnapshot(BitReader  bitReader)
	{
		entityType = EntityType.WalkingBox;
		float x = (bitReader.GetInt(14) - 8192) / 256f;
		float y = bitReader.GetInt(14) / 256f;
		float z = (bitReader.GetInt(14) - 8192) / 256f;
		position = new Vector3(x, y, z);
		rotation = bitReader.GetByte() 1.41176470588f;
	}
}

public enum EntityType
{
	Player,
	WalkingBox
}

public

*/
