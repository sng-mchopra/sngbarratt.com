using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Utils
{
    public static class StringUtils
    {
        public static string RemoveDiacritics(string text)
        {
            dynamic normalizedString = text.Normalize(NormalizationForm.FormD);
            dynamic stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                dynamic unicodeCategory__1 = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory__1 != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
