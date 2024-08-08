using System.Collections;
using UnityEngine;

namespace Src
{
    public class GameStateUpdater : MonoBehaviour
    {
        private int _cashDecreaseSpeed;
        private int _publicTrustDecreaseSpeed;
        private int _aiDevelopmentDecreaseSpeed;
        private int _safetyDecreaseSpeed;
        private int _oneDayInSeconds;
        private GameState _gameState;

        public void Init(GameState gameState, int cashDecreaseSpeed, int publicTrustDecreaseSpeed, int aiDevelopmentDecreaseSpeed, int safetyDecreaseSpeed, int oneDayInSeconds)
        {
            _gameState = gameState;
            _cashDecreaseSpeed = cashDecreaseSpeed;
            _publicTrustDecreaseSpeed = publicTrustDecreaseSpeed;
            _aiDevelopmentDecreaseSpeed = aiDevelopmentDecreaseSpeed;
            _safetyDecreaseSpeed = safetyDecreaseSpeed;
            _oneDayInSeconds = oneDayInSeconds;
        }
    
        private void Start()
        {
            StartCoroutine(UpdateGameState());
        }

        private IEnumerator UpdateGameState()
        {
            while (true)
            {
                yield return new WaitForSeconds(_oneDayInSeconds);
                _gameState.Cash -= _cashDecreaseSpeed;
                _gameState.PublicTrust -= _publicTrustDecreaseSpeed;
                _gameState.AiDevelopment -= _aiDevelopmentDecreaseSpeed;
                _gameState.Safety -= _safetyDecreaseSpeed;
                _gameState.Date = _gameState.Date.AddDays(1);
            }
        }
        
        public void UpdateStatsDecreaseSpeed(int aiDevelopmentDecreaseDelta, int publicTrustDecreaseDelta, int safetyDecreaseDelta)
        {
            _aiDevelopmentDecreaseSpeed -= aiDevelopmentDecreaseDelta;
            _publicTrustDecreaseSpeed -= publicTrustDecreaseDelta;
            _safetyDecreaseSpeed -= safetyDecreaseDelta;
        }
    
    }
}
