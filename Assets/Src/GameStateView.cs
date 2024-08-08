using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Src
{
    public class GameStateView : MonoBehaviour
    {
        [Header("Text Meshes")]
        [SerializeField] private TextMeshProUGUI cashTextMesh;
        [SerializeField] private TextMeshProUGUI dateTextMesh;
        [Header("Sliders")]
        [SerializeField] private Slider publicTrustSlider;
        [SerializeField] private Slider aiDevelopmentSlider;
        [SerializeField] private Slider safetySlider;
        
        private int _publicTrustMaxValue;
        private int _aiDevelopmentMaxValue;
        private int _safetyMaxValue;
        private GameState _gameState;
        
        public void Init(GameState gameState, int publicTrustMaxValue, int aiDevelopmentMaxValue, int safetyMaxValue)
        {
            _gameState = gameState;
            _publicTrustMaxValue = publicTrustMaxValue;
            _aiDevelopmentMaxValue = aiDevelopmentMaxValue;
            _safetyMaxValue = safetyMaxValue;
            UpdateView();
        }
        
        private void Update()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            if (_gameState == null)
            {
                return;
            }
            cashTextMesh.text = "$" + (_gameState.Cash) + "K";
            dateTextMesh.text = _gameState.Date.ToString("MM/dd/yyyy");
            publicTrustSlider.value = _gameState.PublicTrust/(float)_publicTrustMaxValue;
            aiDevelopmentSlider.value = _gameState.AiDevelopment/(float)_aiDevelopmentMaxValue;
            safetySlider.value = _gameState.Safety/(float)_safetyMaxValue;
        }
    }
}