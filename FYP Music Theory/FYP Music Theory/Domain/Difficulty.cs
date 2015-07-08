using System;

namespace FYP_Music_Theory.Domain
{
    [Flags]
    public enum Difficulty
    {
        None = 0,
        Easy = 1,
        Medium = 2,
        Hard = 4
    }
}