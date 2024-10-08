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
        private GameOverChecker _gameOverChecker;
        private MoneyBubbleSpawner _moneyBubbleSpawner;
        private bool _isPaused;

        public void Init(GameState gameState, 
            GameOverChecker gameOverChecker,
            MoneyBubbleSpawner moneyBubbleSpawner,
            int cashDecreaseSpeed, 
            int publicTrustDecreaseSpeed, 
            int aiDevelopmentDecreaseSpeed, 
            int safetyDecreaseSpeed, 
            int oneDayInSeconds)
        {
            _gameState = gameState;
            _gameOverChecker = gameOverChecker;
            _moneyBubbleSpawner = moneyBubbleSpawner;
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
                if (_isPaused)
                {
                    yield return null;
                    continue;
                }
                yield return new WaitForSeconds(_oneDayInSeconds);
                if (_gameState == null)
                {
                    yield return null;
                    continue;
                }
                _gameState.Cash -= _cashDecreaseSpeed;
                _gameState.PublicTrust -= _publicTrustDecreaseSpeed;
                _gameState.AiDevelopment -= _aiDevelopmentDecreaseSpeed;
                _gameState.Safety -= _safetyDecreaseSpeed;
                _gameState.Date = _gameState.Date.AddDays(1);
                _moneyBubbleSpawner.TrySpawn();
                if (_gameOverChecker.CheckGameOver(_gameState))
                {
                    break;
                }
                Debug.Log(_gameState);
            }
        }
        
        public void UpdateStatsDecreaseSpeed(int aiDevelopmentDecreaseDelta, int publicTrustDecreaseDelta, int safetyDecreaseDelta)
        {
            _aiDevelopmentDecreaseSpeed -= aiDevelopmentDecreaseDelta;
            _publicTrustDecreaseSpeed -= publicTrustDecreaseDelta;
            _safetyDecreaseSpeed -= safetyDecreaseDelta;
        }
        
        public void Pause()
        {
            _isPaused = true;
        }
        
        public void Resume()
        {
            _isPaused = false;
        }
    
    }
}
