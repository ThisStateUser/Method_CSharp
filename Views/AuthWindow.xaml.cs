using MahApps.Metro.IconPacks;
using MethodHelper.BD;
using MethodHelper.Controllers;
using MethodHelper.Pages.UIElement;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MethodHelper
{
    public partial class AuthWindow : Window
    {
        bool TbOrPb_pass = false;
        bool r_TbOrPb_pass = false;
        bool PageAorR = true;
        List<int> BugsBD = new List<int>();
        int erMas;

        public AuthWindow()
        {
            InitializeComponent();
            Connect.data = new Model1();
            AuthPage.Visibility = Visibility.Collapsed;
            LoadPage.Visibility = Visibility.Visible;
            Task check = new Task(CheckBD);
            check.Start();
        }

        //////////////// Основные методы

        // Основная форма
        public void MainForm()
        {
            string host = Dns.GetHostName();
            string sip = IPAddress.Parse(Dns.GetHostEntry(host).AddressList[1].ToString()).ToString();
            Connect.host = host;
            Connect.ip = sip;

            var auth = Connect.data.ip_address.Where(x => x.computer_name == host && x.ip_auth_address == sip).FirstOrDefault();
            if (auth != null)
            {
                try
                {
                    var user = Connect.data.users.Where(x => x.id == auth.user_id).FirstOrDefault();
                    user.token = WinObj.generateToken();
                    Connect.data.SaveChanges();

                    Connect.user = user;
                    WinObj.settings = Connect.data.app_settings.Where(x => x.user_id == auth.user_id).FirstOrDefault();
                    new MainWindow().Show();
                }
                catch (Exception ex)
                {
                    WinObj.fatalError(ex);
                }
                this.Close();
                return;
            }
            SwapPage.Visibility = Visibility.Visible;
            r_class_cb.ItemsSource = Connect.data.user_class.ToList();
            r_class_cb.SelectedIndex = 0;
            login_tb.Focus();
        }

        // Проверка БД
        string BugsBDmsg = "";
        public void CheckBD()
        {
            Model1 validBD = Connect.data;
            BugsBD.Clear();
            Loading(true);
            BugsBDmsg = "Таблицы \"";
            try
            {
                ValidBDHelper(validBD);
            }
            catch
            {
                MessageBox.Show("Отсутствуют обязательная база или таблица", "Ошибка", MessageBoxButton.YesNo, MessageBoxImage.Error);
                Dispatcher.Invoke(() => Application.Current.Shutdown());
            }

            if (BugsBD.Count == 0)
            {
                ReturnFormAuth();
                return;
            }

            BugsBDmsg += "\" являются обязательными, но в них отсутствуют данные. Заполнить их автоматически? (если отказаться - приложение будет закрыто.)";

            MessageBoxResult result = MessageBox.Show(BugsBDmsg, "Ошибка", MessageBoxButton.YesNo, MessageBoxImage.Error);
            if (result == MessageBoxResult.Yes)
            {
                FillBD();
            }
            else
            {
                Dispatcher.Invoke(() => Application.Current.Shutdown());
            }
        }

        // Авторизация
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
                if (remember.IsChecked == true)
                {
                    if (Connect.data.ip_address.Where(x => x.user_id == auth.id && x.ip_auth_address == Connect.ip).FirstOrDefault() == null)
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

                new MainWindow().Show();
                this.Close();
            }
            catch (Exception ex)
            {
                WinObj.fatalError(ex);
            }
        }

        // Регистрация
        private void GoReg_Click(object sender, RoutedEventArgs e)
        {
            Validator();
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

        //////////////// Вспомогательные методы

        // Заполнение БД
        private void FillBD()
        {
            try
            {
                foreach (var item in BugsBD)
                {
                    switch (item)
                    {
                        case 1:
                            AddUserClass(0, "(Не выбрано)");
                            AddUserClass(1, "1и1");
                            AddUserClass(2, "2и1");
                            AddUserClass(3, "3и1");
                            AddUserClass(4, "4и1");
                            AddUserClass(5, "Преподаватель");
                            break;
                        case 2:
                            AddUserRole("Администратор");
                            AddUserRole("Преподаватель");
                            AddUserRole("Студент");
                            break;
                        case 3:
                            AddPoint("example");
                            break;
                        case 4:
                            AddCategory("Без фильтров");
                            AddCategory("Еда");
                            AddCategory("Не еда");
                            break;
                        case 5:
                            AddProduct(1000, "EXAMPL", 1, "Пример названия", "Пример описания", 3);
                            break;
                        case 6:
                            AddTypeImg("(Разметка) xaml");
                            AddTypeImg("(Код) xaml.cs");
                            AddTypeImg("(База данных) sql");
                            AddTypeImg("Разное");
                            break;
                        default:
                            break;
                    }
                };
                Connect.data.SaveChanges();
                ReturnFormAuth();
            }
            catch (Exception ex)
            {
                WinObj.fatalError(ex);
            }
        }

        // Добавление данных в базу
        private void AddTypeImg(string type)
        {
            Connect.data.type_image.Add(new type_image()
            {
                type = type
            });
        }

        private void AddCategory(string category_name)
        {
            Connect.data.category.Add(new category()
            {
                category_name = category_name
            });
        }

        private void AddUserClass(int id, string _class)
        {
            Connect.data.user_class.Add(new user_class()
            {
                id = id,
                _class = _class
            });
        }

        private void AddUserRole(string role)
        {
            Connect.data.user_role.Add(new user_role()
            {
                role = role
            });
        }

        private void AddPoint(string point_name)
        {
            Connect.data.point.Add(new point()
            {
                point_name = point_name,
            });
        }

        private void AddProduct(int price, string article, int count, string title, string desk, int category_id)
        {
            Connect.data.product.Add(new product()
            {
                price = price,
                article = article,
                count = count,
                title = title,
                desk = desk,
                category_id = category_id,
            });
        }

        // Хелпер для валидации БД
        private void ValidBDHelper(Model1 validBD)
        {
            if (validBD.user_class.ToList().Count == 0)
            {
                BugsBDmsg += "user_class, ";
                BugsBD.Add(1);
            }

            if (validBD.user_role.ToList().Count == 0)
            {
                BugsBDmsg += "user_role, ";
                BugsBD.Add(2);
            }

            if (validBD.point.ToList().Count == 0)
            {
                BugsBDmsg += "point, ";
                BugsBD.Add(3);
            }

            if (validBD.category.ToList().Count == 0)
            {
                BugsBDmsg += "category, ";
                BugsBD.Add(4);
            }

            if (validBD.product.ToList().Count == 0)
            {
                BugsBDmsg += "product, ";
                BugsBD.Add(5);
            }

            if (validBD.type_image.ToList().Count == 0)
            {
                BugsBDmsg += "type_image, ";
                BugsBD.Add(6);
            }
        }

        private void Validator()
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

        //////////////// UI и Системные методы

        // Возвращение формы после проверки БД
        private void ReturnFormAuth()
        {
            Dispatcher.Invoke(() =>
            {
                Loading(false);
                MainForm();
                AuthPage.Visibility = Visibility.Visible;
                LoadPage.Visibility = Visibility.Collapsed;
            });
        }

        // Переходы по страницам
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

        // Настройки окна
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

        // Анимация загрузки
        public void Loading(bool condition)
        {
            Dispatcher.Invoke(() =>
            {
                if (condition == false)
                { 
                    RotateLoad.BeginAnimation(RotateTransform.AngleProperty, null);
                    return;
                }

                SwapPage.Visibility = Visibility.Collapsed;
                DoubleAnimation animation = new DoubleAnimation()
                {
                    From = 0,
                    To = 360,
                    Duration = TimeSpan.FromSeconds(1),
                    RepeatBehavior = RepeatBehavior.Forever,
                };
                RotateLoad.BeginAnimation(RotateTransform.AngleProperty, animation);
            });
        }

        // Анимация для перехода по страницам
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

        // Скрыть/Показать пароль
        private void SeeHideBtn_Click(object sender, RoutedEventArgs e)
        {
            shb_help(TbOrPb_pass, pass_pb, pass_tb, SHicon);
        }

        private void r_SeeHideBtn_Click(object sender, RoutedEventArgs e)
        {
            shb_help(r_TbOrPb_pass, r_pass_pb, r_pass_tb, r_SHicon);
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

        // Переход по полям на кнопку
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
