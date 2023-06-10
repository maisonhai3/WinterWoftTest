using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool SharedInstance;
        
        public List<GameObject> pooledObjects;
        public GameObject objectToPool;
        
        private int amountToPool;
        private const int Buffer = 10;

        private void Awake()
        {
            SharedInstance = this;
        }

        private void Start()
        {
            SetAmountOfPool();

            Pooling();
        }

        private void SetAmountOfPool()
        {
            var gameSetting = Resources.Load<GameSettings>(Constants.GAME_SETTINGS_PATH);
            if (gameSetting)
            {
                amountToPool = gameSetting.BoardSizeX * gameSetting.BoardSizeY + Buffer;
            }
        }

        private void Pooling()
        {
            pooledObjects = new List<GameObject>();
            GameObject tmp;
            for (var i = 0; i < amountToPool; i++)
            {
                tmp = Instantiate(objectToPool, transform, true);
                tmp.transform.localScale = Vector3.one;
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
        }
        
        public GameObject GetPooledObject()
        {
            if (pooledObjects.Contains(null))
                Debug.LogError("An object is destroyed!");

            for(var i = 0; i < amountToPool; i++)
            {
                if(!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }
            return null;
        }
    }
}