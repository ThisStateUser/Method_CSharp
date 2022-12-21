using MethodHelper.Controllers;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MethodHelper.Pages.UIElement
{
    /// <summary>
    /// Логика взаимодействия для AnimationPage.xaml
    /// </summary>
    public partial class Animation : Page
    {
        public Animation()
        {
            InitializeComponent();
            WinObj.deskHelp(s_description, description, Title);
            if (Connect.user.role_id != 3)
            {
                addDesc.Visibility = Visibility.Visible;
            }
        }

        private void ShowCode_Click(object sender, RoutedEventArgs e)
        {
            WinObj.ShowCode(Title);
        }

        private void addDesc_Click(object sender, RoutedEventArgs e)
        {
            WinObj.addDesc(addDesc, description, textbox_desc, s_description, Title);
            WinObj.deskHelp(s_description, description, Title);
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            if (cb_height.IsChecked == true)
            {
                DoubleAnimation animation = new DoubleAnimation()
                {
                    From = anim_Border.ActualHeight,
                    To = 450,
                    Duration = TimeSpan.FromSeconds(2),
                    EasingFunction = new QuadraticEase(),
                };
                anim_Border.BeginAnimation(HeightProperty, animation);
            } else
            {
                DoubleAnimation animation = new DoubleAnimation()
                {
                    From = anim_Border.ActualHeight,
                    To = 80,
                    Duration = TimeSpan.FromSeconds(2),
                    EasingFunction = new QuadraticEase(),
                };
                anim_Border.BeginAnimation(HeightProperty, animation);
            }
            if (cb_width.IsChecked == true)
            {
                DoubleAnimation animation = new DoubleAnimation()
                {
                    From = anim_Border.ActualWidth,
                    To = 450,
                    Duration = TimeSpan.FromSeconds(2),
                    EasingFunction = new QuadraticEase(),
                };
                anim_Border.BeginAnimation(WidthProperty, animation);
            } else
            {
                DoubleAnimation animation = new DoubleAnimation()
                {
                    From = anim_Border.ActualWidth,
                    To = 80,
                    Duration = TimeSpan.FromSeconds(2),
                    EasingFunction = new QuadraticEase(),
                };
                anim_Border.BeginAnimation(WidthProperty, animation);
            }
            if (cb_rotate.IsChecked == true)
            {
                DoubleAnimation animation = new DoubleAnimation()
                {
                    From = borderAngle.Angle,
                    To = 360,
                    Duration = TimeSpan.FromSeconds(2),
                };
                borderAngle.BeginAnimation(RotateTransform.AngleProperty, animation);
            } else
            {
                DoubleAnimation animation = new DoubleAnimation()
                {
                    From = borderAngle.Angle,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(2),
                };
                borderAngle.BeginAnimation(RotateTransform.AngleProperty, animation);
            }
            if (cb_color.IsChecked == true)
            {

            }
            if (cb_sinwidth.IsChecked == true)
            {

            }
            if (cb_loop.IsChecked == true)
            {

            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
