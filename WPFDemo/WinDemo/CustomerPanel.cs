using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WinDemo
{
    /// <summary>
    /// 自定义容器控件
    /// </summary>
    public class CustomerPanel:Panel
    {
        /// <summary>
        /// 测量
        /// </summary>
        /// <param name="availableSize"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            //Size newSize = new Size(availableSize.Width / 4, availableSize.Height); //容器均分为4列的尺寸
            double height = 0;
            foreach (FrameworkElement item in this.InternalChildren)
            {
                //测量容器内每个控件的尺寸
                //item.Measure(newSize);
                item.Measure(availableSize);
                height += item.DesiredSize.Height;
            }
            //return base.MeasureOverride(availableSize);
            return new Size(availableSize.Width, height); //返回容器实际的尺寸
        }

        /// <summary>
        /// 排列
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            double height = 0;
            foreach (FrameworkElement item in this.InternalChildren)
            {
                //摆放元素
                //item.Measure(newSize);
                item.Arrange(new Rect(new Point(0,height),item.DesiredSize));
                height += item.DesiredSize.Height;
            }
            //return base.ArrangeOverride(finalSize);
            return finalSize;
        }
    }
}
