using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class UncontrolledEntityHandler : EntityHandler
{
    public abstract void HandleSnapshot(EntitySnapshot snapshot);
}