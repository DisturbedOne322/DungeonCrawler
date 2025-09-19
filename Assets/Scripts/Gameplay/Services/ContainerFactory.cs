using UnityEngine;
using Zenject;

namespace Gameplay.Services
{
    public class ContainerFactory
    {
        private readonly DiContainer _container;

        public ContainerFactory(DiContainer container) => _container = container;

        public T Create<T>(GameObject prefab) => _container.InstantiatePrefabForComponent<T>(prefab);
    }
}