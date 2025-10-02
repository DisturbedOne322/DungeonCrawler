using System.Collections.Generic;
using Gameplay.Progression;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Player
{
    [CreateAssetMenu(fileName = "PlayerCharactersDatabase", menuName = "Gameplay/Player/PlayerCharactersDatabase")]
    public class PlayerCharactersDatabase : GameplayConfig
    {
        [SerializeField] private List<PlayerUnitData> _database;
        public List<PlayerUnitData> Database => _database;
    }
}