using UnityEngine;

namespace Src
{
    public class UpgradesNodeTreeService
    {
        public UpgradeNode GetTree()
        {
            var root = new UpgradeNode()
            {
                Data = new UpgradeNodeData()
                {
                    Id = 0,
                    PreviousNodeId = -1,
                    Cost = 0,
                    AiDevelopmentIncrease = 0,
                    PublicOpinionIncrease = 0,
                    AiDevelopmentDynamicDelta = 0,
                    PublicOpinionDynamicDelta = 0,
                    Name = "Root",
                    Description = "Root"
                }
            };
            root.NextNodes.Add(new UpgradeNode()
            {
                Data = new UpgradeNodeData()
                {
                    Id = 1,
                    PreviousNodeId = 0,
                    Cost = 10,
                    AiDevelopmentIncrease = 10,
                    PublicOpinionIncrease = 10,
                    AiDevelopmentDynamicDelta = 1,
                    PublicOpinionDynamicDelta = 1,
                    Name = "Upgrade 1",
                    Description = "Description 1"
                }
            });
            root.NextNodes.Add(new UpgradeNode()
            {
                Data = new UpgradeNodeData()
                {
                    Id = 2,
                    PreviousNodeId = 0,
                    Cost = 20,
                    AiDevelopmentIncrease = 20,
                    PublicOpinionIncrease = 20,
                    AiDevelopmentDynamicDelta = 2,
                    PublicOpinionDynamicDelta = 2,
                    Name = "Upgrade 2",
                    Description = "Description 2"
                }
            });
            root.NextNodes[0].NextNodes.Add(new UpgradeNode()
            {
                Data = new UpgradeNodeData()
                {
                    Id = 3,
                    PreviousNodeId = 1,
                    Cost = 30,
                    AiDevelopmentIncrease = 30,
                    PublicOpinionIncrease = 30,
                    AiDevelopmentDynamicDelta = 3,
                    PublicOpinionDynamicDelta = 3,
                    Name = "Upgrade 3",
                    Description = "Description 3"
                }
            });
            root.NextNodes[0].NextNodes.Add(new UpgradeNode()
            {
                Data = new UpgradeNodeData()
                {
                    Id = 4,
                    PreviousNodeId = 1,
                    Cost = 40,
                    AiDevelopmentIncrease = 40,
                    PublicOpinionIncrease = 40,
                    AiDevelopmentDynamicDelta = 4,
                    PublicOpinionDynamicDelta = 4,
                    Name = "Upgrade 4",
                    Description = "Description 4"
                }
            });
            root.NextNodes[1].NextNodes.Add(new UpgradeNode()
            {
                Data = new UpgradeNodeData()
                {
                    Id = 5,
                    PreviousNodeId = 2,
                    Cost = 50,
                    AiDevelopmentIncrease = 50,
                    PublicOpinionIncrease = 50,
                    AiDevelopmentDynamicDelta = 5,
                    PublicOpinionDynamicDelta = 5,
                    Name = "Upgrade 5",
                    Description = "Description 5"
                }
            });
            root.NextNodes[1].NextNodes.Add(new UpgradeNode()
            {
                Data = new UpgradeNodeData()
                {
                    Id = 6,
                    PreviousNodeId = 2,
                    Cost = 60,
                    AiDevelopmentIncrease = 60,
                    PublicOpinionIncrease = 60,
                    AiDevelopmentDynamicDelta = 6,
                    PublicOpinionDynamicDelta = 6,
                    Name = "Upgrade 6",
                    Description = "Description 6"
                }
            });
            root.NextNodes[0].NextNodes[0].NextNodes.Add(new UpgradeNode()
            {
                Data = new UpgradeNodeData()
                {
                    Id = 7,
                    PreviousNodeId = 3,
                    Cost = 70,
                    AiDevelopmentIncrease = 70,
                    PublicOpinionIncrease = 70,
                    AiDevelopmentDynamicDelta = 7,
                    PublicOpinionDynamicDelta = 7,
                    Name = "Upgrade 7",
                    Description = "Description 7"
                }
            });
            root.NextNodes[0].NextNodes[0].NextNodes.Add(new UpgradeNode()
            {
                Data = new UpgradeNodeData()
                {
                    Id = 8,
                    PreviousNodeId = 3,
                    Cost = 80,
                    AiDevelopmentIncrease = 80,
                    PublicOpinionIncrease = 80,
                    AiDevelopmentDynamicDelta = 8,
                    PublicOpinionDynamicDelta = 8,
                    Name = "Upgrade 8",
                    Description = "Description 8"
                }
            });
            root.NextNodes[1].NextNodes[0].NextNodes.Add(new UpgradeNode()
            {
                Data = new UpgradeNodeData()
                {
                    Id = 9,
                    PreviousNodeId = 4,
                    Cost = 90,
                    AiDevelopmentIncrease = 90,
                    PublicOpinionIncrease = 90,
                    AiDevelopmentDynamicDelta = 9,
                    PublicOpinionDynamicDelta = 9,
                    Name = "Upgrade 9",
                    Description = "Description 9"
                }
            });
            return root;
        }   
    }
}