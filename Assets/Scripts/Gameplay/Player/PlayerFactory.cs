using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerFactory : MonoBehaviour
    {
        [SerializeField] private UnitData _playerUnitData;
        
        public void InitializePlayer(PlayerUnit player)
        {
            var data = _playerUnitData;
            player.InitializeUnit(data);
        }
    }
}