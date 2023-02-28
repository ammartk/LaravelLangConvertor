using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaravelLangConvertor.Parser
{
    public  interface ILangParsers
    {
        public abstract Dictionary<string, string> GetParsedLaravelKeyValuePair(string file);
    }
}
