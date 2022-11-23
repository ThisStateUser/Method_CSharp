using MethodHelper.Controllers;
using MethodHelper.Pages;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MethodHelper
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool MinMaxWin = false;

        public MainWindow()
        {
            InitializeComponent();
            Connect.data = new BD.Model1();
            WinObj.settings = Connect.data.app_settings.Where(x => x.user_id == WinObj.user).FirstOrDefault();
            FrameObj.MainFrame = MainFrame;
            Win.method = this;
            switch (WinObj.settings.start_page)
            {
                case 0:
                    FrameObj.MainFrame.Navigate(new HomePage());
                    HomePage.IsChecked = true;
                    break;
                case 1:
                    FrameObj.MainFrame.Navigate(new UIPage());
                    UIPage.IsChecked = true;
                    break;
                case 2:
                    FrameObj.MainFrame.Navigate(new MethodPage());
                    MethodPage.IsChecked = true;
                    break;
                case 3:
                    FrameObj.MainFrame.Navigate(new SettingsPage());
                    SettingsPage.IsChecked = true;
                    break;
                default:
                    FrameObj.MainFrame.Navigate(new HomePage());
                    HomePage.IsChecked = true;
                    break;
            }
        }

        private void CloseWin_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void CollapseWin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void ResizeWin_Click(object sender, RoutedEventArgs e)
        {
            switch (MinMaxWin)
            {
                case false:
                    WindowState = WindowState.Maximized;
                    ResizeIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.Square;
                    MinMaxWin = true;
                    break;

                case true:
                    WindowState = WindowState.Normal;
                    ResizeIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.SquareOutline;
                    MinMaxWin = false;
                    break;
                default:
                    break;
            }
        }

        //Анимация навигации
        private void NavBar_MouseEnter(object sender, MouseEventArgs e)
        {

            if (WinObj.settings.menu_anim.Value == true)
            {
                DoubleAnimation shownav = new DoubleAnimation()
                {
                    From = NavBar.Width,
                    To = 200,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new QuadraticEase(),
                };
                NavBar.BeginAnimation(WidthProperty, shownav);
                return;
            }
        }

        private void NavBar_MouseLeave(object sender, MouseEventArgs e)
        {
            if (WinObj.settings.menu_anim.Value == true)
            {
                DoubleAnimation hidenav = new DoubleAnimation()
                {
                    From = NavBar.Width,
                    To = 60,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new QuadraticEase(),
                };
                NavBar.BeginAnimation(WidthProperty, hidenav);
                return;
            }
        }

        //Навигация
        private void HomePage_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.MainFrame.Navigate(new HomePage());
        }

        private void UIPage_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.MainFrame.Navigate(new UIPage());
        }

        private void MethodPage_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.MainFrame.Navigate(new MethodPage());
        }

        private void SettingsPage_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.MainFrame.Navigate(new SettingsPage());
        }

        //Вернуться назад
        public bool checkBack()
        {
            if (!FrameObj.MainFrame.CanGoBack)
            {
                backtext.Foreground = (SolidColorBrush)FindResource("textColor3");
                backicon.Foreground = (SolidColorBrush)FindResource("textColor3");
                return false;
            }
            backtext.Foreground = (SolidColorBrush)FindResource("titleColor2");
            backicon.Foreground = (SolidColorBrush)FindResource("titleColor2");
            return true;
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            checkBack();
            if (checkBack() == true)
            {
            FrameObj.MainFrame.GoBack();
            }
        }


    }
}
