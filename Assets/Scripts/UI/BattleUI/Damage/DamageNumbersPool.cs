using System.Collections.Generic;
using Gameplay.Services;

namespace UI.BattleUI.Damage
{
    public class DamageNumbersPool
    {
        private const int PoolSize = 10;

        private DamageNumberView _prefab;
        
        private readonly ContainerFactory _factory;
        private readonly Stack<DamageNumberView> _pool = new();

        public DamageNumbersPool(ContainerFactory factory)
        {
            _factory = factory;
        }
        
        public void Initialize(DamageNumberView prefab)
        {
            _prefab = prefab;
            
            for (int i = 0; i < PoolSize; i++) 
                _pool.Push(Create());
        }

        public DamageNumberView Get()
        {
            DamageNumberView result = null;
            if (_pool.Count > 0)
                result = _pool.Pop();
            else
                result = Create();

            result.gameObject.SetActive(true);
            
            return result;
        }

        public void Return(DamageNumberView dn)
        {
            dn.gameObject.SetActive(false);
            _pool.Push(dn);
        }

        private DamageNumberView Create()
        {
            var view = _factory.Create<DamageNumberView>(_prefab.gameObject);
            view.gameObject.SetActive(false);
            return view;
        }
    }
}