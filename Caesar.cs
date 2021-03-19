using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Caesar
{
    public static class Caesar
    {
        static string c_alphabet = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        static string e_alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string Encode(string text, int shift = 0)
        {
            text = PrepareText(text);
            return Encrypt(text, shift);
        }

        private static string PrepareText(string text)
        {
            text = string.Concat(text.Where(ch => char.IsLetter(ch)));
            text = text.ToUpper();
            text = text.Replace("Ё", "Е");
            return text;
        }

        public static string Encrypt(string str, int n)
        {
            var encrypted = string.Join("", str.Select(x => Encrypt(x, n)));
            return Regex.Replace(encrypted, ".{5}", "$0 ");
        }
      
        public static string Encrypt(char chr, int n)
        {
            if (c_alphabet.Contains(chr))
            {
                int index = c_alphabet.IndexOf(chr);
                index = ((index + n) % c_alphabet.Length + c_alphabet.Length) % c_alphabet.Length;

                return $"{c_alphabet[index]}";
            }
            else if (e_alphabet.Contains(chr))
            {
                int index = e_alphabet.IndexOf(chr);
                index = ((index + n) % e_alphabet.Length + e_alphabet.Length) % e_alphabet.Length;

                return $"{e_alphabet[index]}";
            }
            return "";
        }

        public static string Hack(string text, ref int key)
        {
            text = PrepareText(text);

            var freqTable = c_alphabet.Zip(
                new[]{ 0.062,0.014,0.038,0.013,0.025,0.072,0.007,0.016,0.062,0.010,0.028,0.035,0.026,0.053,0.090,0.023,0.040,0.045,0.053,0.021,0.002,0.009,0.003,0.012,0.006,0.003,0.014,0.016,0.014,0.003,0.006,0.018 },
                (l,f) => new KeyValuePair<char,double>(l,f)).ToDictionary(p => p.Key, d => d.Value);

            var freqText = text.Select(ch => new KeyValuePair<char, double>(ch, (double)text.Count(c => ch == c) / text.Length)).Distinct()
                .OrderBy(pair => pair.Key).ToDictionary(p => p.Key, d => d.Value);

            var min = double.MaxValue;

            for (int m = 0; m < 32; m++)
            {
                var sum = 0.0;

                for(int j = 0; j < 32; j++)
                {
                    var shifted = c_alphabet[(j+m)%c_alphabet.Length];
                    var chr = c_alphabet[j];

                    var shifted_freq = 0.0;

                    if (freqText.ContainsKey(shifted))
                        shifted_freq = freqText[shifted];

                    sum += Math.Pow(freqTable[chr] - shifted_freq, 2);
                }

                if (min > sum)
                {
                    min = sum;
                    key = m;
                }
            }

            return Encode(text, -key);
        }
    }
}
