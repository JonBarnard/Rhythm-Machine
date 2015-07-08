using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> resources = new List<string>();


                Parser parser = new Parser(fileName);

                List<Audio> phraseAudios = new List<Audio>();

     
                phraseAudios.Add(new Audio(new Byte[10], parser.Bpm));
                

                Phrase phrase = new Phrase(phraseAudios, parser.Difficulty);

                List<Phrase> phrases = new List<Phrase>();
            }
        }
        }
    }
}