using Cysharp.Threading.Tasks;
using Gameplay.Consumables;
using Gameplay.Equipment.Armor;
using Gameplay.Equipment.Weapons;
using Gameplay.Skills.Core;
using Gameplay.StatusEffects.Core;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Rewards
{
    public abstract class BaseItemDistributionStrategy: IItemDistributionStrategy
    {
        protected readonly PlayerUnit Player;

        public BaseItemDistributionStrategy(PlayerUnit player)
        {
            Player = player;
        }
        
        public async UniTask DistributeItem(BaseGameItem item, int amount)
        {
            if (!item)
                return;

            switch (item)
            {
                case BaseWeapon weapon:
                    await HandleWeapon(weapon);
                    break;

                case BaseArmor armor:
                    await HandleArmor(armor);
                    break;

                case BaseSkill skill:
                    await HandleSkill(skill);
                    break;

                case BaseConsumable consumable:
                    await HandleConsumable(consumable, amount);
                    break;

                case BaseStatusEffectData statusEffect:
                    await HandleStatusEffect(statusEffect);
                    break;

                case CoinsItem coinsItem:
                    await HandleCoins(coinsItem);
                    break;

                default:
                    Debug.LogWarning($"Unhandled reward type: {item.name}");
                    break;
            }
        }

        protected abstract UniTask HandleWeapon(BaseWeapon weapon);
        protected abstract UniTask HandleArmor(BaseArmor armor);
        protected abstract UniTask HandleSkill(BaseSkill skill);

        protected virtual UniTask HandleConsumable(BaseConsumable consumable, int amount)
        {
            Player.UnitInventoryData.AddItems(consumable, amount);
            return UniTask.CompletedTask;
        }

        protected virtual UniTask HandleStatusEffect(BaseStatusEffectData statusEffect)
        {
            Player.UnitHeldStatusEffectsData.Add(statusEffect);
            return UniTask.CompletedTask;
        }

        protected virtual UniTask HandleCoins(CoinsItem coinsItem)
        {
            var rand = Random.Range(coinsItem.MinAmount, coinsItem.MaxAmount);
            Player.UnitInventoryData.Coins.Value += rand;
            return UniTask.CompletedTask;
        }
    }
}