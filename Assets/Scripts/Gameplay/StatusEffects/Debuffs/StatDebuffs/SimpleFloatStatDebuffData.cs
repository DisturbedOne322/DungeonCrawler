using Gameplay.StatusEffects.Debuffs.Core;
using UnityEngine;

namespace Gameplay.StatusEffects.Debuffs.StatDebuffs
{
    [CreateAssetMenu(fileName = "SimpleFloatStatDebuff",
        menuName = "Gameplay/Debuffs/StatDebuffs/SimpleFloatStatDebuffData")]
    public class SimpleFloatStatDebuffData : StatDebuffData
    {
        [SerializeField] [Min(0.01f)] private float _debuffValue = 0.01f;

        protected override float GetDebuffDelta()
        {
            return _debuffValue;
        }
    }
}