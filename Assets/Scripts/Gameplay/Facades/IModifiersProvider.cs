using System.Collections.Generic;
using Gameplay.Combat.Modifiers;

namespace Gameplay.Facades
{
    public interface IModifiersProvider
    {
        public List<IOffensiveModifier> GetOffensiveModifiers();
        public List<IDefensiveModifier> GetDefensiveModifiers();
    }
}