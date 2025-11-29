using System;
using Cysharp.Threading.Tasks;
using Gameplay.Rewards;
using Gameplay.Skills.Core;
using PopupControllers.SkillDiscarding;

namespace PopupControllers
{
    public class SkillDiscardController
    {
        private readonly LootSkillDiscardStrategy _lootSkillDiscardStrategy;
        private readonly PurchasedSkillDiscardStrategy _purchasedSkillDiscardStrategy;

        private BaseSkill _skillToDiscard;

        public SkillDiscardController(LootSkillDiscardStrategy lootSkillDiscardStrategy,
            PurchasedSkillDiscardStrategy purchasedSkillDiscardStrategy)
        {
            _lootSkillDiscardStrategy = lootSkillDiscardStrategy;
            _purchasedSkillDiscardStrategy = purchasedSkillDiscardStrategy;
        }

        public async UniTask<BaseSkill> MakeSkillDiscardChoice(BaseSkill newSkill, ItemObtainContext context)
        {
            switch (context)
            {
                case ItemObtainContext.Loot:
                    return await _lootSkillDiscardStrategy.MakeSkillDiscardChoice(newSkill);
                case ItemObtainContext.Purchase:
                    return await _purchasedSkillDiscardStrategy.MakeSkillDiscardChoice(null);
                default:
                    throw new Exception($"Unhandled item obtain context {context}");
            }
        }
    }
}