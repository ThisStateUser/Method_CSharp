using MethodHelper.BD;
using MethodHelper.Controllers;
using MethodHelper.Views;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MethodHelper.Pages.MethodElement
{
    /// <summary>
    /// Логика взаимодействия для CRUD_Create.xaml
    /// </summary>
    public partial class CRUD_Create : Page
    {
        byte[] _imgs;
        public CRUD_Create()
        {
            InitializeComponent();
            ComboBoxEl2.ItemsSource = Connect.data.method_crud_combobox.ToList();
            deskHelp();
        }

        private void deskHelp()
        {
            var desc_data = Connect.data.page_desc.Where(x => x.title == Title).FirstOrDefault();
            if (desc_data == null)
            {
                s_description.HorizontalAlignment = HorizontalAlignment.Center;
                s_description.VerticalAlignment = VerticalAlignment.Center;
                description.Text = "Нет описания";
                return;
            }
            if (desc_data.description.Length == 0)
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

        private void ShowCode_Click(object sender, RoutedEventArgs e)
        {
            string winname = Title;
            ImageView imageWin = new ImageView(winname);
            imageWin.Show();
        }

        bool addred = false;
        private void addDesc_Click(object sender, RoutedEventArgs e)
        {
            var pagetitle = Connect.data.page_desc.Where(x => x.title == Title).FirstOrDefault();
            switch (addred)
            {
                case false:
                    addDesc.Content = "Закончить редактирование";
                    addDesc.Background = (SolidColorBrush)FindResource("cyancolor");
                    description.Visibility = Visibility.Collapsed;
                    textbox_desc.Visibility = Visibility.Visible;
                    s_description.HorizontalAlignment = HorizontalAlignment.Stretch;
                    s_description.VerticalAlignment = VerticalAlignment.Top;
                    if (pagetitle != null)
                    {
                        if (pagetitle.description != null)
                        {
                            textbox_desc.Text = pagetitle.description;
                        }
                    }
                    addred = true;
                    break;
                default:
                    addDesc.Content = "Редактировать описание";
                    addDesc.Background = (SolidColorBrush)FindResource("greencolor");
                    description.Visibility = Visibility.Visible;
                    textbox_desc.Visibility = Visibility.Collapsed;
                    addred = false;
                    try
                    {
                        if (pagetitle == null)
                        {
                            page_desc page = new page_desc()
                            {
                                title = Title,
                                description = textbox_desc.Text,
                            };
                            Connect.data.page_desc.Add(page);
                            Connect.data.SaveChanges();

                            Win.method.ShowErrorMessage("Успех", "Описание добавлено", "ok");
                            return;
                        }
                        pagetitle.description = textbox_desc.Text;
                        Connect.data.SaveChanges();

                        Win.method.ShowErrorMessage("Успех", "Описание обновлено", "ok");
                    }
                    catch (Exception ex)
                    {
                        WinObj.fatalError(ex);
                    }
                    deskHelp();
                    break;
            }
        }

        private void SendCrud_Click(object sender, RoutedEventArgs e)
        {
            // using Microsoft.Win32;
            // using System.IO;

            try
            {
                method_crud method_Crud = new method_crud();

                if (TextBoxEl != null)
                {
                    method_Crud.row_text = TextBoxEl.Text.Trim();
                }
                if (ComboBoxEl.SelectedItem != null)
                {
                    method_Crud.row_int = Convert.ToInt32(ComboBoxEl.SelectedIndex + 1);
                }
                if (ComboBoxEl2.SelectedItem != null)
                {
                    method_Crud.row_combo = ((method_crud_combobox)ComboBoxEl2.SelectedItem).id;
                }
                if (_imgs != null)
                {
                    method_Crud.row_image = _imgs;
                }
                method_Crud.row_bool = CheckBoxEl.IsChecked;

                Connect.data.method_crud.Add(method_Crud);
                Connect.data.SaveChanges();
                Win.method.ShowErrorMessage("Успех!", "Запись добавлена", "ok");
            }
            catch (Exception ex)
            {
                WinObj.fatalError(ex);
            }


        }

        private void selectImg_Click(object sender, RoutedEventArgs e)
        {
            selectImg.IsEnabled = false;
            Task.Run(() => { asyncImg(); });
        }

        private void asyncImg()
        {
            // using Microsoft.Win32;
            // using System.IO;

            var pathImage = new OpenFileDialog();
            pathImage.Filter = "img|*.png;*.jpeg;*.jpg";
            var result = pathImage.ShowDialog();
            if (result != null && result == true)
            {
                var imgdata = File.ReadAllBytes(pathImage.FileName);
                Dispatcher.Invoke(() =>
                {
                    selectImg.Content = pathImage.FileName;
                    _imgs = imgdata;
                    selectImg.IsEnabled = true;
                });
            }
            else if (result == false)
            {
                Dispatcher.Invoke(() =>
                {
                    selectImg.IsEnabled = true;
                });
            }
        }
    }
}
