using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Unique_Words
{
    public class Program
    {
        public static int uniqueWordCount;
        public static int numberOfWords;
        protected class WordCount
        {
            public string Key { get; set; }
            public int Total { get; set; }
        }

        /// <summary>
        /// Writes the words of a user-entered paragraph of text
        /// and the count of each word. Writes unique words to
        /// the console if there are any. Could be improved by
        /// checking for variant case, error/null checking, and
        /// specific rules for splitting paragraphs (space is the
        /// character used for splitting). Punctuation is removed from 
        /// the paragraph in order for a more accurate count of words.
        /// </summary>
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter a paragraph of text.");
                //Read the user's input and replace punctuation with 
                //empty space
                string paragraph = "";
                if(args.Length == 0)
                {
                    paragraph = Console.ReadLine();
                }
                else
                {
                    paragraph = args[0];
                }
                var cleanText = Regex.Replace(paragraph, @"[^\w\s]", "");
                //split the paragraph into a list of words
                var wordList = cleanText.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                //group like words and a count
                var groupList = wordList.GroupBy(x => x)
                    .Select(word => new WordCount{ Key = word.Key, Total = word.Count() })
                    .ToList();

                //write word and count to the console
                var uniqueList = WriteWordAndCount(groupList);

                //write unique words to the console
                WriteUniqueWords(uniqueList);

                uniqueWordCount = uniqueList.Count;
                numberOfWords = wordList.Count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        private static List<string> WriteWordAndCount(List<WordCount> groupList)
        {
            Console.WriteLine("Word and Count");

            var uniqueList = new List<string>();
            foreach (var word in groupList)
            {
                Console.WriteLine($"{word.Key} | {word.Total}");

                if (word.Total == 1)
                {
                    uniqueList.Add(word.Key);
                }
            }

            return uniqueList;
        }

        private static void WriteUniqueWords(List<string> uniqueList)
        {
            Console.WriteLine("Unique Words");
            uniqueList.ForEach(word => Console.WriteLine(word));
        }
    }
}
