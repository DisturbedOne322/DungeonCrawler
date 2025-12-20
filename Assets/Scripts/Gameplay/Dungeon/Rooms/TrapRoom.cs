using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.RoomVariants;
using Gameplay.StatusEffects.Buffs.Services;
using Gameplay.StatusEffects.Debuffs.Core;
using Gameplay.Units;
using UnityEngine;
using Zenject;

namespace Gameplay.Dungeon.Rooms
{
    public class TrapRoom : StopRoom
    {
        private PlayerUnit _playerUnit;
        private PlayerDebuffApplicationService _debuffApplicationService;
        
        private TrapRoomVariantData _roomData;

        public override RoomVariantData RoomData => _roomData;

        [Inject]
        private void Construct(PlayerUnit playerUnit, PlayerDebuffApplicationService debuffApplicationService)
        {
            _playerUnit = playerUnit;
            _debuffApplicationService = debuffApplicationService;
        }
        
        public void SetData(TrapRoomVariantData data) => _roomData = data;

        public override async UniTask ClearRoom()
        {
            await UniTask.WaitForSeconds(0.5f);
            PunishPlayer();
            await UniTask.WaitForSeconds(1.5f);
        }

        private void PunishPlayer()
        {
            var trapData = _roomData.TrapRoomData;
            float random = Random.value;
            
            if (random < trapData.DamageChance)
            {
                DealDamage();
                return;
            }

            if (!HasValidDebuffs())
                DealDamage();
            else
                ApplyDebuff();
        }
        
        private void DealDamage()
        {
            var trapData = _roomData.TrapRoomData;
            int damage = Random.Range(trapData.MinDamage, trapData.MaxDamage);
            _playerUnit.UnitHealthController.TakeDamage(damage);
        }

        private void ApplyDebuff()
        {
            _debuffApplicationService.ApplyDebuffOnPlayer(SelectDebuff());
        }

        private StatDebuffData SelectDebuff()
        {
            var trapData = _roomData.TrapRoomData;
            var debuffs = trapData.Debuffs;

            var selection = new List<StatDebuffData>(debuffs).Where(x => x).ToList();
            int index = Random.Range(0, selection.Count);
            return selection[index];
        }
        
        private bool HasValidDebuffs()
        {
            var trapData = _roomData.TrapRoomData;
            return trapData.Debuffs.Count(x => x) > 0;
        }
    }
}