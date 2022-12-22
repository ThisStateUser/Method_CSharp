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
            void act_actualWidth(double size)
            {
                DoubleAnimation animation = new DoubleAnimation()
                {
                    From = anim_Border.ActualWidth,
                    To = size,
                    Duration = TimeSpan.FromSeconds(2),
                    EasingFunction = new QuadraticEase(),
                };
                if (cb_loop.IsChecked == true)
                {
                    animation.RepeatBehavior = RepeatBehavior.Forever;
                    if (cb_reverse.IsChecked == true)
                    {
                        animation.AutoReverse = true;
                    }
                }
                anim_Border.BeginAnimation(WidthProperty, animation);
            }
            if (cb_width.IsChecked == true)
            {
                act_actualWidth(450);
            } else
            {
                act_actualWidth(80);
            }


            void act_actualHeigh(double size)
            {
                DoubleAnimation animation = new DoubleAnimation()
                {
                    From = anim_Border.ActualHeight,
                    To = size,
                    Duration = TimeSpan.FromSeconds(2),
                    EasingFunction = new QuadraticEase(),
                };
                if (cb_loop.IsChecked == true)
                {
                    animation.RepeatBehavior = RepeatBehavior.Forever;
                    if (cb_reverse.IsChecked == true)
                    {
                        animation.AutoReverse = true;
                    }
                }
                anim_Border.BeginAnimation(HeightProperty, animation);
            }
            if (cb_height.IsChecked == true)
            {
                act_actualHeigh(450);
            } else
            {
                act_actualHeigh(80);
            }

            void act_Angle(double angle)
            {
                DoubleAnimation animation = new DoubleAnimation()
                {
                    From = borderAngle.Angle,
                    To = angle,
                    Duration = TimeSpan.FromSeconds(2),
                };
                if (cb_loop.IsChecked == true)
                {
                    animation.RepeatBehavior = RepeatBehavior.Forever;
                    if (cb_reverse.IsChecked == true)
                    {
                        animation.AutoReverse = true;
                    }
                }
                borderAngle.BeginAnimation(RotateTransform.AngleProperty, animation);
            }
            if (cb_rotate.IsChecked == true)
            {
                act_Angle(360);
            } else
            {
                act_Angle(0);
            }
            if (cb_pos.IsChecked == true)
            {
                DoubleAnimation animationX = new DoubleAnimation(borderPos.X, 100, TimeSpan.FromSeconds(2));
                borderPos.BeginAnimation(TranslateTransform.XProperty, animationX);
                DoubleAnimation animationY = new DoubleAnimation(borderPos.Y, 50, TimeSpan.FromSeconds(2));
                borderPos.BeginAnimation(TranslateTransform.YProperty, animationY);
            } else
            {
                DoubleAnimation animationX = new DoubleAnimation(borderPos.X, 0, TimeSpan.FromSeconds(2));
                borderPos.BeginAnimation(TranslateTransform.XProperty, animationX);
                DoubleAnimation animationY = new DoubleAnimation(borderPos.Y, 0, TimeSpan.FromSeconds(2));
                borderPos.BeginAnimation(TranslateTransform.YProperty, animationY);
            }

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
