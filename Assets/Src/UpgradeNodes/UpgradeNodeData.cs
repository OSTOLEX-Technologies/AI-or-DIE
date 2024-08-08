using UnityEngine;

namespace Src.UpgradeNodes
{
    public class UpgradeNodeData
    {
        public string Id { get; set; }
        public string PreviousNodeId { get; set; }
        public int Cost { get; set; }
        public int AiDevelopmentOneTimeChange { get; set; }
        public int PublicTrustOneTimeChange { get; set; }
        public int SafetyOneTimeChange { get; set; }
        public int AiDevelopmentDecreaseSpeedDelta { get; set; }
        public int PublicTrustDecreaseSpeedDelta { get; set; }
        public int SafetyDecreaseSpeedDelta { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Texture2D Image { get; set; }
        public Texture2D Icon { get; set; }
        
        public override string ToString()
        {
            return $"Id: {Id}, PreviousNodeId: {PreviousNodeId}, Cost: {Cost}, AiDevelopmentOneTimeChange: {AiDevelopmentOneTimeChange}, PublicTrustOneTimeChange: {PublicTrustOneTimeChange}, SafetyOneTimeChange: {SafetyOneTimeChange}, AiDevelopmentDecreaseSpeedDelta: {AiDevelopmentDecreaseSpeedDelta}, PublicTrustDecreaseSpeedDelta: {PublicTrustDecreaseSpeedDelta}, SafetyDecreaseSpeedDelta: {SafetyDecreaseSpeedDelta}, Name: {Name}, Description: {Description}";
        }
    }
}