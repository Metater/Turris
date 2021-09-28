using BitManipulation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PacketHandler
{
    public abstract void Handle(BitReader bitReader);
}
