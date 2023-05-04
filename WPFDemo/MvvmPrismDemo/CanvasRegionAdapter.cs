using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MvvmPrismDemo
{
    /// <summary>
    /// 注册一个可以以cavas类型的区域适配器
    /// </summary>
    public class CanvasRegionAdapter : RegionAdapterBase<Canvas>
    {
        public CanvasRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        /// <summary>
        /// 如何将view添加到cavas里面
        /// </summary>
        /// <param name="region"></param>
        /// <param name="regionTarget">对应的canvas区域</param>
        /// <exception cref="NotImplementedException"></exception>
        protected override void Adapt(IRegion region, Canvas regionTarget)
        {
            region.Views.CollectionChanged += (sender, args) =>
            {
                //添加，相当于addToRegion()
                if(args.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    foreach(UIElement view in args.NewItems)
                    {
                        regionTarget.Children.Add(view);
                    }
                }
                else if(args.Action== System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                {
                    foreach (UIElement view in args.NewItems)
                    {
                        regionTarget.Children.Remove(view);
                    }
                }
            };
        }

        protected override IRegion CreateRegion()
        {
           return new AllActiveRegion();
        }
    }
}
