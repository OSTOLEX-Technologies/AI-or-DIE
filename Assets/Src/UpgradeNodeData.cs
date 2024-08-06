namespace Src
{
    public class UpgradeNodeData
    {
        public int Id { get; set; }
        public int PreviousNodeId { get; set; }
        public int Cost { get; set; }
        public int AiDevelopmentIncrease { get; set; }
        public int PublicOpinionIncrease { get; set; }
        public int AiDevelopmentDynamicDelta { get; set; }
        public int PublicOpinionDynamicDelta { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}