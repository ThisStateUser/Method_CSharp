using MahApps.Metro.IconPacks;
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
        bool sp = false;
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
            switch (sp)
            {
                case true:
                    iconinbtn.Kind = PackIconMaterialKind.Play;
                    cb_loop.IsChecked = false;
                    cb_reverse.Visibility = Visibility.Hidden;
                    sp = false;
                    break;
                case false:
                    iconinbtn.Kind = PackIconMaterialKind.Pause;                    
                    sp = true;
                    break;
            }

            cb_width_Click(null, null);
            cb_height_Click(null, null);
            cb_rotate_Click(null, null);
            cb_pos_Click(null, null);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            bool temp = sp;
            sp = true;
            max_width.Text = "450";
            min_width.Text = "80";
            max_height.Text = "450";
            min_height.Text = "80";
            angle_d.Text = "360";
            time_anim.Text = "2";

            cb_width.IsChecked = false;
            cb_height.IsChecked = false;
            cb_rotate.IsChecked = false;
            cb_pos.IsChecked = false;
            cb_loop.IsChecked = false;
            cb_reverse.IsChecked = false;

            cb_width_Click(null, null);
            cb_height_Click(null, null);
            cb_pos_Click(null, null);
            cb_reverse_Click(null, null);
            cb_loop_Click(null, null);
            cb_rotate_Click(null, null);
            sp = temp;
        }

        private void cb_width_Click(object sender, RoutedEventArgs e)
        {
            if (sp == true)
            {
                void act_actualWidth(double size)
                {
                    DoubleAnimation animation = new DoubleAnimation()
                    {
                        From = anim_Border.ActualWidth,
                        To = size,
                        Duration = TimeSpan.FromSeconds(Convert.ToInt32(time_anim.Text)),
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
                    act_actualWidth(Convert.ToInt32(max_width.Text));
                    return;
                }
                act_actualWidth(Convert.ToInt32(min_width.Text));

            }
        }

        private void cb_height_Click(object sender, RoutedEventArgs e)
        {
            if (sp == true)
            {
                void act_actualHeigh(double size)
                {
                    DoubleAnimation animation = new DoubleAnimation()
                    {
                        From = anim_Border.ActualHeight,
                        To = size,
                        Duration = TimeSpan.FromSeconds(Convert.ToInt32(time_anim.Text)),
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
                    act_actualHeigh(Convert.ToInt32(max_height.Text));
                    return;
                }
                act_actualHeigh(Convert.ToInt32(min_height.Text));
            }
        }

        private void cb_rotate_Click(object sender, RoutedEventArgs e)
        {
            if (sp == true)
            {
                void act_Angle(double angle)
                {
                    DoubleAnimation animation = new DoubleAnimation()
                    {
                        From = borderAngle.Angle,
                        To = angle,
                        Duration = TimeSpan.FromSeconds(Convert.ToInt32(time_anim.Text)),
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
                    act_Angle(Convert.ToInt32(angle_d.Text));
                    return;
                }
                act_Angle(0);
            }
        }

        private void cb_pos_Click(object sender, RoutedEventArgs e)
        {
            if (sp == true)
            {
                if (cb_pos.IsChecked == true)
                {
                    DoubleAnimation animationX = new DoubleAnimation(borderPos.X, 100, TimeSpan.FromSeconds(Convert.ToInt32(time_anim.Text)));
                    borderPos.BeginAnimation(TranslateTransform.XProperty, animationX);
                    DoubleAnimation animationY = new DoubleAnimation(borderPos.Y, 50, TimeSpan.FromSeconds(Convert.ToInt32(time_anim.Text)));
                    borderPos.BeginAnimation(TranslateTransform.YProperty, animationY);
                    return;
                }
                DoubleAnimation animationX2 = new DoubleAnimation(borderPos.X, 0, TimeSpan.FromSeconds(Convert.ToInt32(time_anim.Text)));
                borderPos.BeginAnimation(TranslateTransform.XProperty, animationX2);
                DoubleAnimation animationY2 = new DoubleAnimation(borderPos.Y, 0, TimeSpan.FromSeconds(Convert.ToInt32(time_anim.Text)));
                borderPos.BeginAnimation(TranslateTransform.YProperty, animationY2);
            }
        }

        private void cb_loop_Click(object sender, RoutedEventArgs e)
        {
            if (sp == true)
            {
                cb_width_Click(null, null);
                cb_height_Click(null, null);
                cb_rotate_Click(null, null);
                cb_pos_Click(null, null);

                cb_reverse.IsChecked = false;
            }

            if (cb_loop.IsChecked == true)
            {
                cb_reverse.Visibility = Visibility.Visible;
                return;
            }
            cb_reverse.Visibility = Visibility.Hidden;
        }

        private void cb_reverse_Click(object sender, RoutedEventArgs e)
        {
            if (cb_loop.IsChecked == true && sp == true)
            {
                cb_width_Click(null, null);
                cb_height_Click(null, null);
                cb_rotate_Click(null, null);
                cb_pos_Click(null, null);
            }
        }

        bool _settings_open = false;
        private void OpenSettings_Click(object sender, RoutedEventArgs e)
        {
            RotateTransform rotateTransform = new RotateTransform();
            setting_icon.RenderTransform = rotateTransform;
            DoubleAnimation animationset = new DoubleAnimation()
            {
                From = 0,
                To = -360,
                Duration = TimeSpan.FromSeconds(1),
            };
            setting_icon.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, animationset);

            void animations(Border border, double first_param, double last_param)
            {
                DoubleAnimation animation = new DoubleAnimation()
                {
                    From = first_param,
                    To = last_param,
                    Duration = TimeSpan.FromSeconds(1),
                    EasingFunction = new QuadraticEase(),
                };
                border.BeginAnimation(WidthProperty, animation);
            }
            switch (_settings_open)
            {
                case true:
                    animations(cb_set, cb_set.ActualWidth, 580);
                    animations(cb_value, cb_value.ActualWidth, 0);
                    _settings_open = false;
                    break;
                case false:
                    animations(cb_value, cb_value.ActualWidth, 580);
                    animations(cb_set, cb_set.ActualWidth, 0);
                    _settings_open = true;
                    break;
            }
        }
        private void CodeAnimation_Click(object sender, RoutedEventArgs e)
        {
            anim_CodePage.Visibility = Visibility.Visible;
            anim_StylePage.Visibility = Visibility.Collapsed;
            CodeAnimation.Background = (SolidColorBrush)FindResource("cyancolor");
            StyleAnimation.Background = (SolidColorBrush)FindResource("buttonColor");

        }

        private void StyleAnimation_Click(object sender, RoutedEventArgs e)
        {
            anim_CodePage.Visibility = Visibility.Collapsed;
            anim_StylePage.Visibility = Visibility.Visible;
            CodeAnimation.Background = (SolidColorBrush)FindResource("buttonColor");
            StyleAnimation.Background = (SolidColorBrush)FindResource("cyancolor");
        }


    }
}
