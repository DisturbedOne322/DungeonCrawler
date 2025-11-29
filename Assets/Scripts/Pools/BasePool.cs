using System.Collections.Generic;
using Gameplay.Services;
using UnityEngine;

namespace Pools
{
    public class BasePool<T> where T : Component
    {
        private readonly List<T> _activeElements = new();
        private readonly ContainerFactory _factory;

        private readonly Stack<T> _pool = new();

        private Transform _parent;

        private T _prefab;

        public BasePool(ContainerFactory factory)
        {
            _factory = factory;
        }

        public void Initialize(T prefab, int poolSize = 10)
        {
            _parent = new GameObject($"[{typeof(T)} POOL]").transform;

            _prefab = prefab;

            for (var i = 0; i < poolSize; i++)
                _pool.Push(Create());
        }

        public virtual T Get()
        {
            T result = null;

            if (_pool.Count > 0)
                result = _pool.Pop();
            else
                result = Create();

            result.gameObject.SetActive(true);
            _activeElements.Add(result);

            return result;
        }

        public virtual void Return(T view, bool reparent = true)
        {
            view.gameObject.SetActive(false);

            _activeElements.Remove(view);
            _pool.Push(view);
            
            if(reparent)
                view.transform.SetParent(_parent);
        }

        public void ReturnAll()
        {
            for (var i = _activeElements.Count - 1; i >= 0; i--)
                Return(_activeElements[i]);

            _activeElements.Clear();
        }

        protected virtual T Create()
        {
            var view = _factory.Create<T>(_prefab.gameObject);
            view.gameObject.SetActive(false);

            view.transform.SetParent(_parent);

            return view;
        }
    }
}