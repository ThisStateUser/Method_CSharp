using MahApps.Metro.IconPacks;
using MethodHelper.BD;
using MethodHelper.Controllers;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace MethodHelper
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        bool TbOrPb_pass = false;
        bool r_TbOrPb_pass = false;
        bool PageAorR = true;


        public AuthWindow()
        {
            InitializeComponent();            
            Connect.data = new BD.Model1();      
            
            string host = Dns.GetHostName();
            IPAddress address = Dns.GetHostEntry(host).AddressList[1];
            string sip = IPAddress.Parse(address.ToString()).ToString();

            Connect.host = host;
            Connect.ip = sip;

            if (Connect.data.ip_address.Where(x => x.computer_name == host && x.ip_auth_address == sip).FirstOrDefault() != null)
            {
                MainWindow window = new MainWindow();
                window.Show();
                this.Close();
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

        private void shb_help(bool param, PasswordBox password, TextBox text, PackIconMaterial icon)
        {
            switch (param)
            {
                case false:
                    text.Text = password.Password;
                    text.Visibility = Visibility.Visible;
                    password.Visibility = Visibility.Collapsed;
                    icon.Kind = PackIconMaterialKind.Eye;
                    r_TbOrPb_pass = true;
                    TbOrPb_pass = true;
                    break;

                case true:
                    password.Password = text.Text;
                    text.Visibility = Visibility.Collapsed;
                    password.Visibility = Visibility.Visible;
                    icon.Kind = PackIconMaterialKind.EyeOff;
                    r_TbOrPb_pass = false;
                    TbOrPb_pass = false;
                    break;
                default:

                    break;
            }
        }

        private void SeeHideBtn_Click(object sender, RoutedEventArgs e)
        {
            shb_help(TbOrPb_pass, pass_pb, pass_tb, SHicon);
        }

        private void r_SeeHideBtn_Click(object sender, RoutedEventArgs e)
        {
            shb_help(r_TbOrPb_pass, r_pass_pb, r_pass_tb, r_SHicon);
        }

        private void GoPage_help(StackPanel stack, int prop)
        {
            DoubleAnimation animswipe = new DoubleAnimation()
            {
                From = stack.Height,
                To = prop,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new QuadraticEase(),
            };
            stack.BeginAnimation(HeightProperty, animswipe);
        }
        public delegate void ThreadStart();



        private async void GoPage_Click(object sender, RoutedEventArgs e)
        {
            TbOrPb_pass = false;
            r_TbOrPb_pass = false;

            switch (PageAorR)
            {
                case true:
                    GoPage_help(AuthPage, 0);
                    await Task.Delay(600);
                    GoPage_help(RegPage, 314);
                    RegPage.Visibility = Visibility.Visible;
                    AuthPage.Visibility = Visibility.Collapsed;
                    downauthtext.Text = "Уже есть аккаунт?";
                    GoPage.Content = "Авторизироваться";
                    title.Text = "Регистрация";
                    PageAorR = false;
                    break;

                case false:
                    GoPage_help(RegPage, 0);
                    await Task.Delay(600);
                    GoPage_help(AuthPage, 195);
                    AuthPage.Visibility = Visibility.Visible;
                    RegPage.Visibility = Visibility.Collapsed;
                    downauthtext.Text = "Нет аккаунта?";
                    GoPage.Content = "Перейти к регистрации";
                    title.Text = "Авторизация";
                    PageAorR = true;
                    break;
                default:
                    break;
            }
        }

        private void GoAuth_Click(object sender, RoutedEventArgs e)
        {
            pass_tb.Text = pass_pb.Password;
            try
            {
                var auth = Connect.data.users.Where(x => x.login == login_tb.Text && x.password == pass_tb.Text).FirstOrDefault();
                var ip = Connect.data.ip_address.Where(x => x.user_id == auth.id && x.ip_auth_address == Connect.ip).FirstOrDefault();
                if (auth == null)
                {
                    MessageBox.Show("Неверный логин или пароль");
                    return;
                }


                string stoken = WinObj.generateToken();
                if (remember.IsChecked == true)
                {                
                    if (ip == null)
                    {
                        ip_address adress = new ip_address()
                        {
                            user_id = auth.id,
                            ip_auth_address = Connect.ip,
                            computer_name = Connect.host,
                            remember = true,
                        };
                        Connect.data.ip_address.Add(adress);
                    }
                } 

                auth.token = stoken;

                Connect.data.SaveChanges();

                MainWindow window = new MainWindow();
                window.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void GoReg_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
