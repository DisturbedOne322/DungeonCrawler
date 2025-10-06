using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Core
{
    [Serializable]
    public class FadeOptions
    {
        [Min(0.01f)] public float FadeDuration = 0.5f;
        public Ease Ease = Ease.Linear;
    }
}