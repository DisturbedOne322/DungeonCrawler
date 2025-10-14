using Gameplay.Services;
using Pools;

namespace UI.BattleUI.Damage
{
    public class NumberObjectsPool : BasePool<NumberObjectView>
    {
        public NumberObjectsPool(ContainerFactory factory) : base(factory)
        {
        }
    }
}