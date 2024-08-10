using UnityEngine;

namespace Src
{
    public class MoneyBubble : MonoBehaviour
    {
        private GameState _gameState;
        private int _moneyAmount;
        
        public void Init(GameState gameState, int moneyAmount)
        {
            _gameState = gameState;
            _moneyAmount = moneyAmount;
        }
        
        public void Collect()
        {
            _gameState.Cash += _moneyAmount;
            Destroy(gameObject);
        }
    }
}