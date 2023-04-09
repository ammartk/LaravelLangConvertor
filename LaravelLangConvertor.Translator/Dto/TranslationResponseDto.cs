using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaravelLangConvertor.Translator.Dto
{
    public class TranslationResponseDto
    {
        public List<TranslationDto> translations { get; set; }
    }
    public class TranslationDto
    {
        public string detected_source_language { get; set; }
        public string text { get; set; }
    }
}
