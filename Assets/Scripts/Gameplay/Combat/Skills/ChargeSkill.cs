using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "ChargeSkill", menuName = "Skills/General/ChargeSkill")]
    public class ChargeSkill : BaseSkill
    {
        public override async UniTask UseAction(CombatService combatService)
        {
            combatService.ApplyChargeToActiveUnit();
            await UniTask.WaitForSeconds(0.5f);
        }

        public override bool CanUse(CombatService combatService) =>
            !combatService.ActiveUnit.UnitBuffsData.Charged.Value;
    }
}