﻿using Template.Scripts.SO;
using UnityEngine;

namespace Template.Scripts
{
    public class ObjectPooling : Singleton<ObjectPooling>
    {
        [SerializeField] private Transform parent;
        public Pool[] poolObjects;

        protected override void Initialize()
        {
            for (var i = 0; i < poolObjects.Length; i++)
            {
                poolObjects[i] = Instantiate(poolObjects[i]);
                poolObjects[i].Setup(parent);
            }
        }
    }
}