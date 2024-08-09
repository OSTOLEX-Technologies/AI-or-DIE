using UnityEngine;

namespace Src.GameOverConditions
{
    public class GameOverScenario
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Texture2D Image { get; set; }
        
        private readonly ICondition _aiDevelopmentCondition;
        private readonly ICondition _safetyCondition;
        private readonly ICondition _publicTrustCondition;
        private readonly ICondition _moneyCondition;

        public GameOverScenario(ICondition aiDevelopmentCondition,
            ICondition safetyCondition, 
            ICondition publicTrustCondition, 
            ICondition moneyCondition)
        {
            _aiDevelopmentCondition = aiDevelopmentCondition;
            _safetyCondition = safetyCondition;
            _publicTrustCondition = publicTrustCondition;
            _moneyCondition = moneyCondition;
        }

        public bool IsGameOver(GameState state)
        {
            return _aiDevelopmentCondition.IsSatisfied(state.AiDevelopment) &&
                   _safetyCondition.IsSatisfied(state.Date.Day) &&
                   _publicTrustCondition.IsSatisfied(state.PublicTrust) &&
                   _moneyCondition.IsSatisfied(state.Cash);
        }
        
        public override string ToString()
        {
            return $"{Name} - {Description}, " +
                   $"AI Development: {_aiDevelopmentCondition}, " +
                   $"Safety: {_safetyCondition}, " +
                   $"Public Trust: {_publicTrustCondition}, " +
                   $"Money: {_moneyCondition}";
        }
    }
}