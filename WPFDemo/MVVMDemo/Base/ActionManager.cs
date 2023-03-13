using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMDemo.Base
{
    public class ActionManager
    {
        static Dictionary<string, Action> _actions = new Dictionary<string, Action>();

        public static void Register(string name,Action action)
        {
            if (!_actions.ContainsKey(name))
            {
                _actions.Add(name, action);
            }
        }

        public static void Invoke(string name)
        {
            if (_actions.ContainsKey(name))
                _actions[name]();
        }
    }
}
