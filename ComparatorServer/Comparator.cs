using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparatorServer
{

    public class Comparator
    {
        private int patternLength;
        private bool caseSensitive;

        public Comparator(int patternLength, bool caseSensitive)
        {
            this.patternLength = patternLength;
            this.caseSensitive = caseSensitive;
        }

        public List<string> Compare(string source, string target)
        {
            var result = new List<string>();

            var sourceArray = source.ToCharArray();
            var targetArray = target.ToCharArray();

            var matchedCharacters = new List<char>();
            int lastMatchedIndex = 0;

            for (int sourceIndex = 0; sourceIndex < sourceArray.Count(); sourceIndex++)
            {
                
                var sourceCharacter = sourceArray[sourceIndex];

                for (; lastMatchedIndex < targetArray.Count();)
                {
                    Console.WriteLine(sourceCharacter);
                    var targetCharacter = targetArray[lastMatchedIndex];
                    Console.WriteLine(targetCharacter);
                    if (sourceCharacter == targetCharacter)
                    {
                        Console.WriteLine("match");
                        matchedCharacters.Add(sourceCharacter);
                    } else
                    {
                        Console.WriteLine(matchedCharacters.Count());
                        if (matchedCharacters.Count() >= patternLength)
                        {
                            result.Add(new string(matchedCharacters.ToArray()));
                            sourceIndex += matchedCharacters.Count();
                            matchedCharacters.Clear();
                        }  
                    }

                    lastMatchedIndex++;
                    break;
                } 
            }

            if (matchedCharacters.Count() >= patternLength)
            {
                result.Add(new string(matchedCharacters.ToArray()));
            }

            return result;
        }
    }
}
