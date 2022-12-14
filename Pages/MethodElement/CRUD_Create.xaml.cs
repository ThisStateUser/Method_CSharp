using MethodHelper.BD;
using MethodHelper.Controllers;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
