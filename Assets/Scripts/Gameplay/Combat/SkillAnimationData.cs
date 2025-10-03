using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Combat
{
    [CreateAssetMenu(fileName = "SkillAnimationData", menuName = "Gameplay/Animations/Skills/SkillAnimationData")]
    public class SkillAnimationData : ScriptableObject
    {
        [SerializeField] private AnimationClip  _animationClip;
        [SerializeField] private List<HitTiming> _hitTimings;
        
        public AnimationClip AnimationClip => _animationClip;
        public List<HitTiming> HitTimings => _hitTimings;
        
        public float TimeInSeconds => AnimationClip.length;

        public float GetHitTime(int index)
        {
            if(index < 0 || index >= _hitTimings.Count)
                throw new Exception("Animation clip index is out of range.");
            
            return _hitTimings[index].Timing * TimeInSeconds;
        }
        
        public float GetHitDelay(int index)
        {
            if(index < 0 || index >= _hitTimings.Count)
                throw new Exception("Animation clip index is out of range.");

            if (index == 0)
                return GetHitTime(0);
            
            int prev = index - 1;
            
            return GetHitTime(index) - GetHitTime(prev);
        }
    }

    [Serializable]
    public class HitTiming
    {
        [Range(0f, 1f)] public float Timing;
    }
}