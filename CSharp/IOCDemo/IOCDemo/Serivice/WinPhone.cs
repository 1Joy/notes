using IOCDemo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCDemo.Serivice
{
    public class WinPhone : IPhoneInterface
    {
        public void Call()
        {
            Console.WriteLine("winPhone is called");
        }
    }
}
