using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MvvmLightFWDemo.Dialog
{
    public class DialogService : IDialogService
    {
        public void ShowMsg(string msg)
        {
            MessageBox.Show(msg);
        }
    }
}
