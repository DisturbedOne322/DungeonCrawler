using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Visual
{
    public class AttackAnimator : MonoBehaviour
    {
        [SerializeField] private Animation _animation;
        
        public async UniTask PlayAnimation(AnimationClip clip)
        {
            var clipLength = clip.length;
            _animation.clip = clip;
            _animation.Play();
            
            await UniTask.WaitForSeconds(clipLength);
        }
    }
}