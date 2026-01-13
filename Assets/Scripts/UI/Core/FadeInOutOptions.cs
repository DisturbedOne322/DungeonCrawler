using Data.Constants;
using UnityEngine;

namespace UI.Core
{
    [CreateAssetMenu(menuName = MenuPaths.UIEffects + "Fade In Out Options")]
    public class FadeInOutOptions : ScriptableObject
    {
        public FadeOptions FadeIn;
        public float StayTime;
        public FadeOptions FadeOut;
    }
}