using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoadingDisplay : MonoBehaviour
    {
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeTime = 0.5f;
        public static LoadingDisplay Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance.gameObject);

            Instance = this;
        }

        public void SetProgress(float progress)
        {
            _progressSlider.value = progress;
        }

        public async UniTask FadeIn()
        {
            await _canvasGroup.DOFade(1, _fadeTime).AsyncWaitForCompletion().AsUniTask();
        }

        public async UniTask FadeOut()
        {
            await _canvasGroup.DOFade(0, _fadeTime).AsyncWaitForCompletion().AsUniTask();
        }
    }
}