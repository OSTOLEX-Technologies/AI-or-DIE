using System;

namespace Src
{
    public class GameState
    {
        public int Cash
        {
            get => _cash;
            set
            {
                _cash = value;
                if (_cash < 0)
                {
                    _cash = 0;
                }
            }
        }
        public DateTime Date { get; set; }

        public int PublicTrust
        {
            get => _publicTrust;
            set
            {
                _publicTrust = value;
                if (_publicTrust < 0)
                {
                    _publicTrust = 0;
                }
            }
        }

        public int AiDevelopment
        {
            get => _aiDevelopment;
            set
            {
                _aiDevelopment = value;
                if (_aiDevelopment < 0)
                {
                    _aiDevelopment = 0;
                }
            }
        }

        public int Safety
        {
            get => _safety;
            set
            {
                _safety = value;
                if (_safety < 0)
                {
                    _safety = 0;
                }
            }
        }

        private int _cash;
        private int _publicTrust;
        private int _aiDevelopment;
        private int _safety;
        
        public override string ToString()
        {
            return
                $"Cash: {Cash}, Public Trust: {PublicTrust}, AI Development: {AiDevelopment}, Safety: {Safety}, Date: {Date}";
        }
    }
}
