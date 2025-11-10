using Gameplay.StatusEffects.Debuffs.Core;
using UnityEngine;

namespace Gameplay.StatusEffects.Debuffs.StatDebuffs
{
    [CreateAssetMenu(fileName = "SimpleIntStatDebuffData",
        menuName = "Gameplay/Debuffs/StatDebuffs/SimpleIntStatDebuffData")]
    public class SimpleIntStatDebuffData : StatDebuffData
    {
        [SerializeField] [Min(1)] private int _debuffValue = 1;

        protected override float GetDebuffDelta()
        {
            return _debuffValue;
        }
    }
}