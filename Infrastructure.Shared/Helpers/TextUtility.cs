using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Shared.Helpers
{
    public static class TextUtility
    {
        /// <summary>
        /// Transform Text To Camel Casing
        /// </summary>
        /// <param name="str">String To Be Camel Cased</param>
        /// <returns></returns>
        public static string CamelCaseTranslation(string str)
        {
            var strs = str.Split(' ');
            if (strs.Count() > 1)
            {
                string returnString = string.Empty;
                foreach (var item in strs)
                {
                    returnString = returnString + " " + item.First().ToString().ToUpper() + String.Join("", item.Skip(1));
                }
                return returnString.TrimStart(' ');
            }
            else
                return strs[0].First().ToString().ToUpper() + String.Join("", strs[0].Skip(1));
        }

        /// <summary>
        /// Get Full Name From First, Middle And Last Name
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="middleName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public static string GetFullName(string firstName, string middleName, string lastName)
        {
            return string.Concat(firstName, string.IsNullOrEmpty(middleName) ? "" : " " + middleName, " " + lastName);
        }
        public static string FormatToStandardDate(string dateTimeValue)
        {
            if (string.IsNullOrEmpty(dateTimeValue))
                return null;
            try
            {
                return Convert.ToDateTime(dateTimeValue).ToString("yyyy-MM-dd");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return "";
            }
        }
        public static string GetNepaliNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "";
            }
            var Num = "";
            for (int i = 0; i < value.Length; i++)
            {
                var result = value.Substring(i, 1);
                result = result.Replace("0", "०");
                result = result.Replace("1", "१");
                result = result.Replace("2", "२");
                result = result.Replace("3", "३");
                result = result.Replace("4", "४");
                result = result.Replace("5", "५");
                result = result.Replace("6", "६");
                result = result.Replace("7", "७");
                result = result.Replace("8", "८");
                result = result.Replace("9", "९");
                Num += result;
            }
            return Num;
        }

        #region Number To Words In English
        public static string ConvertToWordsInEnglish(string number)
        {
            var numberText = new NumberTextInEnglish();
            decimal amountToConvert = Convert.ToDecimal(number);
            amountToConvert = Math.Round(amountToConvert, 2);
            string amountValue = amountToConvert.ToString();
            string numberInWords = "";

            var rupeeAndPaisa = amountValue.Split('.');
            if (rupeeAndPaisa.Count() > 1 && Convert.ToDouble(rupeeAndPaisa[1]) > 0)
            {
                var rupeePart = Convert.ToInt64(rupeeAndPaisa[0]);
                var rupeeWord = numberText.ToEnglishText(rupeePart);
                var paisaPart = Convert.ToInt64(rupeeAndPaisa[1]);
                var paisaWord = numberText.ToEnglishText(paisaPart);
                numberInWords = rupeeWord + " " + paisaWord + " Paisa";
            }
            else
            {
                var rupeePart = Convert.ToInt64(rupeeAndPaisa[0]);
                var rupeeWord = numberText.ToEnglishText(rupeePart);
                numberInWords = rupeeWord;
            }
            return numberInWords;
        }

        public class NumberTextInEnglish
        {

            private Dictionary<long, string> textStrings = new Dictionary<long, string>();
            private Dictionary<long, string> scales = new Dictionary<long, string>();
            private StringBuilder builder;

            public NumberTextInEnglish()
            {
                Initialize();
            }

            public string ToEnglishText(long num)
            {
                builder = new StringBuilder();

                if (num == 0)
                {
                    builder.Append(textStrings[num]);
                    return builder.ToString();
                }

                num = scales.Aggregate(num, (current, scale) => Append(current, scale.Key));
                AppendLessThanOneThousand(num);

                return builder.ToString().Trim();
            }

            private long Append(long num, long scale)
            {
                if (num > scale - 1)
                {
                    var baseScale = ((long)(num / scale));
                    AppendLessThanOneThousand(baseScale);
                    builder.AppendFormat("{0} ", scales[scale]);
                    num = num - (baseScale * scale);
                }
                return num;
            }

            private long AppendLessThanOneThousand(long num)
            {
                num = AppendHundreds(num);
                num = AppendTens(num);
                AppendUnits(num);
                return num;
            }

            private void AppendUnits(long num)
            {
                if (num > 0)
                {
                    builder.AppendFormat("{0} ", textStrings[num]);
                }
            }

            private long AppendTens(long num)
            {
                if (num > 20)
                {
                    var tens = ((long)(num / 10)) * 10;
                    builder.AppendFormat("{0} ", textStrings[tens]);
                    num = num - tens;
                }
                return num;
            }

            private long AppendHundreds(long num)
            {
                if (num > 99)
                {
                    var hundreds = ((long)(num / 100));
                    builder.AppendFormat("{0} hundred ", textStrings[hundreds]);
                    num = num - (hundreds * 100);
                }
                return num;
            }

            private void Initialize()
            {
                textStrings.Add(0, "Zero");
                textStrings.Add(1, "One");
                textStrings.Add(2, "Two");
                textStrings.Add(3, "Three");
                textStrings.Add(4, "Four");
                textStrings.Add(5, "Five");
                textStrings.Add(6, "Six");
                textStrings.Add(7, "Seven");
                textStrings.Add(8, "Eight");
                textStrings.Add(9, "Nine");
                textStrings.Add(10, "Ten");
                textStrings.Add(11, "Eleven");
                textStrings.Add(12, "Twelve");
                textStrings.Add(13, "Thirteen");
                textStrings.Add(14, "Fourteen");
                textStrings.Add(15, "Fifteen");
                textStrings.Add(16, "Sixteen");
                textStrings.Add(17, "Seventeen");
                textStrings.Add(18, "Eighteen");
                textStrings.Add(19, "Nineteen");
                textStrings.Add(20, "Twenty");
                textStrings.Add(30, "Thirty");
                textStrings.Add(40, "Forty");
                textStrings.Add(50, "Fifty");
                textStrings.Add(60, "Sixty");
                textStrings.Add(70, "Seventy");
                textStrings.Add(80, "Eighty");
                textStrings.Add(90, "Ninety");
                textStrings.Add(100, "Hundred");

                scales.Add(100000000000, "Kharab");
                scales.Add(1000000000, "Arab");
                scales.Add(10000000, "Crore");
                scales.Add(100000, "Lakh");
                scales.Add(1000, "Thousand");
            }
        }

        #endregion

        #region Number to Words In Nepali
        public static string ConvertToWordsInNepali(string number)
        {
            var numberText = new NumberTextInNepali();
            decimal amountToConvert = Convert.ToDecimal(number);
            amountToConvert = Math.Round(amountToConvert, 2);
            string amountValue = amountToConvert.ToString();
            string numberInWords = "";
            var rupeeAndPaisa = amountValue.Split('.');
            if (rupeeAndPaisa.Count() > 1 && Convert.ToInt64(rupeeAndPaisa[1]) > 0)
            {
                var rupeePart = Convert.ToInt64(rupeeAndPaisa[0]);
                var rupeeWord = numberText.ToNepaliText(rupeePart);
                var paisaPart = Convert.ToInt64(rupeeAndPaisa[1]);
                var paisaWord = numberText.ToNepaliText(paisaPart);
                numberInWords = rupeeWord + " " + paisaWord + " पैसा";
            }
            else
            {
                var rupeePart = Convert.ToInt64(rupeeAndPaisa[0]);
                var rupeeWord = numberText.ToNepaliText(rupeePart);
                numberInWords = rupeeWord;
            }
            return numberInWords;
        }

        public class NumberTextInNepali
        {
            private Dictionary<long, string> textStrings = new Dictionary<long, string>();
            private Dictionary<long, string> scales = new Dictionary<long, string>();
            private StringBuilder builder;

            public NumberTextInNepali()
            {
                Initialize();
            }

            public string ToNepaliText(long num)
            {
                builder = new StringBuilder();

                if (num == 0)
                {
                    builder.Append(textStrings[num]);
                    return builder.ToString();
                }

                num = scales.Aggregate(num, (current, scale) => Append(current, scale.Key));
                AppendLessThanOneThousand(num);

                return builder.ToString().Trim();
            }

            private long Append(long num, long scale)
            {
                if (num > scale - 1)
                {
                    var baseScale = ((long)(num / scale));
                    AppendLessThanOneThousand(baseScale);
                    builder.AppendFormat("{0} ", scales[scale]);
                    num = num - (baseScale * scale);
                }
                return num;
            }

            private long AppendLessThanOneThousand(long num)
            {
                num = AppendHundreds(num);
                num = AppendTens(num);
                AppendUnits(num);
                return num;
            }

            private void AppendUnits(long num)
            {
                if (num > 0)
                {
                    builder.AppendFormat("{0} ", textStrings[num]);
                }
            }

            private long AppendTens(long num)
            {
                if (num > 20)
                {
                    builder.AppendFormat("{0} ", textStrings[num]);
                    num = num - num;
                }
                return num;
            }

            private long AppendHundreds(long num)
            {
                if (num > 99)
                {
                    var hundreds = ((long)(num / 100));
                    builder.AppendFormat("{0} सय ", textStrings[hundreds]);
                    num = num - (hundreds * 100);
                }
                return num;
            }

            private void Initialize()
            {
                textStrings.Add(0, "सुन्य");
                textStrings.Add(1, "एक");
                textStrings.Add(2, "दुई");
                textStrings.Add(3, "तीन");
                textStrings.Add(4, "चार");
                textStrings.Add(5, "पाँच");
                textStrings.Add(6, "छ");
                textStrings.Add(7, "सात");
                textStrings.Add(8, "आठ");
                textStrings.Add(9, "नौ");
                textStrings.Add(10, "दस");
                textStrings.Add(11, "एघार");
                textStrings.Add(12, "बाह्र");
                textStrings.Add(13, "तेह्र");
                textStrings.Add(14, "चौध");
                textStrings.Add(15, "पन्ध्र");
                textStrings.Add(16, "सोह्र");
                textStrings.Add(17, "सत्र");
                textStrings.Add(18, "अठाह्र");
                textStrings.Add(19, "उन्नाइस");
                textStrings.Add(20, "बीस");
                textStrings.Add(21, "एकाइस");
                textStrings.Add(22, "बाइस");
                textStrings.Add(23, "तेइस");
                textStrings.Add(24, "चौबीस");
                textStrings.Add(25, "पचीस");
                textStrings.Add(26, "छब्बीस");
                textStrings.Add(27, "सत्ताइस");
                textStrings.Add(28, "अठ्ठाइस");
                textStrings.Add(29, "उनन्तीस");
                textStrings.Add(30, "तीस");
                textStrings.Add(31, "एकतीस");
                textStrings.Add(32, "बतीस");
                textStrings.Add(33, "तेतीस");
                textStrings.Add(34, "चौतीस");
                textStrings.Add(35, "पैतीस");
                textStrings.Add(36, "छतीस");
                textStrings.Add(37, "सरतीस");
                textStrings.Add(38, "अरतीस");
                textStrings.Add(39, "उननचालीस");
                textStrings.Add(40, "चालीस");
                textStrings.Add(41, "एकचालीस");
                textStrings.Add(42, "बयालिस");
                textStrings.Add(43, "तीरचालीस");
                textStrings.Add(44, "चौवालिस");
                textStrings.Add(45, "पैंतालिस");
                textStrings.Add(46, "छयालिस");
                textStrings.Add(47, "सरचालीस");
                textStrings.Add(48, "अरचालीस");
                textStrings.Add(49, "उननचास");
                textStrings.Add(50, "पचास");
                textStrings.Add(51, "एकाउन्न");
                textStrings.Add(52, "बाउन्न");
                textStrings.Add(53, "त्रिपन्न");
                textStrings.Add(54, "चौवन्न");
                textStrings.Add(55, "पच्पन्न");
                textStrings.Add(56, "छपन्न");
                textStrings.Add(57, "सन्ताउन्न");
                textStrings.Add(58, "अन्ठाउँन्न");
                textStrings.Add(59, "उनान्न्साठी ");
                textStrings.Add(60, "साठी");
                textStrings.Add(61, "एकसाठी");
                textStrings.Add(62, "बासाठी");
                textStrings.Add(63, "तीरसाठी");
                textStrings.Add(64, "चौंसाठी");
                textStrings.Add(65, "पैसाठी");
                textStrings.Add(66, "छैसठी");
                textStrings.Add(67, "सत्सठ्ठी");
                textStrings.Add(68, "अर्सठ्ठी");
                textStrings.Add(69, "उनन्सत्तरी");
                textStrings.Add(70, "सतरी");
                textStrings.Add(71, "एकहत्तर");
                textStrings.Add(72, "बहत्तर");
                textStrings.Add(73, "त्रिहत्तर");
                textStrings.Add(74, "चौहत्तर");
                textStrings.Add(75, "पचहत्तर");
                textStrings.Add(76, "छहत्तर");
                textStrings.Add(77, "सत्हत्तर");
                textStrings.Add(78, "अठ्हत्तर");
                textStrings.Add(79, "उनास्सी");
                textStrings.Add(80, "अस्सी");
                textStrings.Add(81, "एकासी");
                textStrings.Add(82, "बयासी");
                textStrings.Add(83, "त्रीयासी");
                textStrings.Add(84, "चौरासी");
                textStrings.Add(85, "पचासी");
                textStrings.Add(86, "छयासी");
                textStrings.Add(87, "सतासी");
                textStrings.Add(88, "अठासी");
                textStrings.Add(89, "उनान्नब्बे");
                textStrings.Add(90, "नब्बे");
                textStrings.Add(91, "एकान्नब्बे");
                textStrings.Add(92, "बयान्नब्बे");
                textStrings.Add(93, "त्रियान्नब्बे");
                textStrings.Add(94, "चौरान्नब्बे");
                textStrings.Add(95, "पंचान्नब्बे");
                textStrings.Add(96, "छयान्नब्बे");
                textStrings.Add(97, "सन्तान्‍नब्बे");
                textStrings.Add(98, "अन्ठान्नब्बे");
                textStrings.Add(99, "उनान्सय");

                scales.Add(100000000000, "खरब");
                scales.Add(1000000000, "अरब");
                scales.Add(10000000, "करोड");
                scales.Add(100000, "लाख");
                scales.Add(1000, "हजार");
            }
        }
        #endregion


    }
}
