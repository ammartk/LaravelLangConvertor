using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaravelLangConvertor.Translator
{
    public interface ILangTranslator
    {
        public abstract Task<List<string>> TranslateToLanguage(Dictionary<string, string> values, string language);
    }
}
