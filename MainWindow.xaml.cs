using MethodHelper.Controllers;
using MethodHelper.Pages;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MethodHelper
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool MinMaxWin = false;
        int time = 0;
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            //connect
            Connect.data = new BD.Model1();
            WinObj.settings = Connect.data.app_settings.Where(x => x.user_id == WinObj.user).FirstOrDefault();
            FrameObj.MainFrame = MainFrame;
            Win.method = this;
            //other
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

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

        private void Timer_Tick(object sender, EventArgs e)
        {
            ErrorTimer.Text = time.ToString();
            switch (time)
            {
                case 0:
                    AnimErrorWindow(-320, 0);
                    time = 0;
                    timer.Stop();
                    break;
                default:
                    time--;
                    break;
            }
        }

        //ToolBar
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

        //Анимированное окно уведомления
        private void AnimErrorWindow(int startPos, int endPos)
        {
            ErrorWindow.Visibility = Visibility.Visible;
            ErrorTimer.Text = time.ToString();
            TranslateTransform ShowTrans = new TranslateTransform();
            ErrorWindow.RenderTransform = ShowTrans;
            DoubleAnimation ShowAnimX = new DoubleAnimation(startPos, endPos, TimeSpan.FromMilliseconds(200));
            ShowTrans.BeginAnimation(TranslateTransform.XProperty, ShowAnimX);
        }

        private void HideErrorBtn_Click(object sender, RoutedEventArgs e)
        {
            AnimErrorWindow(-320, 0);
            time = 0;
            timer.Stop();
        }

        private void TextMsgIn(string error, string errormessage)
        {
            ErrorTitle.Text = error;
            ErrorDesk.Text = errormessage;
        }

        public void ShowErrorMessage(string error, string errormessage, string status, int delay = 5)
        {
            time = delay;
            AnimErrorWindow(0, -320);

            timer.Start();
            switch (status.ToLower())
            {
                case "error":
                    ErrorWindow.Background = (SolidColorBrush)FindResource("redcolor");
                    break;
                case "ok":
                    ErrorWindow.Background = (SolidColorBrush)FindResource("greencolor");
                    break;
                case "info":
                    ErrorWindow.Background = (SolidColorBrush)FindResource("yellowcolor");
                    break;
                default:
                    TextMsgIn("Неизвестная ошибка", ":(");
                    ErrorWindow.Background = (SolidColorBrush)FindResource("redcolor");
                    break;
            }
            TextMsgIn(error, errormessage);
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

        private void InfoPage_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.MainFrame.Navigate(new InfoPage());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            var exit = Connect.data.ip_address.Where(x => x.computer_name == Connect.host && x.ip_auth_address == Connect.ip).FirstOrDefault();
            if (exit != null)
            {
                Connect.data.ip_address.Remove(exit);
                Connect.data.SaveChanges();
            }
            AuthWindow window = new AuthWindow();
            window.Show();
            this.Close();
        }
    }
}
