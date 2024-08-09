namespace Src.GameOverConditions
{
    public class RelativeBiggerThanCondition : ICondition
    {
        private readonly int _threshold;
        private readonly float _percent;

        public RelativeBiggerThanCondition(int threshold, float percent)
        {
            _threshold = threshold;
            _percent = percent;
        }

        public bool IsSatisfied(int value)
        {
            return value > _threshold * _percent;
        }
        
        public override string ToString()
        {
            return $"Is true if value is bigger than {_percent * 100}% of {_threshold}";
        }
    }
}