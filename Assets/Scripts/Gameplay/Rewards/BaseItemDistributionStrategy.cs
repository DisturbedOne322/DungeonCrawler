using AssetManagement.AssetProviders.ConfigProviders;
using Cysharp.Threading.Tasks;
using Gameplay.Consumables;
using Gameplay.Equipment.Armor;
using Gameplay.Equipment.Weapons;
using Gameplay.Skills.Core;
using Gameplay.StatusEffects.Buffs.Services;
using Gameplay.StatusEffects.Core;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Rewards
{
    public abstract class BaseItemDistributionStrategy: IItemDistributionStrategy
    {
        protected readonly PlayerUnit Player;
        private readonly GameplayConfigsProvider _configsProvider;
        private readonly StatusEffectsProcessor _statusEffectsProcessor;

        public BaseItemDistributionStrategy(PlayerUnit player, GameplayConfigsProvider configsProvider, StatusEffectsProcessor statusEffectsProcessor)
        {
            Player = player;
            _configsProvider = configsProvider;
            _statusEffectsProcessor = statusEffectsProcessor;
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

        private UniTask HandleConsumable(BaseConsumable consumable, int amount)
        {
            Player.UnitInventoryData.AddItems(consumable, amount);
            return UniTask.CompletedTask;
        }

        private UniTask HandleStatusEffect(BaseStatusEffectData statusEffect)
        {
            _statusEffectsProcessor.AddStatusEffectTo(Player, statusEffect, statusEffect);
            return UniTask.CompletedTask;
        }

        private UniTask HandleCoins(CoinsItem coinsItem)
        {
            var config = _configsProvider.GetConfig<LuckTableConfig>();
            int reward = config.GetCoins(coinsItem, Player.UnitStatsData.Luck.Value);
            
            Player.UnitInventoryData.Coins.Value += reward;
            return UniTask.CompletedTask;
        }
    }
}