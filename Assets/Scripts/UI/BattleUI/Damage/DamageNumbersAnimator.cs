using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace UI.BattleUI.Damage
{
    public class DamageNumbersAnimator
    {
        public void PlayScreenAnimation(DamageNumberView view, Color targetColor, DamageNumbersPool pool)
        {
            Transform t = view.transform;
        
            t.localScale = Vector3.zero;
            var text = view.DamageText;
            view.DamageText.color = targetColor;
        
            Sequence seq = DOTween.Sequence();

            seq.Append(t.DOScale(1.2f, 0.2f).SetEase(Ease.OutBack))
                .Join(text.DOFade(1f, 0.1f))
                .Append(t.DOMoveY(t.position.y + 50f, 0.6f).SetEase(Ease.OutQuad))
                .Join(text.DOFade(0f, 0.6f))
                .AppendCallback(() =>
                {
                    pool.Return(view);
                });
        }
        
        public void PlayWorldAnimation(DamageNumberView view, Color targetColor, DamageNumbersPool pool)
        {
            Transform t = view.transform;

            t.localScale = Vector3.zero;
            var text = view.DamageText;
            text.color = targetColor;

            Sequence seq = DOTween.Sequence();

            seq.Append(t.DOScale(Vector3.one * 0.2f, 0.2f).SetEase(Ease.OutBack))
                .Join(text.DOFade(1f, 0.1f))
           
                .Append(t.DOMoveY(t.position.y + 0.5f, 0.8f).SetEase(Ease.OutQuad))
                .Join(text.DOFade(0f, 0.8f))
           
                .Join(t.DORotate(new Vector3(0, 0, Random.Range(-15f, 15f)), 0.8f, RotateMode.LocalAxisAdd))
           
                .AppendCallback(() => pool.Return(view)); 
        }
    }
}