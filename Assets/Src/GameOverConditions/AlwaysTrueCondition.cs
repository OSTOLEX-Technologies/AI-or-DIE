namespace Src.GameOverConditions
{
    public class AlwaysTrueCondition : ICondition
    {
        public bool IsSatisfied(int value)
        {
            return true;
        }
        
        public override string ToString()
        {
            return "Is always true";
        }
    }
}