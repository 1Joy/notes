using MVVMDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMDemo.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindoeModel MainModel { get; set; } = new MainWindoeModel();

        public void Add()
        {
            MainModel.Value3 = MainModel.Value1 + MainModel.Value2;
        }
    }
}
