using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaravelLangConvertor.Translator.Dto
{
    public class TranslationRequest
    {
        public List<string> text { get; set; }
        public string target_lang { get; set; }
    }
}
