using DG.Tweening;
using UnityEngine;

namespace UI.BattleUI.Damage
{
    public class NumberObjectsAnimator
    {
        public void PlayScreenAnimation(NumberObjectView objectView, Color targetColor, NumberObjectsPool pool)
        {
            var t = objectView.transform;

            t.localScale = Vector3.zero;
            var text = objectView.NumberText;
            objectView.NumberText.color = targetColor;

            var seq = DOTween.Sequence();

            seq.Append(t.DOScale(1.2f, 0.2f).SetEase(Ease.OutBack))
                .Join(text.DOFade(1f, 0.1f))
                .Append(t.DOMoveY(t.position.y + 50f, 0.6f).SetEase(Ease.OutQuad))
                .Join(text.DOFade(0f, 0.6f))
                .AppendCallback(() => { pool.Return(objectView); });
        }

        public void PlayWorldAnimation(NumberObjectView objectView, Color targetColor, NumberObjectsPool pool)
        {
            var t = objectView.transform;

            t.localScale = Vector3.zero;
            var text = objectView.NumberText;
            text.color = targetColor;

            var seq = DOTween.Sequence();

            seq.Append(t.DOScale(Vector3.one * 0.2f, 0.2f).SetEase(Ease.OutBack))
                .Join(text.DOFade(1f, 0.1f))
                .Append(t.DOMoveY(t.position.y + 0.5f, 0.8f).SetEase(Ease.OutQuad))
                .Join(text.DOFade(0f, 0.8f))
                .Join(t.DORotate(new Vector3(0, 0, Random.Range(-15f, 15f)), 0.8f, RotateMode.LocalAxisAdd))
                .AppendCallback(() => pool.Return(objectView));
        }
    }
}