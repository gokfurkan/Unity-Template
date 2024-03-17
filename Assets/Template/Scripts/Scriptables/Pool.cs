using System;
using System.Collections.Generic;
using UnityEngine;

namespace Template.Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "Pool", menuName = "Pooling", order = 1)]
    public class Pool : ScriptableObject
    {
        public GameObject prefab;
        public int preloadAmount = 5;
        public int incrementSize = 5;

        private Dictionary<int, GameObject> activeItems;
        private Stack<GameObject> passiveItems;
        private Transform parent;
    
        public void Setup(Transform poolParent)
        {
            parent = poolParent;
            passiveItems = new Stack<GameObject>(preloadAmount);
            activeItems = new Dictionary<int, GameObject>(preloadAmount);
            Warm(preloadAmount);
        }
    
        private void Warm(int capacity)
        {
            for (int i = 0; i < capacity; i++)
            {
                CreatePassiveItem();
            }
        }

        private void CreatePassiveItem()
        {
            GameObject go = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
            go.SetActive(false);
            passiveItems.Push(go);
        }

        public GameObject GetItem()
        {
            if (passiveItems.Count == 0)
            {
                Warm(incrementSize);
            }

            GameObject go = passiveItems.Pop();
            activeItems.Add(go.GetInstanceID(), go);
            return go;
        }

        public void PutItem(GameObject go)
        {
            int id = go.GetInstanceID();
            if (!activeItems.ContainsKey(id))
            {
                throw new Exception("object pool does not contain" + go.name);
            }
            go.SetActive(false);
            go.transform.SetParent(parent);
            activeItems.Remove(id);
            passiveItems.Push(go);
        }

        public void PutAll()
        {
            foreach (var activeItem in activeItems)
            {
                var go = activeItem.Value;
                go.SetActive(false);
                go.transform.SetParent(parent);
                passiveItems.Push(go);
            }
            activeItems.Clear();
        }
    }
}