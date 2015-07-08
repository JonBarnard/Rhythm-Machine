namespace FYP_Music_Theory.Domain
{
    public sealed class Score
    {
        private int correctCount;
        private int incorrectCount;

        public double PercentageCorrect
        {
            get
            {
                if (correctCount + incorrectCount != 0)
                {
                    return (correctCount/(correctCount + (double) incorrectCount))*100.0;
                }
                return 100;
            }
        }

        public void incrementCorrect()
        {
            correctCount++;
        }

        public void incrementIncorrect()
        {
            incorrectCount++;
        }
    }
}