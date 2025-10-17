using UnityEngine;

namespace Gameplay.Dungeon
{
    public class DecisionDoor : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        public void SetDoorIcon(Sprite sprite)
        {
            _meshRenderer.material.mainTexture = sprite.texture;
        }
    }
}