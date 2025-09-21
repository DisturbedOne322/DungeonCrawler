using UnityEngine;

namespace Gameplay.Dungeon
{
    public class DecisionDoor : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        
        public void SetDoorIcon(Texture2D texture) => _meshRenderer.material.mainTexture = texture;
    }
}