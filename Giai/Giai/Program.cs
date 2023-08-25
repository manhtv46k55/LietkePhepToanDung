using System;

namespace Giai
{
    class Program
    {
        public static int[] a = new int[9];
        public static float calculate(string s)
        {
            float ret = (s[0] - '0');
            int sign = 1;
            int num = 0;
            int n = s.Length;
            int i = 1;
            while (i < n)
            {
                char x = s[i];
                if (x == '+')
                {
                    ret += (s[i + 1] - '0');
                }
                else if (x == '-')
                {
                    ret -= (s[i + 1] - '0');
                }
                else if (x == '*')
                {
                    if (i >= 3)
                    {
                        if (s[i - 2] == '+')
                        {
                            ret -= (s[i - 1] - '0');
                            ret += (s[i - 1] - '0') * (s[i + 1] - '0');

                        }
                        else if (s[i - 2] == '-')
                        {
                            ret += (s[i - 1] - '0');
                            ret -= (s[i - 1] - '0') * (s[i + 1] - '0');
                        }
                        else
                        {
                            ret *= (s[i + 1] - '0');
                        }
                    }
                    else
                    {
                        ret *= (s[i + 1] - '0');
                    }
                }
                else if (x == '/')
                {
                    if (i >= 3)
                    {
                        if (s[i - 2] == '+')
                        {
                            ret -= (s[i - 1] - '0');
                            ret += (float)(s[i - 1] - '0') / (s[i + 1] - '0');

                        }
                        else if (s[i - 2] == '-')
                        {
                            ret += (s[i - 1] - '0');
                            ret -= (float)(s[i - 1] - '0') / (s[i + 1] - '0');

                        }
                        else
                        {
                            ret /= (s[i + 1] - '0');
                        }
                    }
                    else
                    {
                        ret /= (s[i + 1] - '0');
                    }
                }
                i += 2;
            }

            return ret;
        }

        public static void cal(int index, bool isEqual, string s)
        {
            if (index <= 8)
            {
                string ss = Convert.ToString(a[index]);
                cal(index + 1, isEqual, s + '+' + ss);
                cal(index + 1, isEqual, s + '-' + ss);
                cal(index + 1, isEqual, s + '*' + ss);
                cal(index + 1, isEqual, s + '/' + ss);
                if (isEqual == false)
                {
                    cal(index + 1, true, s + '=' + ss);
                }
            }
            else
            {
                if (isEqual == true)
                {
                    int p = s.IndexOf("=");

                    string s1 = s.Substring(0, p);
                    string s2 = s.Substring(p + 1, s.Length - p - 1);
                    if (calculate(s1) == calculate(s2))
                    {
                        Console.Write(s);
                        Console.Write("\n");
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            int num;
            num = int.Parse(ConsoleInput.ReadToWhiteSpace(true));
            for (int i = 8; i >= 1; i--)
            {
                a[i] = num % 10;
                num /= 10;
            }

            cal(2, false, Convert.ToString(a[1]));
        }

        internal static class ConsoleInput
        {
            private static bool goodLastRead = false;
            public static bool LastReadWasGood
            {
                get
                {
                    return goodLastRead;
                }
            }

            public static string ReadToWhiteSpace(bool skipLeadingWhiteSpace)
            {
                string input = "";

                char nextChar;
                while (char.IsWhiteSpace(nextChar = (char)System.Console.Read()))
                {
                    //accumulate leading white space if skipLeadingWhiteSpace is false:
                    if (!skipLeadingWhiteSpace)
                        input += nextChar;
                }
                //the first non white space character:
                input += nextChar;

                //accumulate characters until white space is reached:
                while (!char.IsWhiteSpace(nextChar = (char)System.Console.Read()))
                {
                    input += nextChar;
                }

                goodLastRead = input.Length > 0;
                return input;
            }

            public static string ScanfRead(string unwantedSequence = null, int maxFieldLength = -1)
            {
                string input = "";

                char nextChar;
                if (unwantedSequence != null)
                {
                    nextChar = '\0';
                    for (int charIndex = 0; charIndex < unwantedSequence.Length; charIndex++)
                    {
                        if (char.IsWhiteSpace(unwantedSequence[charIndex]))
                        {
                            //ignore all subsequent white space:
                            while (char.IsWhiteSpace(nextChar = (char)System.Console.Read()))
                            {
                            }
                        }
                        else
                        {
                            //ensure each character matches the expected character in the sequence:
                            nextChar = (char)System.Console.Read();
                            if (nextChar != unwantedSequence[charIndex])
                                return null;
                        }
                    }

                    input = nextChar.ToString();
                    if (maxFieldLength == 1)
                        return input;
                }

                while (!char.IsWhiteSpace(nextChar = (char)System.Console.Read()))
                {
                    input += nextChar;
                    if (maxFieldLength == input.Length)
                        return input;
                }

                return input;
            }
        }
    }

}
