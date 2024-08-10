using System.Collections.Generic;
using System.Linq;
using Src.UpgradeNodes;
using UnityEngine;

namespace Src
{
    public class UpgradesTreeUIBuilder : MonoBehaviour
    {
        [SerializeField] private Transform nodesParent;
        [SerializeField] private Transform rootTransform;
        [SerializeField] private float heightBetweenLevels;
        [SerializeField] private float widthBetweenNodes;
        [SerializeField] private UpgradeNodeView nodePrefab;
        [SerializeField] private GameObject connectionPrefab;
        [SerializeField] private Transform connectionsParent;
        [SerializeField] private UpgradeNodeInfoPanel infoPanel;
        [SerializeField] private UpgradeNodeBuyButton buyButton;

        private UpgradeNodeView _rootNodeView;
        
        public void BuildTree(UpgradeNode rootUpgradeNode)
        {
            InstantiateViewsTree(rootUpgradeNode);
            PositionNodes();
            BuildConnections(_rootNodeView);
        }
        
        private void InstantiateViewsTree(UpgradeNode rootUpgradeNode)
        {
            _rootNodeView = CreateNodeView(rootUpgradeNode);
            foreach (var child in rootUpgradeNode.NextNodes)
            {
                InstantiateViewsTree(_rootNodeView, child);
            }
        }

        private void InstantiateViewsTree(UpgradeNodeView previousNodeView, UpgradeNode leafNode)
        {
            var leafNodeView = CreateNodeView(leafNode);
            previousNodeView.Children.Add(leafNodeView);
            foreach (var child in leafNode.NextNodes)
            {
                InstantiateViewsTree(leafNodeView, child);
            }
        }

        private void PositionNodes()
        {
            var rootPosition = rootTransform.position;
            _rootNodeView.transform.position = rootPosition;
            var nextLevelCenter = rootPosition + Vector3.up * heightBetweenLevels;
            var nextLevelNodes = _rootNodeView.Children.ToList();
            if (nextLevelNodes.Any())
            {
                PositionNodes(nextLevelCenter, nextLevelNodes);
            }
        }

        private void PositionNodes(Vector3 levelCenter, List<UpgradeNodeView> nodes)
        {
            AlignHorizontally(levelCenter, nodes, widthBetweenNodes);
            var nextLevelCenter = levelCenter + Vector3.up * heightBetweenLevels;
            var nextLevelNodes = nodes.SelectMany(node => node.Children).ToList();
            if (nextLevelNodes.Any())
            {
                PositionNodes(nextLevelCenter, nextLevelNodes);
            }
        }

        private void BuildConnections(UpgradeNodeView node)
        {
            foreach (var child in node.Children)
            {
                BuildConnection(node.transform, child.transform);
                BuildConnections(child);
            }
        }
        
        
        /*public void BuildTree(UpgradeNode rootUpgradeNode)
        {
            var rootNodeView = Instantiate(nodePrefab, parent: nodesParent);
            rootNodeView.Init(rootUpgradeNode, infoPanel, buyButton);
            var rootTransform = rootNodeView.transform;
            rootTransform.position = rootPosition.position;
            var currentLevelNodes = new List<UpgradeNode> {rootUpgradeNode};
            var currentLevelNodesTransforms = new List<Transform> {rootTransform};
            var nextLevelCenter = rootTransform.position;
            nextLevelCenter += Vector3.up * heightBetweenLevels;
            while (currentLevelNodes.Any())
            {
                (currentLevelNodesTransforms, currentLevelNodes) = BuildNextLevel(nextLevelCenter, currentLevelNodes, currentLevelNodesTransforms);
                nextLevelCenter += Vector3.up * heightBetweenLevels;
            }
        }

        private (List<Transform> transforms, List<UpgradeNode> nodes) BuildNextLevel(Vector3 nextLevelCenter, List<UpgradeNode> currentLevelNodes, List<Transform> currentLevelNodesTransforms)
        {
            foreach (var node in currentLevelNodes)
            {
                
            }
            
            //Creating game objects for next level nodes
            var children = currentLevelNodes.SelectMany(node => node.NextNodes).ToList();
            var childrenTransforms = new List<Transform>();
            foreach (var child in children)
            {
                var childNodeView = Instantiate(nodePrefab, parent: nodesParent);
                childNodeView.Init(child, infoPanel, buyButton);
                childrenTransforms.Add(childNodeView.transform);
            }
            
            //Aligning next level nodes game objects
            AlignHorizontally(nextLevelCenter, childrenTransforms, widthBetweenNodes);
            
            //Building connections between current level nodes and next level nodes
            var lastChildIndex = 0;
            for(int i = 0; i < currentLevelNodes.Count; i++)
            {
                var node = currentLevelNodes[i];
                var childrenCount = node.NextNodes.Count;
                for (int j = 0; j < childrenCount; j++)
                {
                    BuildConnection(currentLevelNodesTransforms[i], childrenTransforms[lastChildIndex]);
                    lastChildIndex++;
                }
            }
            
            return (childrenTransforms, children);
        }*/
        
        private UpgradeNodeView CreateNodeView(UpgradeNode node)
        {
            var nodeView = Instantiate(nodePrefab, parent: nodesParent);
            nodeView.Init(node, infoPanel, buyButton);
            return nodeView;
        }
        
        private void AlignHorizontally(Vector3 center, List<UpgradeNodeView> toAlign, float distanceBetween)
        {
            var centerPosition = center;
            var count = toAlign.Count;
            var totalWidth = count * distanceBetween;
            var left = centerPosition.x - totalWidth / 2 + distanceBetween/2;
            for (var i = 0; i < count; i++)
            {
                var position = toAlign[i].transform.position;
                position.x = left + i * distanceBetween;
                position.y = centerPosition.y;
                toAlign[i].transform.position = position;
            }
        }

        private void BuildConnection(Transform from, Transform to)
        {
            var positionFrom = from.position;
            var positionTo = to.position;
            var middlePoint = (positionFrom + positionTo) / 2;
            var distance = Vector3.Distance(positionFrom, positionTo);
            var direction = positionTo - positionFrom;
            var angle = Vector3.Angle(Vector3.right, direction);
            var connection = Instantiate(connectionPrefab, middlePoint, Quaternion.identity, parent: connectionsParent);
            var scale = connection.transform.localScale;
            connection.transform.localScale = new Vector3(distance/100, scale.y, scale.z);
            connection.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
