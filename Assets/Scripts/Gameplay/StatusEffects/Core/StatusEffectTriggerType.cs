namespace Gameplay.StatusEffects.Core
{
    public enum StatusEffectTriggerType
    {
        //universal conditions
        OnObtained,

        OnCombatStart,
        OnCombatEnd,
        
        //active unit conditions
        OnHit,
        OnSkillCasted,
        OnSkillUsed,
        OnCriticalHit,
        OnEvaded,
        
        OnPhysicalDamageDealt,
        OnMagicalDamageDealt,
        OnAbsoluteDamageDealt,
        
        OnHealed,
        OnMediumHealth,
        OnCriticalHealth,

        //inactive unit conditions

        OnDamageTaken,
        OnOtherSkillCasted,
        OnOtherSkillUsed,
        OnCriticalDamageTaken,
        OnMissed,
        
        OnPhysicalDamageTaken,
        OnMagicalDamageTaken,
        OnAbsoluteDamageTaken,
        
        OnOtherHealed,
        OnOtherMediumHealth,
        OnOtherCriticalHealth,
    }
}