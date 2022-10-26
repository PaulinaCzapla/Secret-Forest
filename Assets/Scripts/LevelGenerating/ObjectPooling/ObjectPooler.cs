using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LevelGenerating.ObjectPooling
{
    public class ObjectPooler
    {
        // private List<GameObject> _pool;
        // private List<IPoolable> _activeObjects;
        //
        // public IPoolable GetFromPool()
        // {
        //     if (_pool.Count > 0)
        //     {
        //        
        //         IPoolable pooledObject = _pool[0];
        //             _pool.RemoveAt(0);
        //             
        //         pooledObject.Spawn();
        //         pooledObject.gameObject.transform.position =
        //             grid.LevelsGrid[(int) newPosition.x, (int) newPosition.y].Position;
        //         pooledObject.Reset();
        //     }
        //     else
        //     {
        //         var glade = Instantiate(gladesSo.Glades[type],
        //             grid.LevelsGrid[(int) newPosition.x, (int) newPosition.y].Position,
        //             Quaternion.Euler(Vector3.zero));
        //         newGlade = glade.GetComponent<SpawnedGlade>();
        //     }
        // }
        //
        // public void AddToPool(T obj)
        // {
        //     
        // }
        //
        // public void DestroyPool()
        // {
        //     
        // }
    }
}