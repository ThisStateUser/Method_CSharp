//using MethodHelper.BD;
//using MethodHelper.Controllers;
//using System;
//using System.Linq;
//using System.Windows;
//using System.Windows.Controls;

//namespace MethodHelper.Pages.MethodElement
//{
//    /// <summary>
//    /// Логика взаимодействия для CRUD.xaml
//    /// </summary>
//    public partial class CRUD : Page
//    {
//        public CRUD()
//        {
//            InitializeComponent();
//            update();
//            addComboBox.ItemsSource = Connect.data.method_crud_combobox.ToList();
//            UpdateComboBox.ItemsSource = Connect.data.method_crud_combobox.ToList();
//        }

//        private void update()
//        {
//            DataDG.ItemsSource = Connect.data.method_crud.ToList();
//        }

//        private void addElement_Click(object sender, RoutedEventArgs e)
//        {
//            try
//            {
//                if (addTextBox.Text.Length == 0 || addComboBox.SelectedItem == null)
//                {
//                    MessageBox.Show("Заполните данные в поля добавления");
//                    return;
//                }
//                method_crud method_Crud = new method_crud()
//                {
//                    TextBox = addTextBox.Text.Trim(),
//                    ComboBox = ((method_crud_combobox)addComboBox.SelectedItem).id,
//                };
//                Connect.data.method_crud.Add(method_Crud);
//                Connect.data.SaveChanges();

//                MessageBox.Show("Успешно добавлено");
//                update();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }
//        }

//        private void UpdateElement_Click(object sender, RoutedEventArgs e)
//        {
//            if (UpdateIdElement.Text.Length == 0)
//            {
//                MessageBox.Show("Заполните поле id для редактирования");
//                return;
//            }
//            int idel = Convert.ToInt32(UpdateIdElement.Text);
//            var item = Connect.data.method_crud.Where(x => x.id == idel).FirstOrDefault();

//            try
//            {
//                if (item == null)
//                {
//                    MessageBox.Show("Данный объект " + idel.ToString() + " отсутствует в базе");
//                    return;
//                }
//                if (UpdateTextBox.Text.Length == 0 && UpdateComboBox.SelectedItem == null)
//                {
//                    MessageBox.Show("Заполните хотя бы одно поле редактирования");
//                    return;
//                }
//                if (UpdateTextBox.Text.Length != 0)
//                {
//                    item.TextBox = UpdateTextBox.Text.Trim();
//                }
//                if (UpdateComboBox.SelectedItem != null)
//                {
//                    item.ComboBox = ((method_crud_combobox)UpdateComboBox.SelectedItem).id;
//                }
//                Connect.data.SaveChanges();
//                MessageBox.Show("Успешно обновлен объект с id " + idel);
//                update();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }

//        }

//        private void GetElement_Click(object sender, RoutedEventArgs e)
//        {
//            if (GetIdElement.Text.Length == 0)
//            {
//                MessageBox.Show("Заполните поле id для получения объекта");
//                return;
//            }
//            int idel = Convert.ToInt32(GetIdElement.Text);
//            var item = Connect.data.method_crud.Where(x => x.id == idel).FirstOrDefault();
//            if (item == null)
//            {
//                MessageBox.Show("Данный объект " + idel.ToString() + " отсутствует в базе");
//                return;
//            }
//            GetTextBox.Text = item.TextBox;
//            GetComboBox.Text = item.method_crud_combobox.title;
//            MessageBox.Show("Успешный вывод объекта с id " + idel);
//        }

//        private void RemoveElement_Click(object sender, RoutedEventArgs e)
//        {
//            if (ElementIdRemove.Text.Length == 0)
//            {
//                MessageBox.Show("Заполните поле id для удаления");
//                return;
//            }
//            int idel = Convert.ToInt32(ElementIdRemove.Text);
//            var item = Connect.data.method_crud.Where(x => x.id == idel).FirstOrDefault();
//            try
//            {
//                if (item == null)
//                {
//                    MessageBox.Show("Данный объект " + idel.ToString() + " отсутствует в базе");
//                    return;
//                }
//                Connect.data.method_crud.Remove(item);
//                Connect.data.SaveChanges();
//                MessageBox.Show("Объект с id " + idel + " удален");
//                update();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }
//        }
//    }
//}
