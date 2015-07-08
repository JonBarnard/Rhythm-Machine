using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using FYP_Music_Theory.Utilities;

namespace FYP_Music_Theory.Domain
{
    public sealed class Game : IGame
    {
        private readonly Score gameCurrentGameScore = new Score();
        private readonly IEnumerable<Phrase> phrases;
        private Question currentQuestion;

        private readonly List<Question> gameQuestions = new List<Question>();

        public Game(Difficulty difficulty, IEnumerable<Phrase> phrasesToLoad)
        {
            Difficulty = difficulty;
            phrases = phrasesToLoad;
        }

        public int AnsweredQuestionCount { get; private set; }

        public IEnumerable<int> QuestionAttempts
        {
            get { return gameQuestions.Select(gameQuestion => gameQuestion.Attempts); }
        }

        public Difficulty Difficulty { get; set; }

        public bool IsPhraseCorrectForQuestion(string imageTag)
        {
            var imageTagValue = int.Parse(imageTag);

            if (currentQuestion != null)
            {
                currentQuestion.NewAttempt();

                if (currentQuestion.IsSelectedPhraseCorrect(imageTagValue))
                {
                    gameCurrentGameScore.incrementCorrect();

                    return true;
                }

                gameCurrentGameScore.incrementIncorrect();

                return false;
            }

            return false;
        }

        public Score CurrentGameScore
        {
            get { return gameCurrentGameScore; }
        }

        public void NewQuestion(List<Image> gameImages)
        {
            currentQuestion = new Question(phrases, Difficulty);

            gameQuestions.Add(currentQuestion);

            AssignShuffledPhrasesToImages(currentQuestion.DisplayedPhrases, gameImages);

            AnsweredQuestionCount++;
        }

        public bool HasQuestion
        {
            get { return currentQuestion != null; }
        }

        public Audio CorrectAudio(int bpm)
        {
            return currentQuestion.CorrectPhrase.GetAudio(bpm);
        }

        private static void AssignShuffledPhrasesToImages(IReadOnlyList<Phrase> shuffledPhrases, IReadOnlyList<Image> gameImages)
        {
            for (var imagePosition = 0; imagePosition < 4; imagePosition++)
            {
                gameImages[imagePosition].Source = ImageUtilities.ImageToImageSource(shuffledPhrases[imagePosition].Image);
            }
        }
    }
}