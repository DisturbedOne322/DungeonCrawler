using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoadingDisplay : MonoBehaviour
    {
        private static LoadingDisplay _instance;
        public static LoadingDisplay Instance => _instance;

        [SerializeField] private Slider _progressSlider;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeTime = 0.5f;
        
        private void Awake()
        {
            if(_instance != null) 
                Destroy(_instance.gameObject);

            _instance = this;
        }

        public void SetProgress(float progress) => _progressSlider.value = progress;

        public async UniTask FadeIn() => await _canvasGroup.DOFade(1, _fadeTime).AsyncWaitForCompletion().AsUniTask();

        public async UniTask FadeOut() => await _canvasGroup.DOFade(0, _fadeTime).AsyncWaitForCompletion().AsUniTask();
    }
}