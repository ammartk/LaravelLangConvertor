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
        public Dictionary<string, string> GetParsedLaravelKeyValuePair(string file)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            try
            {
                var fileHandle = File.Open(file, FileMode.Open);
                using (var reader = new StreamReader(fileHandle))
                {
                    using (var stringReader = new StringReader(reader.ReadToEnd()))
                    {
                        string data;
                        while((data = stringReader.ReadLine()) != null)
                        {
                            if (data.StartsWith("<php"))
                            {
                                continue;
                            }
                            else if (data.StartsWith(">"))
                            {
                                continue;
                            }
                            
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return result;
        }
    }
}
