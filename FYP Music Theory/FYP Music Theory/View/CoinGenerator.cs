using System.Collections.Generic;
using FYP_Music_Theory.Domain;

namespace FYP_Music_Theory.View
{
    internal sealed class CoinGenerator
    {
        public void AddGameScoreToBank(Bank bank, IEnumerable<int> questionScores)
        {
            foreach (int score in questionScores)
            {
                if (score == 1)
                {
                    bank.Deposit(5);
                }
                if (score == 2)
                {
                    bank.Deposit(1);
                }
            }
        }
    }
}