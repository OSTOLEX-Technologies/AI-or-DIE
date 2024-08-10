using System.Collections.Generic;
using Src.GameOverConditions;
using UnityEngine;

namespace Src
{
    public class GameOverChecker
    {
        private readonly GameOverView _view;
        private readonly List<GameOverScenario> _gameOverScenarios;

        public GameOverChecker(List<GameOverScenario> gameOverScenarios, GameOverView view)
        {
            _gameOverScenarios = gameOverScenarios;
            _view = view;
        }

        public bool CheckGameOver(GameState state)
        {
            foreach (var scenario in _gameOverScenarios)
            {
                if (!scenario.IsGameOver(state)) continue;
                _view.ShowGameOver(scenario);
                return true;
            }

            return false;
        }
    }
}