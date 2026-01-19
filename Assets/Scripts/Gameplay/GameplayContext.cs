using System;
using System.Collections.Generic;

namespace Gameplay
{
    public class GameplayContext : IDisposable
    {
        private readonly IEnumerable<IGameplayService> _services;
        
        public GameplayContext(IEnumerable<IGameplayService> services)
        {
            _services = services;
            SetupServices();
        }
        
        private void SetupServices()
        {
            foreach (var service in _services)
             service.ProcessGameplayStart();
        }
        
        public void Dispose()
        {
            foreach (var service in _services)
                service.ProcessGameplayEnd();
        }
    }
}