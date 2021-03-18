using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGrammar
{
    class Student
    {
        public string FistName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FistName}-{LastName}";
        public Student(string fistName, string lastName)
        {
            FistName = fistName;
            LastName = lastName;
        }
        public void nullTest()
        {
            Console.WriteLine("usingStaticTest");
        }

        public static void usingStaticTest()
        {
            Console.WriteLine("usingStaticTest");
        }
    }

    public class SixNewgrammar
    {
        public void Show()
        {
            {
                var student = new Student("1", "2");
                //1：只读自动属性
                Console.WriteLine(student.FullName);
            }
            {
                //2：using static
                //引用静态命名空间
            }
            {
                //3：NUll条件运算符
                Student student=null;
                student?.nullTest();
                Console.WriteLine(student?.FullName??"kong");
            }
            {
                //4：字符串内插
                string ii = "ii", iii = "iii";
                Console.WriteLine($"{ii}-{iii}");
            }
            {
                //5：异常筛选器
                Student student = null;
                try
                {

                }catch(Exception ex)when(student is null)
                {
                    Console.WriteLine("满足when条件才会捕捉到异常");
                }
            }
            {
                //6：事件
                //7：使用索引器初始化关联集合
                Dictionary<int, string> keyValuePairs = new Dictionary<int, string>()
                {
                    {1,"value1" },
                    {2,"value2" }
                };
            }
        }
    }
}