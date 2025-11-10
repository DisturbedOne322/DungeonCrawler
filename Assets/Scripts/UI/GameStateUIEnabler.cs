using DG.Tweening;
using Gameplay.Combat.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI
{
    public class GameStateUIEnabler : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _walkCanvas;
        [SerializeField] private CanvasGroup _battleCanvas;
        [SerializeField] private float _fadeDuration = 0.5f;
        private readonly CompositeDisposable _disposable = new();

        private Sequence _sequence;

        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        [Inject]
        private void Construct(CombatEventsBus combatEventsBus)
        {
            combatEventsBus.OnCombatStarted.Subscribe(_ => ShowBattleCanvas()).AddTo(_disposable);

            combatEventsBus.OnCombatEnded.Subscribe(_ => ShowWalkCanvas()).AddTo(_disposable);
        }

        private void ShowBattleCanvas()
        {
            ChangeActiveCanvas(_walkCanvas, _battleCanvas);
        }

        private void ShowWalkCanvas()
        {
            ChangeActiveCanvas(_battleCanvas, _walkCanvas);
        }

        private void ChangeActiveCanvas(CanvasGroup oldCanvas, CanvasGroup newCanvas)
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            _sequence.Append(oldCanvas.DOFade(0, _fadeDuration));
            _sequence.Append(newCanvas.DOFade(1, _fadeDuration));
            _sequence.SetEase(Ease.InOutQuad);
            _sequence.SetLink(gameObject);
        }
    }
}