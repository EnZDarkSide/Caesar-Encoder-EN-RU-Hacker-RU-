using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Caesar
{
    class Vigenere
    {
        public static string c_alphabet = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        static string key = "";

        public static string Encode(string text, string key, bool reverse = false)
        {
            Vigenere.key = key.ToUpper();
            text = PrepareText(text);
            return Encrypt(text, reverse);
        }

        private static string PrepareText(string text)
        {
            text = string.Concat(text.Where(ch => char.IsLetter(ch)));
            text = text.ToUpper();
            text = text.Replace("Ё", "Е");
            return text;
        }

        public static string Encrypt(string str, bool reverse = false)
        {
            var encrypted = string.Join("", str.Select((x, i) => Encrypt(i, x, reverse)));
            return Regex.Replace(encrypted, ".{5}", "$0 ");
        }

        public static string Encrypt(int index, char ch, bool reverse = false)
        {
            var kindex = c_alphabet.IndexOf(key[index % key.Length]);
            var chrindex = c_alphabet.IndexOf(ch);

            if (chrindex == -1)
                return ch.ToString();

            var multiplier = reverse ? -1 : 1;

            var shift = (chrindex + kindex * multiplier + c_alphabet.Length) % c_alphabet.Length;

            return c_alphabet[shift].ToString();
        }

        public static string Hack(string text, ref string key)
        {
            text = PrepareText(text);

            var keyLength = GetKeyLength(text);

            var rows = Enumerable.Repeat("", keyLength).ToList();
            var keys = Enumerable.Repeat('0', keyLength).ToList();

            for (int i = 0; i < text.Length; i += keyLength)
            {
                for (int j = 0; j < keyLength; j++)
                {
                    if (i + j >= text.Length)
                        break;

                    rows[j] += text[i + j];
                }
            }

            for (int i = 0; i < keyLength; i++)
            {
                var k = 0;
                rows[i] = Caesar.Hack(rows[i], ref k);

                keys[i] = c_alphabet[(c_alphabet.Length + k) % c_alphabet.Length];
            }

            key = string.Join("", keys);
            text = Encode(text, key, true);

            return text;
        }

        public static int GetKeyLength(string text)
        {
            var keyLength = 0;

            for(int i = 1; i < c_alphabet.Length-1; i++)
            {
                var count = 0;
                for (int j = 0; j < text.Length; j++)
                {
                    count += text[j] == text[(j + i) % text.Length] ? 1 : 0;
                }

                double m_index = (double)count / text.Length;
                if (m_index >= 0.05)
                {
                    keyLength = i;
                    break;
                }
            }
            

            return keyLength;
        }

        public static int gcd(int a, int b)
        {
            while (a > 0 && b > 0)
            {
                if (a > b)
                    a = a % b;
                else
                    b = b % a;
            }

            return a + b;
        }
    }
}
