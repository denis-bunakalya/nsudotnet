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
                    bool comment = false;

                    while (s != null)
                    {
                        if (comment)
                        {
                            int end = s.IndexOf("*/");
                            if (end == -1)
                            {
                                s = streamReader.ReadLine();
                            }
                            else
                            {
                                s = s.Substring(end + 2);
                                comment = false;
                            }
                            continue;
                        }
                        s = s.Trim();
                        if ((s.Length != 0) && !s.StartsWith("//"))
                        {
                            if (!s.StartsWith("/*"))
                            {
                                i++;
                            }
                            else
                            {
                                comment = true;
                                s = s.Substring(2);
                                continue;
                            }
                        }
                        int lastCommentBeginning = s.LastIndexOf("/*");
                        if ((lastCommentBeginning != -1) && (s.Substring(lastCommentBeginning + 2).LastIndexOf("*/") == -1))
                        {
                            comment = true;
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
