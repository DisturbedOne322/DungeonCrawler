using System.Collections.Generic;
using Data.Constants;
using UnityEngine;

namespace Gameplay.Rewards
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayRewards + "Reward Drop Table")]
    public class RewardDropTable : ScriptableObject
    {
        [SerializeField] private List<DropEntry> _entriesList;
        [SerializeField] private DropEntry _fallbackItem;

        public List<DropEntry> EntriesList => _entriesList;
        public DropEntry FallbackItem => _fallbackItem;
    }
}