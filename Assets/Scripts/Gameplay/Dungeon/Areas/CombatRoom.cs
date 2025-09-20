using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Combat;
using UnityEngine;
using Zenject;

namespace Gameplay.Dungeon.Areas
{
    public class CombatRoom : StopRoom
    {
        [SerializeField] private Transform _enemySpawnPoint;
        
        private CombatSequenceController _combatSequenceController;
        private EnemyFactory _enemyFactory;
        
        public override RoomType RoomType => RoomType.Combat;

        [Inject]
        private void Construct(CombatSequenceController combatSequenceController, EnemyFactory enemyFactory)
        {
            _combatSequenceController = combatSequenceController;
            _enemyFactory = enemyFactory;
        }
        
        public override void ResetRoom()
        {
            
        }

        public override async UniTask ClearRoom()
        {
            var enemy = _enemyFactory.CreateEnemy();
            enemy.transform.position = _enemySpawnPoint.position;

            await _combatSequenceController.StartCombat(enemy);
        }
    }
}