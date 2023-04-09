using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LaravelLangConvertor.Parser.LangParsers
{
    public class StringParser : ILangParsers
    {
        public Dictionary<string, string> GetParsedLaravelKeyValuePair(string filename)
        {
            Dictionary<string, string> languageData = new Dictionary<string, string>();
            try
            {
                
                int state = 0;
                StringBuilder sb = new StringBuilder();
                var fileHandle = File.Open(filename, FileMode.Open);
                using (var file = new StreamReader(fileHandle))
                {
                    var line = "";
                    while ((line = file.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (line.StartsWith("return ["))
                        {
                            // Start of the language array
                            continue;
                        }
                        if (line.StartsWith("];"))
                        {
                            // End of the language array
                            break;
                        }
                        if (line.StartsWith("'"))
                        {
                            // Found a key-value pair
                            var keyValue = line.Split("=>");
                            var key = keyValue[0].Trim('\'', ' ');
                            var val = keyValue[1].Trim('\'', ',', ' ');
                            languageData.Add(key, val);
                        }
                    }
                }

                // Output the key-value pairs
                foreach (var pair in languageData)
                {
                    Console.WriteLine($"{pair.Key}: {pair.Value}");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return languageData;
        }
            
    }
}
