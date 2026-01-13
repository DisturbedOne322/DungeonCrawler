using Data.Constants;
using Gameplay.StatusEffects.Buffs.StatBuffsCore;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.StatBuffs
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayStatBuffs + "SimpleIntStatBuffData")]
    public class SimpleIntStatBuffData : StatBuffData
    {
        [SerializeField] [Min(1)] private int _increaseAmount;

        protected override float GetBuffDelta()
        {
            return _increaseAmount;
        }
    }
}