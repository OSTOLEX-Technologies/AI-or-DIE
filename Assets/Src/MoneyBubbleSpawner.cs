using UnityEngine;

namespace Src
{
    public class MoneyBubbleSpawner : MonoBehaviour
    {
        [SerializeField] private RectTransform spawnArea;
        [SerializeField] private MoneyBubble moneyBubblePrefab;
        [SerializeField] private int spawnProbability;

        private GameState _gameState;
        private int _moneyAmount;
        
        public void Init(GameState gameState, int moneyAmount)
        {
            _gameState = gameState;
            _moneyAmount = moneyAmount;
        }
        
        public void TrySpawn()
        {
            if (Random.Range(0, 100) > spawnProbability)
            {
                return;
            }
            var rect = spawnArea.rect;
            var randomX = Random.Range(rect.xMin, rect.xMax);
            var randomY = Random.Range(rect.yMin, rect.yMax);
            var bubble = Instantiate(moneyBubblePrefab, spawnArea);
            bubble.transform.localPosition = new Vector3(randomX, randomY, 0);
            bubble.Init(_gameState, _moneyAmount);
        }
    }
}