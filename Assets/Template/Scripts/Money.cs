using DG.Tweening;
using UnityEngine;

namespace Template.Scripts
{
    public class Money : MonoBehaviour
    {
        [SerializeField] private RectTransform pos;
        
        public void InitMoney(RectTransform whereTo , Transform parent)
        {
            transform.parent = parent;
            pos.anchoredPosition = Vector2.zero;
            gameObject.SetActive(true);
            
            var firstWaveTarget = new Vector3(Random.Range(-200, 200) + transform.position.x, Random.Range(-200, 200) + transform.position.y);
            
            Sequence mySequence = DOTween.Sequence();

            mySequence.Append(transform.DOMove(firstWaveTarget,.5f).SetEase(Ease.OutSine));
            mySequence.Append(transform.DOMove(whereTo.position, .5f).SetEase(Ease.InCubic));
            mySequence.PrependInterval(Random.Range(0, 0.5f));
            mySequence.OnComplete(() => {
                ObjectPooling.Instance.poolObjects[(int)ObjectPoolType.MoneyUI].PutItem(gameObject);
            });
        }
    }
}