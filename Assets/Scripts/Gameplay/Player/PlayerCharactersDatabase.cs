using System.Collections.Generic;
using Data.Constants;
using Gameplay.Configs;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Player
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayPlayer + "PlayerCharactersDatabase")]
    public class PlayerCharactersDatabase : GameplayConfig
    {
        [SerializeField] private List<PlayerUnitData> _database;
        public List<PlayerUnitData> Database => _database;
    }
}