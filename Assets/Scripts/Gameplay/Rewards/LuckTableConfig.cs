using AYellowpaper.SerializedCollections;
using Data.Constants;
using Gameplay.Configs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Rewards
{
    [CreateAssetMenu(menuName = "Gameplay/Rewards/Luck Table")]
    public class LuckTableConfig : GameplayConfig
    {
        [SerializeField] private SerializedDictionary<ItemRarity, int> _minLuckWeights;
        [SerializeField] private SerializedDictionary<ItemRarity, int> _maxLuckWeights;
        
        [SerializeField, Space] private CoinsDropChance _minLuckCoinDropChance;
        [SerializeField] private CoinsDropChance _maxLuckCoinDropChance;
        
        public int GetWeight(ItemRarity rarity, int luck)
        {
            float t = Mathf.Clamp01((luck - 1) / 99f);
            return Mathf.RoundToInt(
                Mathf.Lerp(
                    _minLuckWeights[rarity],
                    _maxLuckWeights[rarity],
                    t
                )
            );
        }

        public int GetCoins(CoinsItem coins, int luck)
        {
            int maxStat = StatConstants.MaxStatPoints;
            int minStat = 1;
            
            float t = Mathf.InverseLerp(minStat, maxStat, luck);

            float minRange = Mathf.Lerp(_minLuckCoinDropChance.MinRange, _maxLuckCoinDropChance.MinRange, t);
            float maxRange = Mathf.Lerp(_minLuckCoinDropChance.MaxRange, _maxLuckCoinDropChance.MaxRange, t);
            
            float coinT = Random.Range(minRange, maxRange);
            
            return Mathf.CeilToInt(Mathf.Lerp(coins.MinAmount, coins.MaxAmount, coinT));
        }
    }
}