using DG.Tweening;
using UnityEngine;

namespace Template.Scripts
{
    public class Money : MonoBehaviour
    {
        public void InitMoney(RectTransform whereTo)
        {
            var firstWaveTarget = new Vector3(Random.Range(-200, 200) + transform.position.x, Random.Range(-200, 200) + transform.position.y);

            Sequence mySequence = DOTween.Sequence();

            mySequence.Append(transform.DOMove(firstWaveTarget,.5f).SetEase(Ease.OutSine));
            mySequence.Append(transform.DOMove(whereTo.position, .5f).SetEase(Ease.InCubic));
            mySequence.PrependInterval(Random.Range(0, 0.5f));
            mySequence.OnComplete(() => {
                Destroy(gameObject);
            });
        }
    }
}