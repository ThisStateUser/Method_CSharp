using MahApps.Metro.IconPacks;
using MethodHelper.BD;
using MethodHelper.Controllers;
using System;
using System.Collections.Generic;
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
        List<int> BugsBD = new List<int>();

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

        public void CheckBD()
        {
            var BugsBDmsg = "Таблицы \"";
            BugsBD.Clear();
            var validBD = Connect.data;

            if (validBD != null)
            {
                MessageBox.Show("База данных отсутствует, подключите ее в проект.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                return;
            }
            if (validBD.user_class == null)
            {
                BugsBDmsg += "user_class, ";
                BugsBD.Add(1);
            }
            if (validBD.user_role == null)
            {
                BugsBDmsg += "user_role, ";
                BugsBD.Add(2);
            }
            if (validBD.point == null)
            {
                BugsBDmsg += "point, ";
                BugsBD.Add(3);
            }
            if (validBD.category == null)
            {
                BugsBDmsg += "category, ";
                BugsBD.Add(4);
            }
            if (validBD.product == null)
            {
                BugsBDmsg += "product, ";
                BugsBD.Add(5);
            }
            if (validBD.type_image == null)
            {
                BugsBDmsg += "type_image, ";
                BugsBD.Add(6);
            }

            BugsBDmsg = "\" являются обязательными, но в них отсутствуют данные. Заполнить их автоматически? (если отказаться - приложение будет закрыто.)";
            MessageBoxResult result = MessageBox.Show(BugsBDmsg, "Ошибка", MessageBoxButton.YesNo, MessageBoxImage.Error);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    foreach (var item in BugsBD)
                    {
                        switch (item)
                        {
                            case 1:
                                user_class user_Class = new user_class()
                                {
                                    id = 0,
                                    _class = "(Не выбрано)"
                                };
                                user_class user_Class1 = new user_class()
                                {
                                    id = 1,
                                    _class = "1и1"
                                };
                                user_class user_Class2 = new user_class()
                                {
                                    id = 2,
                                    _class = "2и1"
                                };
                                user_class user_Class3 = new user_class()
                                {
                                    id = 3,
                                    _class = "3и1"
                                };
                                user_class user_Class4 = new user_class()
                                {
                                    id = 4,
                                    _class = "4и1"
                                };
                                user_class user_Class5 = new user_class()
                                {
                                    id = 5,
                                    _class = "Преподаватель"
                                };

                                Connect.data.user_class.Add(user_Class);
                                Connect.data.user_class.Add(user_Class1);
                                Connect.data.user_class.Add(user_Class2);
                                Connect.data.user_class.Add(user_Class3);
                                Connect.data.user_class.Add(user_Class4);
                                Connect.data.user_class.Add(user_Class5);
                                break;
                            case 2:
                                user_role user_Role = new user_role()
                                {
                                    role = "Администратор"
                                };
                                user_role user_Role1 = new user_role()
                                {
                                    role = "Преподаватель"
                                };
                                user_role user_Role2 = new user_role()
                                {
                                    role = "Студент"
                                };

                                Connect.data.user_role.Add(user_Role);
                                Connect.data.user_role.Add(user_Role1);
                                Connect.data.user_role.Add(user_Role2);
                                break;
                            case 3:
                                point point_name = new point()
                                {
                                    point_name = "example",
                                };
                                Connect.data.point.Add(point_name);
                                break;
                            case 4:
                                category category_ = new category()
                                {
                                    category_name = "Без фильтров" 
                                };
                                category category_1 = new category()
                                {
                                    category_name = "Еда"
                                };
                                category category_2 = new category()
                                {
                                    category_name = "Не еда"
                                };
                                Connect.data.category.Add(category_);
                                Connect.data.category.Add(category_1);
                                Connect.data.category.Add(category_2);
                                break;
                            case 5:
                                product product_ = new product()
                                {
                                    price = 1000,
                                    article = "EXAMPL",
                                    count = 1,
                                    title = "Пример названия",
                                    desk = "Пример описания",
                                    category_id = 3,
                                };

                                Connect.data.product.Add(product_);
                                break;
                            case 6:
                                type_image type_Image = new type_image()
                                {
                                    type = "(Разметка) xaml"
                                };
                                type_image type_Image1 = new type_image()
                                {
                                    type = "(Код) xaml.cs"
                                };
                                type_image type_Image2 = new type_image()
                                {
                                    type = "(База данных) sql"
                                };
                                type_image type_Image3 = new type_image()
                                {
                                    type = "Разное"
                                };

                                Connect.data.type_image.Add(type_Image);
                                Connect.data.type_image.Add(type_Image1);
                                Connect.data.type_image.Add(type_Image2);
                                Connect.data.type_image.Add(type_Image3);
                                break;
                            default:
                                break;
                        }
                    };
                    Connect.data.SaveChanges();
                }
                catch (Exception ex)
                {
                    WinObj.fatalError(ex);
                }

            }
            else
            {
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
