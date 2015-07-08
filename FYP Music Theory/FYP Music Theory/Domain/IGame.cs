using System.Collections.Generic;
using System.Windows.Controls;

namespace FYP_Music_Theory.Domain
{
    public interface IGame
    {
        int AnsweredQuestionCount { get; }

        bool HasQuestion { get; }

        Difficulty Difficulty { get; set; }

        bool IsPhraseCorrectForQuestion(string imageTag);

        void NewQuestion(List<Image> gameImages);

        Audio CorrectAudio(int bpm);

        Score CurrentGameScore { get; }
        IEnumerable<int> QuestionAttempts { get; }
    }
}