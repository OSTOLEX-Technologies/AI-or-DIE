using System.Collections.Generic;
using Src.UpgradeNodes;

namespace Src
{
    public class UpgradeTreesBuilder
    {
        private readonly GameState _gameState;
        private readonly GameStateUpdater _gameStateUpdater;

        public UpgradeTreesBuilder(GameState gameState, GameStateUpdater gameStateUpdater)
        {
            _gameState = gameState;
            _gameStateUpdater = gameStateUpdater;
        }

        public UpgradeNode GetUpgradesTree(List<UpgradeNodeData> upgradeNodesData)
        {
            var rootData = upgradeNodesData.Find(node => node.PreviousNodeId == "");
            var rootUpgradeNode = new UpgradeNode(_gameState, _gameStateUpdater, rootData);
            BuildTree(rootUpgradeNode, upgradeNodesData);
            return rootUpgradeNode;
        }

        private void BuildTree(UpgradeNode root, List<UpgradeNodeData> upgradeNodesData)
        {
            var nextLevelNodesData = upgradeNodesData.FindAll(node => node.PreviousNodeId == root.Data.Id);
            foreach (var nextLevelNodeData in nextLevelNodesData)
            {
                var nextLevelNode = new UpgradeNode(_gameState, _gameStateUpdater, nextLevelNodeData);
                nextLevelNode.PreviousNode = root;
                root.NextNodes.Add(nextLevelNode);
                BuildTree(nextLevelNode, upgradeNodesData);
            }
        }
    }
}