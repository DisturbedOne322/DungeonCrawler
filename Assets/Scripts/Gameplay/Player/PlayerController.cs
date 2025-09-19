using Cysharp.Threading.Tasks;
using Data;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        public async UniTask MoveTowards(MovementData movementData)
        {
            var currentPos = transform.position;
            var targetPos = movementData.TargetPos;

            float moveTime = movementData.MoveTime;
            
            if (!Mathf.Approximately(currentPos.x, targetPos.x))
            {
                var horizontalMovePos = new Vector3(targetPos.x, currentPos.y, currentPos.z);
                transform.DOMove(horizontalMovePos, moveTime);
                
                await UniTask.WaitForSeconds(moveTime);
            }
            
            transform.DOMove(targetPos, moveTime);
            await UniTask.WaitForSeconds(moveTime);
        }
    }
}