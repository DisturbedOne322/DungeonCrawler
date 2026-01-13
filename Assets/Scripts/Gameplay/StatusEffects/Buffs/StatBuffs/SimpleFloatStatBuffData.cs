using Data.Constants;
using Gameplay.StatusEffects.Buffs.StatBuffsCore;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.StatBuffs
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayStatBuffs + "SimpleFloatStatBuffData")]
    public class SimpleFloatStatBuffData : StatBuffData
    {
        [SerializeField] [Min(0f)] private float _increaseAmount;

        protected override float GetBuffDelta()
        {
            return _increaseAmount;
        }
    }
}