using Gameplay.StatusEffects.Core;
using Gameplay.StatusEffects.Debuffs.Core;
using Gameplay.Units;

namespace Gameplay.StatusEffects.Buffs.Services
{
    public class PlayerDebuffApplicationService
    {
        private readonly PlayerUnit _unit;
        
        public PlayerDebuffApplicationService(PlayerUnit unit)
        {
            _unit = unit;
        }
        
        public void ApplyDebuffOnPlayer(StatDebuffData data)
        {
            var instance = data.CreateInstance();
            instance.Apply(_unit, null);
        }
    }
}