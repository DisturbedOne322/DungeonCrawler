using Gameplay.Buffs;
using Gameplay.Buffs.DefensiveCore;
using Gameplay.Buffs.OffensiveCore;
using Gameplay.Buffs.StatBuffsCore;
using Gameplay.Combat;
using UnityEngine;

namespace Gameplay.Equipment.Weapons
{
    public abstract class BaseWeapon : BaseGameItem
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField, Space] private OffensiveBuffData _offensiveBuff;
        [SerializeField] private DefensiveBuffData _defensiveBuff;
        [SerializeField] private StatBuffData _statBuff;

        public GameObject Prefab => _prefab;
        public OffensiveBuffData OffensiveBuff => _offensiveBuff;
        public DefensiveBuffData DefensiveBuff => _defensiveBuff;
        public StatBuffData StatBuff => _statBuff;
    }
}