using System;
using System.IO;
using System.Linq;

namespace LinesCounter
{
    class LinesCounter
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("LinesCounter must have 1 parameter: file type");
                return;
            }
            Console.WriteLine("Total number of lines: {0}", GoToDirectory(".", args[0]));
        }

        private static int GoToDirectory(string path, string type)
        {
            string[] files = Directory.GetFiles(path);
            int numberOfLines = 0;

            foreach (var file in files.Where(file => file.EndsWith(type)))
            {
                Console.Write("{0}: ", file);

                int i = 0;
                using (var streamReader = new StreamReader(file))
                {
                    string s = streamReader.ReadLine();
                    while (s != null)
                    {
                        s = s.Trim();
                        if ((s != string.Empty) && !s.StartsWith("//"))
                        {
                            i++;
                        }
                        s = streamReader.ReadLine();
                    }
                    Console.WriteLine("{0} lines", i);
                    numberOfLines += i;
                }
            }

            string[] directories = Directory.GetDirectories(path);
            numberOfLines += directories.Sum(directory => GoToDirectory(directory, type));

            return numberOfLines;
        }

    }

}
