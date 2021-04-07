using System;
using System.Collections.Generic;
using System.Linq;

namespace ComparatorServer
{

    public class Comparator
    {
        private int patternLength;
        // TODO:
        private bool caseSensitive;
        private bool convertPolishCharacters;

        public Comparator(int patternLength, bool caseSensitive)
        {
            this.patternLength = patternLength;
            this.caseSensitive = caseSensitive;
        }

        public HashSet<string> Compare(string source, string target)
        {
            // Tu trzymamy wszystkie wyniki naszego porównania.
            var result = new HashSet<string>();

            // Stringi zamienione są na tablice znaków.
            char[] sourceArray = source.ToCharArray();
            char[] targetArray = target.ToCharArray();

            // Zaczynamy sprawdzać każdy znak z pierwszego pliku.
            for (int sourceIndex = 0; sourceIndex < sourceArray.Count(); sourceIndex++)
            {
                // Dany znak z pierwszego pliku zaczyna być porównywany z każdym znakiem drugiego pliku.
                for (int targetIndex = 0; targetIndex < targetArray.Count(); targetIndex++)
                {
                    // Nie znaleziono znaku, idziemy do następnego.
                    if (sourceArray[sourceIndex] != targetArray[targetIndex])
                    {
                        continue;
                    }

                    // Znaleziono znak, teraz iterujemy po następnych znakach w obu plikach,
                    // dopóki nie napotkamy na znak, który się różni.
                    // Wtedy sprawdzamy długość znalezionego ciągu i decydujemy czy jest to rezultat,
                    // który nas interesuje.

                    // Lista trzyma znalezione pojedyncze znaki.
                    var matched = new List<char>();

                    // Indeksy pomocnicze do iteracji w pętli while.
                    int matchedSourceIndex = sourceIndex;
                    int matchedTargetIndex = targetIndex;


                    // Znak musi być równy i w obrębie indeksów dla obu plików tekstowych.
                    while (matchedTargetIndex < targetArray.Count()
                        && matchedSourceIndex < sourceArray.Count()
                        && sourceArray[matchedSourceIndex] == targetArray[matchedTargetIndex])
                    {
                        // Znaleziono znak, sprawdzamy następną parę znaków.
                        matched.Add(sourceArray[matchedSourceIndex]);
                        matchedSourceIndex++;
                        matchedTargetIndex++;
                    }

                    // Puste znaki nie zliczają się do naszego parametru `patternLength`,
                    // lecz wciąż są używane do sprawdzania równości w plikach.
                    List<char> matchesWithoutWhileLine = matched.FindAll(m => !Char.IsWhiteSpace(m));

                    // Poprawnym rezultatem jest wynik majacy długość równą `patternLength`
                    // do tej ilości nie wliczają się puste znaki.
                    if (matchesWithoutWhileLine.Count >= patternLength)
                    {
                        // Aby ładnie sformatować wynik, obcinamy puste znaki wokół niego metodą `Trim()`
                        result.Add(new string(matched.ToArray()).Trim());
                        // Po znalezionym wyniku przesuwamy nasz indeks na ostatnią znaleziony znak.
                        sourceIndex += matched.Count() - 1;
                        break;
                    }
                }
            }

            return result;
        }
    }
}
