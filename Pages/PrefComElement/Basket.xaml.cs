using MethodHelper.BD;
using MethodHelper.Controllers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MethodHelper.Pages.PrefComElement
{
    /// <summary>
    /// Логика взаимодействия для Basket.xaml
    /// </summary>
    public partial class Basket : Page
    {
        public Basket()
        {
            InitializeComponent();
            var prod = Connect.data.product.ToList();
            Lv_magazine.ItemsSource = prod;
            Filter.ItemsSource = Connect.data.category.ToList();

            AllCard.Text = prod.Count.ToString();
            Filter.SelectedIndex = 0;
            Sorting.SelectedIndex = 0;

            WinObj.deskHelp(s_description, description, Title);
            if (Connect.user.role_id != 3)
            {
                addDesc.Visibility = Visibility.Visible;
            }
        }

        private void ShowCode_Click(object sender, RoutedEventArgs e)
        {
            WinObj.ShowCode(Title);
        }

        private void addDesc_Click(object sender, RoutedEventArgs e)
        {
            WinObj.addDesc(addDesc, description, textbox_desc, s_description, Title);
            WinObj.deskHelp(s_description, description, Title);
        }

        private void Update()
        {
            Model1.UpdateContext();
            var product = BD.Model1.GetContext().product.ToList();

            switch (Sorting.SelectedIndex)
            {
                case 1:
                    product = product.OrderByDescending(s => s.price).ToList();
                    break;
                case 2:
                    product = product.OrderBy(s => s.price).ToList();
                    break;
                default:
                    break;
            }

            if (Filter.SelectedIndex == 1)
            {
                product = product.Where(x => x.category_id == 2).ToList();
            }
            if (Filter.SelectedIndex == 2)
            {
                product = product.Where(x => x.category_id == 3).ToList();
            }

            product = product.Where(x => x.title.ToLower().Trim().Contains(SearchMB.Text.ToLower().Trim()) || x.desk.ToLower().Trim().Contains(SearchMB.Text.ToLower().Trim())).ToList();

            CurrentCard.Text = product.Count.ToString();
            Lv_magazine.ItemsSource = product;
        }

        private void StoreBtn_Click(object sender, RoutedEventArgs e)
        {
            Lv_magazine.Visibility = Visibility.Visible;
            Lv_Basket.Visibility = Visibility.Collapsed;

            BasketBtn.Background = (SolidColorBrush)FindResource("buttonColor");
            StoreBtn.Background = (SolidColorBrush)FindResource("cyancolor");
        }

        private void BasketBtn_Click(object sender, RoutedEventArgs e)
        {
            Lv_magazine.Visibility = Visibility.Collapsed;
            Lv_Basket.Visibility = Visibility.Visible;

            BasketBtn.Background = (SolidColorBrush)FindResource("cyancolor");
            StoreBtn.Background = (SolidColorBrush)FindResource("buttonColor");
            //string path = "";
            //var file = new OpenFileDialog();
            //file.Filter = "Изображение Img|*.png;*.jpg";
            //if (file.ShowDialog() == true)
            //{
            //    path = file.FileName;
            //    BitmapImage image = new BitmapImage();
            //    MemoryStream ms = new MemoryStream(File.ReadAllBytes(path));
            //    image.BeginInit();
            //    image.StreamSource = ms;
            //    image.EndInit();
            //}
        }

        private void Sorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
        private void SearchMB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void AddBasket_Click(object sender, RoutedEventArgs e)
        {
            int ProductId = Convert.ToInt32(((Button)sender).Tag);
            int ProductCount = Convert.ToInt32(((TextBox)((StackPanel)((Border)((StackPanel)((Button)sender).Parent).Children[1]).Child).Children[1]).Text);
            var Product = Connect.data.product.Where(x => x.id == ProductId).FirstOrDefault();

            if (ProductCount > Product.count)
            {
                ProductCount = Product.count;
            }
            if (ProductCount < 1)
            {
                ProductCount = 1;
            }

            try
            {
                MessageBoxResult result = MessageBox.Show("Добавить продукт \"" + Product.title + "\" в колличестве " + ProductCount + " шт. в корзину?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.No)
                {
                    return;
                }

                Connect.data.basket.Add(new basket()
                {
                    product_id = ProductId,
                    user_id = Connect.user.id,
                    count_prod = ProductCount,
                });
                Connect.data.SaveChanges();
            }
            catch (Exception ex)
            {
                WinObj.fatalError(ex);
            }
        }


    }
}
