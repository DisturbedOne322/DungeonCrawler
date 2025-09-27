using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Rewards
{
    [CreateAssetMenu(fileName = "RewardDropTable", menuName = "Gameplay/Rewards/Reward Drop Table")]
    public class RewardDropTable : ScriptableObject
    {
        [SerializeField] private List<DropEntry> _entriesList;
        
        public List<DropEntry> EntriesList => _entriesList;
    }
}