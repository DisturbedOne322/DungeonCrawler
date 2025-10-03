using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Visual
{
    public class AttackAnimator : MonoBehaviour
    {
        [SerializeField] private Animation _animation;
        
        public async UniTask PlayAnimation(AnimationClip clip)
        {
            if(!clip)
            {
                Debug.Log("Animation clip is null");
                return;
            }
            
            var clipLength = clip.length;
            _animation.clip = clip;
            _animation.AddClip(clip, clip.name);
            _animation.Play();
            
            await UniTask.WaitForSeconds(clipLength);
        }
    }
}