using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace MethodHelper.Pages.UIElement
{
    /// <summary>
    /// Логика взаимодействия для GridAndPanel.xaml
    /// </summary>
    public partial class GridAndPanel : Page
    {
        public GridAndPanel()
        {
            InitializeComponent();
        }

        private void ResizeGridFirst_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation resize = new DoubleAnimation()
            {
                From = GridUI.Width,
                To = 500,
                Duration = TimeSpan.FromSeconds(2),
                EasingFunction = new QuadraticEase(),
            };
            DoubleAnimation resize2 = new DoubleAnimation()
            {
                From = GridUI.Height,
                To = 400,
                Duration = TimeSpan.FromSeconds(2),
                EasingFunction = new QuadraticEase(),
            };
            GridUI.BeginAnimation(WidthProperty, resize);
            GridUI.BeginAnimation(HeightProperty, resize2);
        }

        private void ResizeGridUpDown_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation resize = new DoubleAnimation()
            {
                From = GridUI.Height,
                To = 500,
                Duration = TimeSpan.FromSeconds(2),
                EasingFunction = new QuadraticEase(),
            };
            GridUI.BeginAnimation(HeightProperty, resize);
        }

        private void ResizeGridLeftRight_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation resize = new DoubleAnimation()
            {
                From = GridUI.Width,
                To = 600,
                Duration = TimeSpan.FromSeconds(2),
                EasingFunction = new QuadraticEase(),
            };
            GridUI.BeginAnimation(WidthProperty, resize);
        }

        private void ResizeGridLast_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation resize = new DoubleAnimation()
            {
                From = GridUI.Width,
                To = 600,
                Duration = TimeSpan.FromSeconds(2),
                EasingFunction = new QuadraticEase(),
            };
            DoubleAnimation resize2 = new DoubleAnimation()
            {
                From = GridUI.Height,
                To = 500,
                Duration = TimeSpan.FromSeconds(2),
                EasingFunction = new QuadraticEase(),
            };
            GridUI.BeginAnimation(WidthProperty, resize);
            GridUI.BeginAnimation(HeightProperty, resize2);
        }
    }
}
