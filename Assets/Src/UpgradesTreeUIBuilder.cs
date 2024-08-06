using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Src
{
    public class UpgradesTreeUIBuilder : MonoBehaviour
    {
        [SerializeField] private Transform root;
        [SerializeField] private float heightBetweenLevels;
        [SerializeField] private float widthBetweenNodes;
        [SerializeField] private GameObject nodePrefab;
        [SerializeField] private GameObject connectionPrefab;
        [SerializeField] private Transform connectionsParent;

        private void Start()
        {
            var upgradesNodeTreeService = new UpgradesNodeTreeService();
            var rootUpgradeNode = upgradesNodeTreeService.GetTree();
            var rootTransform = Instantiate(nodePrefab, parent: transform).transform;
            rootTransform.position = root.position;
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
            //Creating game objects for next level nodes
            var children = currentLevelNodes.SelectMany(node => node.NextNodes).ToList();
            var childrenTransforms = new List<Transform>();
            foreach (var child in children)
            {
                var childGameObject = Instantiate(nodePrefab, parent: transform);
                childrenTransforms.Add(childGameObject.transform);
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
        }
        
        private void AlignHorizontally(Vector3 center, List<Transform> toAlign, float distanceBetween)
        {
            var centerPosition = center;
            var count = toAlign.Count;
            var totalWidth = count * distanceBetween;
            var left = centerPosition.x - totalWidth / 2 + distanceBetween/2;
            for (var i = 0; i < count; i++)
            {
                var position = toAlign[i].position;
                position.x = left + i * distanceBetween;
                position.y = centerPosition.y;
                toAlign[i].position = position;
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
