using IOCDemo.Interfaces;
using IOCDemo.Serivice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

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
            
            {
                Console.WriteLine("生命周期");
                //创建容器，容器作为对象创建的入口，可以加入自定义的对象管理逻辑(生命周期)
                IUnityContainer container = new UnityContainer();
                //瞬时生命周期，每一次创建的对象都是全新的
                container.RegisterType<IPhoneInterface, IPhone>();//等价于container.RegisterType<IPhoneInterface, IPhone>(new TransientLifetimeManager());

                //容器单例生命周期，在一个容器里面创建的一种对象都是同一个,不用自己去实现单例
                container.RegisterType<IPhoneInterface, IPhone>(new ContainerControlledLifetimeManager());

                //线程单例生命周期，在同一个线程里面创建的一种对象都是同一个
                container.RegisterType<IPhoneInterface, IPhone>(new PerThreadLifetimeManager());

                //子容器单例生命周期，在同一个容器里面创建的一种对象都是同一个
                container.RegisterType<IPhoneInterface, IPhone>(new HierarchicalLifetimeManager());
                IUnityContainer childContainer = container.CreateChildContainer(); //创建一个容器的子容器
            }
            */
            {
                Console.WriteLine("通过配置文件来创建对象，减少细节依赖");
            }
            Console.ReadLine();
        }
    }
}
