using Data.Constants;
using UnityEngine;

namespace Gameplay.StatusEffects.Core
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayBuffs + "DurationType")]
    public class StatusEffectDurationData : ScriptableObject
    {
        [SerializeField] private StatusEffectExpirationType _effectExpirationType;
        [SerializeField] [Min(-1)] private int _duration = -1;

        public StatusEffectExpirationType EffectExpirationType => _effectExpirationType;
        public int Duration => _duration;
    }
}