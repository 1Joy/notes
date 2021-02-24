using IOCDemo.Interfaces;
using IOCDemo.Serivice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace IOCDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            {
                Console.WriteLine("Unity容器应用");
                //1.实例化容器
                IUnityContainer container = new UnityContainer();
                //2.初始化容器，注册类型，如果遇到指定的抽象类型，就实例化一个指定的具体类型实例
                container.RegisterType<IPhoneInterface, IPhone>();
                //3.通过反射来创建对象
                IPhoneInterface phone = container.Resolve<IPhoneInterface>();
                phone.Call();
            }
            {
                Console.WriteLine("多种注册，一对多注册");
                IUnityContainer container = new UnityContainer();
                //为一个接口注册多个具体类型，需要给类型取别名来区分
                //注册类型可以注册接口，抽象类，父子类等类型
                container.RegisterType<IPhoneInterface, WinPhone>("winPhone");
                container.RegisterType<IPhoneInterface, IPhone>("IPhone");

                var winPhone = container.Resolve<IPhoneInterface>("winPhone");
                var iphone = container.Resolve<IPhoneInterface>("IPhone");
                winPhone.Call();
                iphone.Call();
            }
            */
            {
                Console.WriteLine("依赖注入");
                IUnityContainer container = new UnityContainer();
                //2.初始化容器，注册类型，如果遇到指定的抽象类型，就实例化一个指定的具体类型实例
                container.RegisterType<IPhoneInterface, IPhone>();
                container.RegisterType<AbstractPhoneButton, IPhoneButton>();
                //3.通过反射来创建对象,在创建对象时，会自动创建构造函数需要的对象，并在构造函数调用完成后会去检查是否有属性注入和方法注入
                IPhoneInterface phone = container.Resolve<IPhoneInterface>();
                phone.Call();
            }
            Console.ReadLine();
        }
    }
}
