using Pifagor.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pifagor.Core.CustomElements
{
    class MenuNavigationBtn : Button
    {
        static MenuNavigationBtn()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuNavigationBtn), new FrameworkPropertyMetadata(typeof(MenuNavigationBtn)));
        }
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(ImageSource), typeof(MenuNavigationBtn), new PropertyMetadata(null));

        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register(nameof(IconSize), typeof(double), typeof(MenuNavigationBtn), new PropertyMetadata(16.0));

        public double IconSize {
            get => (double)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }
    }
}
