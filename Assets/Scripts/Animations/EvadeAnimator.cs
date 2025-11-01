using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Animations
{
    public class EvadeAnimator : MonoBehaviour
    {
        [SerializeField] private float _evadeDistance = 1.5f;
        [SerializeField] private float _rotateAngle = 30f;
        [SerializeField] private float _evadeDuration = 0.2f;
        [SerializeField] private float _returnDuration = 0.25f;

        [SerializeField] private Ease _evadeEase = Ease.OutQuad;
        [SerializeField] private Ease _returnEase = Ease.InOutQuad;

        private bool _inAnimation;

        public async UniTask PlayEvadeAnimation()
        {
            if (_inAnimation)
                return;

            _inAnimation = true;

            var direction = Random.value > 0.5f ? 1 : -1;

            var originalPos = transform.localPosition;
            var originalRot = transform.localRotation;

            var targetPos = originalPos + transform.right * (direction * _evadeDistance);
            var targetRot = Quaternion.Euler(
                originalRot.eulerAngles.x,
                originalRot.eulerAngles.y + _rotateAngle * direction,
                originalRot.eulerAngles.z
            );

            transform.DOKill();

            var seq = DOTween.Sequence();

            seq.Append(transform.DOLocalMove(targetPos, _evadeDuration)
                .SetEase(_evadeEase));
            seq.Join(transform.DOLocalRotateQuaternion(targetRot, _evadeDuration)
                .SetEase(_evadeEase));

            seq.Append(transform.DOLocalMove(originalPos, _returnDuration)
                .SetEase(_returnEase));
            seq.Join(transform.DOLocalRotateQuaternion(originalRot, _returnDuration)
                .SetEase(_returnEase));

            seq.SetLink(gameObject);

            await seq.AsyncWaitForCompletion().AsUniTask();

            _inAnimation = false;
        }
    }
}