using DG.Tweening;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerIdleAnimator : MonoBehaviour
    {
        [SerializeField] private float _breathingAmplitude = 0.05f;
        [SerializeField] private float _breathingSpeed = 2f;

        private Tween _breathingTween;
        
        private void Start()
        {
            _breathingTween = transform.DOLocalMoveY(
                    transform.localPosition.y + _breathingAmplitude, 
                    1f / _breathingSpeed
                )
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDestroy()
        {
            _breathingTween?.Kill();
        }
    }
}