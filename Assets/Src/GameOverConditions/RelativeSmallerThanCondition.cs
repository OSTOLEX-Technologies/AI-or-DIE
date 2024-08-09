namespace Src.GameOverConditions
{
    public class RelativeSmallerThanCondition : ICondition
    {
        private readonly int _threshold;
        private readonly float _percent;

        public RelativeSmallerThanCondition(int threshold, float percent)
        {
            _threshold = threshold;
            _percent = percent;
        }

        public bool IsSatisfied(int value)
        {
            return value < _threshold * _percent;
        }
        
        public override string ToString()
        {
            return $"Is true if value is smaller than {_percent * 100}% of {_threshold}";
        }
    }
}