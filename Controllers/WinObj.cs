using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MethodHelper.BD;
using MethodHelper.Views;

namespace MethodHelper.Controllers
{
    internal class WinObj
    {
        public static app_settings settings;

        public static string generateToken()
        {
            Random random = new Random();
            char[] tokenMas = new char[]
            {
                    'A','b','c','D','e','F',
                    'f','J','j','w','W','g',
                    '0','1','2','C','V','G',
                    '9','5','3','7','#','@',
                    '>','N','q','Q','L','!',
                    '<','.','s','S','Z','z',
                    '=','o','O','a','P','p',
                    '-','n','i','I','4','8'
            };

            string token = "";

            for (int i = 0; i <= 32; i++)
            {
                int ti = random.Next(0, tokenMas.Length - 1);
                token += tokenMas[ti];
            }

            var check_token = Connect.data.users.Where(x => x.token == token).FirstOrDefault();
            
            if (check_token == null)
            {
                return token.ToString();
            }
            return "";
        }

        public static void fatalError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Fatal error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void deskHelp(StackPanel s_description, TextBlock description, string Title)
        {
            var desc_data = Connect.data.page_desc.Where(x => x.title == Title).FirstOrDefault();
            if (desc_data == null)
            {
                s_description.HorizontalAlignment = HorizontalAlignment.Center;
                s_description.VerticalAlignment = VerticalAlignment.Center;
                description.Text = "Нет описания";
                return;
            }
            if (desc_data.description == null)
            {
                s_description.HorizontalAlignment = HorizontalAlignment.Center;
                s_description.VerticalAlignment = VerticalAlignment.Center;
                description.Text = "Нет описания";
                return;
            }
            description.Text = desc_data.description;
            s_description.HorizontalAlignment = HorizontalAlignment.Stretch;
            s_description.VerticalAlignment = VerticalAlignment.Top;
        }

        public static void ShowCode(string Title)
        {
            ImageView imageWin = new ImageView(Title);
            imageWin.Show();
        }

        private static bool addred = false;
        public static void addDesc(Button addDesc, TextBlock description, TextBox textbox_desc, StackPanel s_description, string Title)
        {
            var pagetitle = Connect.data.page_desc.Where(x => x.title == Title).FirstOrDefault();
            switch (addred)
            {
                case false:
                    try
                    {
                        var selectPage = Connect.data.page_desc.Where(x => x.title == Title).FirstOrDefault();
                        if (selectPage == null)
                        {
                            MessageBoxResult result = MessageBox.Show("Страница " + Title + " отсутствует в базе данных, вы хотите создать ее?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                            if (result == MessageBoxResult.Yes)
                            {
                                page_desc page_Desc = new page_desc()
                                {
                                    title = Title,
                                };
                                Connect.data.page_desc.Add(page_Desc);
                                Connect.data.SaveChanges();
                            }
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        WinObj.fatalError(ex);
                    }
                    addDesc.Content = "Закончить редактирование";
                    addDesc.Background = (SolidColorBrush)Application.Current.FindResource("cyancolor");
                    description.Visibility = Visibility.Collapsed;
                    textbox_desc.Visibility = Visibility.Visible;
                    s_description.HorizontalAlignment = HorizontalAlignment.Stretch;
                    s_description.VerticalAlignment = VerticalAlignment.Top;
                    if (pagetitle.description != null)
                    {
                        textbox_desc.Text = pagetitle.description;
                    }
                    addred = true;
                    break;
                default:
                    addDesc.Content = "Редактировать описание";
                    addDesc.Background = (SolidColorBrush)Application.Current.FindResource("greencolor");
                    description.Visibility = Visibility.Visible;
                    textbox_desc.Visibility = Visibility.Collapsed;
                    addred = false;
                    try
                    {
                        string descdata = textbox_desc.Text;
                        if (descdata.Length == 0)
                        {
                            descdata = null;
                        }
                        pagetitle.description = descdata;
                        Connect.data.SaveChanges();

                        Win.method.ShowErrorMessage("Успех", "Описание обновлено", "ok");
                    }
                    catch (Exception ex)
                    {
                        WinObj.fatalError(ex);
                    }
                    break;
            }
        }

    }
}
