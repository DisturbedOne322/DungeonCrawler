using UnityEngine;

namespace Gameplay.Combat
{
    [CreateAssetMenu(fileName = "SkillAnimationData", menuName = "Gameplay/Animations/Skills/SkillAnimationData")]
    public class SkillAnimationData : ScriptableObject
    {
        [SerializeField] private AnimationClip  _fpvAnimationClip;
        [SerializeField] private AnimationClip  _tpvAnimationClip;
        [SerializeField, Range(0,1)] private float _swingTiming = 0.5f;
        [SerializeField, Range(0,1)] private float _recoveryTiming = 0.7f;
        
        public AnimationClip FpvAnimationClip => _fpvAnimationClip;
        public AnimationClip TpvAnimationClip => _tpvAnimationClip;
        
        public float SwingTiming => _swingTiming * TimeInSeconds;
        public float RecoveryTiming => _recoveryTiming * TimeInSeconds;
        
        public float TimeInSeconds => _fpvAnimationClip.length;

        private void OnValidate()
        {
            if(_recoveryTiming < _swingTiming)
                _recoveryTiming = _swingTiming;
        }
    }
}