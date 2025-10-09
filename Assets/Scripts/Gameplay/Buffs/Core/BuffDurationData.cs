using UnityEngine;

namespace Gameplay.Buffs.Core
{
    [CreateAssetMenu(fileName = "BuffDurationData", menuName = "Gameplay/Buffs/DurationType")]
    public class BuffDurationData : ScriptableObject
    {
        [SerializeField] private BuffExpirationType _expirationType;
        [SerializeField, Min(-1)] private int _turnDurations = -1;
        
        public BuffExpirationType ExpirationType => _expirationType;
        public int TurnDurations => _turnDurations;
    }
}