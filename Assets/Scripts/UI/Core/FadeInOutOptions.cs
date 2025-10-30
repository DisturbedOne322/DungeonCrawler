using UnityEngine;

namespace UI.Core
{
    [CreateAssetMenu(menuName = "UI/Effects/Fade In Out Options")]
    public class FadeInOutOptions : ScriptableObject
    {
        public FadeOptions FadeIn;
        public float StayTime;
        public FadeOptions FadeOut;
    }
}