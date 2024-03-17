using System.Collections;
using UnityEngine;

namespace Template.Scripts
{
    public class ParticleManager : Singleton<ParticleManager>
    {
        public void PlayParticle(ParticlePoolType type, Vector3 pos, Vector3 rot, bool rePush = false, float rePushDuration = 2)
        {
            var particle = GetParticleFromPool(type);
            
            particle.transform.position = pos;
            particle.transform.eulerAngles = rot;
            
            particle.gameObject.SetActive(true);
            particle.Play();

            if (rePush)
            {
                StartCoroutine(RePushParticleWithDelay(particle.gameObject, type, rePushDuration));
            }
        }
        
        private ParticleSystem GetParticleFromPool(ParticlePoolType type)
        {
            return ParticlePooling.Instance.poolObjects[(int)type].GetItem().GetComponent<ParticleSystem>();
        }
        
        private IEnumerator RePushParticleWithDelay(GameObject particle, ParticlePoolType type, float duration)
        {
            yield return new WaitForSeconds(duration);
            ParticlePooling.Instance.poolObjects[(int)type].PutItem(particle);
        }
        
        public void ResetParticles()
        {
            var particlePools = ParticlePooling.Instance.poolObjects;
            
            for (int i = 0; i < particlePools.Length; i++)
            { 
                foreach (var pool in particlePools)
                {
                    var particleController = pool.GetItem().GetComponent<ParticleController>();
                    if (particleController != null)
                    {
                        particleController.ResetParticle();
                    }
                }
            }
        }
    }
}