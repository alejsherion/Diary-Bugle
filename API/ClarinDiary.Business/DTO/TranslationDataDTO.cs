using System;
using System.Collections.Generic;
using System.Text;

namespace ClarinDiary.Business.DTO
{
    public class TranslationDataDTO
    {
        public TranslationDataTranslationsDTO data { get; set; }
    }

    public class TranslationDataTranslationsDTO
    {
        public List<TranslatedTextDTO> translations { get; set; }
    }

    public class TranslatedTextDTO
    {
        public string translatedText { get; set; }
    }
}
