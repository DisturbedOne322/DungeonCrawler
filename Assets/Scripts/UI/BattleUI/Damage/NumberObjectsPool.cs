using System.Collections.Generic;
using Gameplay.Services;
using UnityEngine;

namespace UI.BattleUI.Damage
{
    public class NumberObjectsPool
    {
        private const int PoolSize = 10;

        private NumberObjectView _prefab;
        
        private readonly ContainerFactory _factory;
        private readonly Stack<NumberObjectView> _pool = new();

        private Transform _parent;

        public NumberObjectsPool(ContainerFactory factory)
        {
            _factory = factory;
        }
        
        public void Initialize(NumberObjectView prefab)
        {
            _parent = new GameObject("[NUMBERS POOL]").transform;
            
            _prefab = prefab;
            
            for (int i = 0; i < PoolSize; i++) 
                _pool.Push(Create());
        }

        public NumberObjectView Get()
        {
            NumberObjectView result = null;
            
            if (_pool.Count > 0)
                result = _pool.Pop();
            else
                result = Create();

            result.gameObject.SetActive(true);
            
            return result;
        }

        public void Return(NumberObjectView view)
        {
            view.gameObject.SetActive(false);
            _pool.Push(view);
            
            view.gameObject.SetActive(true);
        }

        private NumberObjectView Create()
        {
            var view = _factory.Create<NumberObjectView>(_prefab.gameObject);
            view.gameObject.SetActive(false);
            
            view.transform.SetParent(_parent);
            
            return view;
        }
    }
}