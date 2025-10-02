using TMPro;
using UnityEngine;

namespace UI.Stats
{
    public class StatNameDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _statNameText;
        
        public void Initialize(string statName) => _statNameText.text = statName;
    }
}