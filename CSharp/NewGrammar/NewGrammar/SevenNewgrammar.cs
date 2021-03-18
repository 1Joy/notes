using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGrammar
{
    public class SevenNewgrammar
    {
        public void Show()
        {
            {
                //out变量
                //2：元组
                (string a, string b) letters = ("c", "d");
                Console.WriteLine($"{letters.a},{letters.b}");

                var letters2 = (a: "c", b: "d");
                Console.WriteLine($"{letters2.a},{letters2.b}");

                (int a, string b) = GetValue();
                Console.WriteLine($"{a},{b}");

                //3：弃元
                (_, string b1) = GetValue();
            }
        }

        public (int a,string b) GetValue()
        {
            return (1, "dd");
        }
    }
}
