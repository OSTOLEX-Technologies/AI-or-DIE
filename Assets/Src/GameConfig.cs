using System;

namespace Src
{
    [Serializable]
    public class GameConfig
    {
        public int InitialCash;
        public int InitialPublicTrust;
        public int InitialAiDevelopment;
        public int InitialSafety;
        public DateTime InitialDate;
        public int CashDecreaseSpeed;
        public int PublicTrustDecreaseSpeed;
        public int AiDevelopmentDecreaseSpeed;
        public int SafetyDecreaseSpeed;
        public int OneDayInSeconds;
        public int PublicTrustMaxValue;
        public int AiDevelopmentMaxValue;
        public int SafetyMaxValue;
        public int CashBubbleAmount;
    }
}