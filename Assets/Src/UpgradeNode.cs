using System.Collections.Generic;
using JetBrains.Annotations;

namespace Src
{
    public class UpgradeNode
    {
        public UpgradeNodeData Data { get; set; }
        [CanBeNull] public UpgradeNode PreviousNode { get; set; }
        public List<UpgradeNode> NextNodes { get; set; } = new();
    }
}