namespace Src.GameOverConditions
{
    public class EqualsCondition : ICondition
    {
        private readonly int _threshold;

        public EqualsCondition(int threshold)
        {
            _threshold = threshold;
        }

        public bool IsSatisfied(int value)
        {
            return value == _threshold;
        }
        
        public override string ToString()
        {
            return $"Is true if value is equal to {_threshold}";
        }
    }
}