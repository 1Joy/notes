using IOCDemo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace IOCDemo.Serivice
{
    public class IPhone : IPhoneInterface
    {
        public AbstractPhoneButton PhoneButton { get; set; }

        [InjectionConstructor] //构造函数注入，默认找参数最多的构造函数
        public IPhone(AbstractPhoneButton button)
        {
            PhoneButton = button;
            Console.WriteLine("iphone is created");
        }
        public void Call()
        {
            Console.WriteLine("iphone is called");
        }

        [InjectionMethod] //方法注入，并且能够自动创建方法所需要的参数实例
        public void PhoneInit()
        {
            Console.WriteLine("正在方法注入，可以创建对象");
        }
    }
}
