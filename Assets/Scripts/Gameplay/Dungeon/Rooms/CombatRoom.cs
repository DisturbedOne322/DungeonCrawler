using System;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Combat;
using Gameplay.Enemies;
using Gameplay.Units;
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

        private EnemyUnit _enemy;

        [Inject]
        private void Construct(CombatSequenceController combatSequenceController, EnemyFactory enemyFactory)
        {
            _combatSequenceController = combatSequenceController;
            _enemyFactory = enemyFactory;
        }
        
        public override void SetupRoom()
        {
            _enemy = _enemyFactory.CreateEnemy();
            
            _enemy.transform.SetParent(_enemySpawnPoint, false);
            _enemy.transform.localPosition = Vector3.zero;
        }

        public override async UniTask PlayEnterSequence()
        {
            await _enemy.PlayAppearAnimation();
        }

        public override async UniTask ClearRoom()
        {
            await _combatSequenceController.StartCombat(_enemy);
            Destroy(_enemy.gameObject);
        }
    }
}