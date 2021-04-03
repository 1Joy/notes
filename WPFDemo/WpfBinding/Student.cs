using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBinding
{
    public class Student:INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private string name;

        public string Name
        {
            get { return name; }
            set {
                name = value;
                //激发事件来通知binding，Name属性发生了变化，通知binding的目标端的UI元素显示新的值
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        public int Id { get; set; }

        public int Age { get; set; }


    }
}
