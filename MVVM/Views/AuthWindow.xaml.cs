﻿using MahApps.Metro.IconPacks;
using MethodHelper.BD;
using MethodHelper.Controllers;
using System;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Threading;
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

            Connect.data = new Model1();

            string host = Dns.GetHostName();
            IPAddress address = Dns.GetHostEntry(host).AddressList[1];
            string sip = IPAddress.Parse(address.ToString()).ToString();

            Connect.host = host;
            Connect.ip = sip;

            var auth = Connect.data.ip_address.Where(x => x.computer_name == host && x.ip_auth_address == sip).FirstOrDefault();
            if (auth != null)
            {
                Connect.user = Connect.data.users.Where(x => x.id == auth.user_id).FirstOrDefault();
                WinObj.settings = Connect.data.app_settings.Where(x => x.user_id == auth.user_id).FirstOrDefault();
                MainWindow window = new MainWindow();
                window.Show();
                this.Close();
                return;
            }

            r_class_cb.ItemsSource = Connect.data.user_class.ToList();
            r_class_cb.SelectedIndex = 0;

            login_tb.Focus();
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
                    r_first_name_tb.Focus();
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
                    login_tb.Focus();
                    PageAorR = true;
                    break;
                default:
                    break;
            }
        }

        private void GoAuth_Click(object sender, RoutedEventArgs e)
        {
            switch (TbOrPb_pass)
            {
                case true:
                    pass_pb.Password = pass_tb.Text;
                    break;
                default:
                    pass_tb.Text = pass_pb.Password;
                    break;
            }

            try
            {
                var auth = Connect.data.users.Where(x => x.login == login_tb.Text && x.password == pass_tb.Text).FirstOrDefault();
                if (auth == null)
                {
                    MessageBox.Show("Неверный логин или пароль");
                    return;
                }
                var ip = Connect.data.ip_address.Where(x => x.user_id == auth.id && x.ip_auth_address == Connect.ip).FirstOrDefault();

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

                string stoken = WinObj.generateToken();
                if (Connect.data.users.Where(x => x.token == stoken).FirstOrDefault() != null)
                {
                    stoken = WinObj.generateToken();
                }

                auth.token = stoken;
                Connect.user = auth;
                Connect.data.SaveChanges();
                WinObj.settings = Connect.data.app_settings.Where(x => x.user_id == auth.id).FirstOrDefault();

                MainWindow window = new MainWindow();
                window.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                WinObj.fatalError(ex);
            }

        }

        int erMas;
        private void validator()
        {
            erMas = 0;
            switch (r_TbOrPb_pass)
            {
                case true:
                    r_pass_pb.Password = r_pass_tb.Text;
                    break;
                default:
                    r_pass_tb.Text = r_pass_pb.Password;
                    break;
            }

            if (r_first_name_tb.Text.Length == 0)
            {
                name_er.Text = "заполните поле";
            }
            else if (r_first_name_tb.Text.Length <= 1)
            {
                name_er.Text = "мин. 2 символа";
            }
            else
            {
                erMas++;
                name_er.Text = "";
            }
            if (r_class_cb.SelectedIndex == 0)
            {
                class_er.Text = "выберите группу";
            }
            else
            {
                erMas++;
                class_er.Text = "";
            }
            if (r_login_tb.Text.Length == 0)
            {
                login_er.Text = "заполните поле";
            }
            else if (r_login_tb.Text.Length <= 3)
            {
                login_er.Text = "мин. 4 символа";
            }
            else if (Connect.data.users.Where(x => x.login == r_login_tb.Text.Trim()).FirstOrDefault() != null)
            {
                login_er.Text = "логин занят";
            }
            else
            {
                erMas++;
                login_er.Text = "";
            }
            if (r_pass_tb.Text.Length == 0)
            {
                pass_er.Text = "заполните поле";
            }
            else if (r_pass_tb.Text.Length <= 3)
            {
                pass_er.Text = "мин. 4 символа";
            }
            else
            {
                erMas++;
                pass_er.Text = "";
            }
        }

        private void GoReg_Click(object sender, RoutedEventArgs e)
        {
            validator();
            if (erMas == 4)
            {
                try
                {
                    int role = 3;
                    if (r_class_cb.SelectedIndex == 5)
                    {
                        role = 2;
                    }
                    users users = new users()
                    {
                        first_name = r_first_name_tb.Text.Trim(),
                        login = r_login_tb.Text.Trim(),
                        password = r_pass_tb.Text.Trim(),
                        class_id = ((user_class)r_class_cb.SelectedItem).id,
                        role_id = role,
                        user_role = Connect.data.user_role.Where(x => x.id == role).FirstOrDefault(),
                    };
                    app_settings app_set = new app_settings()
                    {
                        user_id = users.id,
                        menu_anim = true,
                        start_page = 0,
                        start_page_desk = Connect.data.start_page_desk.Where(x => x.id == 0).FirstOrDefault(),
                    };
                    Connect.data.users.Add(users);
                    Connect.data.app_settings.Add(app_set);
                    Connect.data.SaveChanges();
                    MessageBox.Show("Успешная регистрация", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    GoPage_Click(null, null);
                }
                catch (Exception ex)
                {
                    WinObj.fatalError(ex);
                }
            }
        }

        //Переход по полям на кнопку

        private void login_tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                switch (TbOrPb_pass)
                {
                    case true:
                        pass_tb.Focus();
                        break;
                    case false:
                        pass_pb.Focus();
                        break;
                    default:
                        break;
                }
            }
        }

        private void pass_tpb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GoAuth_Click(null, null);
            }
        }

        private void r_first_name_tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                r_class_cb.Focus();
                r_class_cb.IsDropDownOpen = true;
            }
        }

        private void r_class_cb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                r_login_tb.Focus();
            }
        }
        private void r_login_tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                switch (r_TbOrPb_pass)
                {
                    case true:
                        r_pass_tb.Focus();
                        break;
                    case false:
                        r_pass_pb.Focus();
                        break;
                    default:
                        break;
                }
            }
        }

        private void r_pass_tpb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GoReg_Click(null, null);
            }
        }
    }
}