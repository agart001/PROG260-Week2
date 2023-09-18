using static PROG260_Week2.Utility;

namespace PROG260_Week2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string Base = BaseDir();

            Dictionary<string, string> dict = ReadAll("txt", ".txt");

            foreach (KeyValuePair<string, string> kvp in dict)
            {
                Console.WriteLine($"File: {kvp.Key}");
                Dictionary<char[], int[]> charcounts = CharCount(kvp);
                char[] chars = charcounts.First().Key.ToArray();
                int[] counts = charcounts.First().Value.ToArray();

                int index = 0;
                foreach(char c in chars)
                {
                    Console.WriteLine($"{c} : {counts[index]}");
                    index++;
                }
            };
        }
    }
}