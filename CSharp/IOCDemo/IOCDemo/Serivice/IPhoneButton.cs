using IOCDemo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCDemo.Serivice
{
    public class IPhoneButton : AbstractPhoneButton
    {
        public IPhoneButton()
        {
            CreateButton();
        }
        public override void CreateButton()
        {
            Console.WriteLine("iphone button is created");
        }
    }
}
