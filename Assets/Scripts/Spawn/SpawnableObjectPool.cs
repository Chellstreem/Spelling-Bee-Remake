using System.Collections.Generic;
using UnityEngine;

namespace Spawn
{
    public class SpawnableObjectPool
    {
        public SpawnableObjectInfo Info { get; }
        public Queue<GameObject> Pool { get; } = new();

        public SpawnableObjectPool(SpawnableObjectInfo info) => Info = info;
    }
}
