using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        public async UniTask MoveTowards(Vector3 targetPosition, float moveTime, float rotateTime)
        {
            var direction = (targetPosition - transform.position).normalized;

            if (Vector3.Dot(direction, transform.forward) < 0.99f)
            {
                transform.DORotate(direction, rotateTime);
                await UniTask.WaitForSeconds(rotateTime);
            }
            
            transform.DOMove(targetPosition, moveTime);
            await UniTask.WaitForSeconds(moveTime);
        }
    }
}