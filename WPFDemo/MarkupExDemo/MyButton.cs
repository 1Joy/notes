using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MarkupExDemo
{
    public class MyButton:Button
    {
        public Type UseWindowType { get; set; }

        protected override void OnClick()
        {
            base.OnClick();
            Window win = Activator.CreateInstance(UseWindowType) as Window;
            win?.ShowDialog();
        }
    }
}
