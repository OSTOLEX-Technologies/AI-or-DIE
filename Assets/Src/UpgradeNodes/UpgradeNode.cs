using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Src.UpgradeNodes
{
    public class UpgradeNode
    {
        private GameState _gameState;
        private GameStateUpdater _gameStateUpdater;
        private bool _isBought;
        private UpgradeNode _previousNode;
        
        public UpgradeNodeData Data { get; }

        [CanBeNull]
        public UpgradeNode PreviousNode
        {
            get => _previousNode;
            set
            {
                if (_previousNode != null)
                {
                    _previousNode.Bought -= OnPreviousNodeBought;
                }
                _previousNode = value;
                if (_previousNode != null)
                {
                    _previousNode.Bought += OnPreviousNodeBought;
                }
            }
        }
        
        public List<UpgradeNode> NextNodes { get; } = new();
        public bool IsAvailable => PreviousNode == null || PreviousNode.IsBought;
        public bool IsBought => _isBought;
        
        public event Action<UpgradeNode> Bought;
        public event Action MadeAvailable;
        
        public UpgradeNode(GameState gameState, GameStateUpdater gameStateUpdater, UpgradeNodeData data)
        {
            _gameState = gameState;
            _gameStateUpdater = gameStateUpdater;
            Data = data;
        }
        
        public void Buy()
        {
            if (!IsAvailable)
            {
                return;
            }
            if (_gameState.Cash < Data.Cost)
            {
                return;
            }
            _gameState.Cash -= Data.Cost;
            _gameState.AiDevelopment += Data.AiDevelopmentOneTimeChange;
            _gameState.PublicTrust += Data.PublicTrustOneTimeChange;
            _gameState.Safety += Data.SafetyOneTimeChange;
            _gameStateUpdater.UpdateStatsDecreaseSpeed(Data.AiDevelopmentDecreaseSpeedDelta, 
                Data.PublicTrustDecreaseSpeedDelta, 
                Data.SafetyDecreaseSpeedDelta);
            _isBought = true;
            Debug.Log("Bought: " + Data);
            Bought?.Invoke(this);
        }
        
        private void OnPreviousNodeBought(UpgradeNode previousNode)
        {
            MadeAvailable?.Invoke();
        }
        
    }
}