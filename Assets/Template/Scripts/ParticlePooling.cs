using Template.Scripts.Scriptables;
using UnityEngine;

namespace Template.Scripts
{
    public class ParticlePooling : Singleton<ParticlePooling>
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