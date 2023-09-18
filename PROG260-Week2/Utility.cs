using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Collections.Concurrent;
using System.Collections;

namespace PROG260_Week2
{
    public static class Utility
    {
        public static DirectoryInfo UseableBaseDir = new DirectoryInfo(BaseDir());

        public static string BaseDir()
        {
            string str = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));

            return str;
        }

        public static int GetLetterCount(string input, char lookup)
        {
            char[] chars = input.ToCharArray();
            return chars.Where(c => c == lookup).Count();
        }

        public static int[] CountsArray(char[] chars, string lookup)
        {
            int[] result = new int[chars.Length];
            int index = 0;
            foreach(char c in chars)
            { 
                result[index] = GetLetterCount(lookup, c);
                index++;
            }
            return result;
        }

       
        public static Dictionary<string, string> ReadAll(string dir, string type)
        {
            ConcurrentDictionary<string, string> FileLines = new ConcurrentDictionary<string, string>();

            string path = Path.Combine(UseableBaseDir.FullName, dir);

            List<string> paths = Directory.EnumerateFiles(path, $"*{type}").ToList();

            Parallel.ForEach(paths, current => {
                string[] strings = File.ReadAllLines(current);
                string filestring = strings.Aggregate((sum, next) => sum += next);

                FileLines.TryAdd(Path.GetFileName(current), filestring);
            });

            return FileLines.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        
        public static Dictionary<char[], int[]> CharCount(KeyValuePair<string, string> keyValuePair)
        {
            Dictionary<char[], int[]> CharCounts = new Dictionary<char[], int[]>();

            char[] distinctChars = keyValuePair.Value.Distinct().ToArray();
            int[] charCounts = CountsArray(distinctChars, keyValuePair.Value);

            CharCounts.Add(distinctChars, charCounts);
            
            return CharCounts;
        } 
    }
}
