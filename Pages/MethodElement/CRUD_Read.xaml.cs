using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MethodHelper.BD;
using MethodHelper.Controllers;
using MethodHelper.Views;

namespace MethodHelper.Pages.MethodElement
{
    /// <summary>
    /// Логика взаимодействия для CRUD_Read.xaml
    /// </summary>
    public partial class CRUD_Read : Page
    {
        List<method_crud> ListCrud = new List<method_crud>();
        public CRUD_Read()
        {
            InitializeComponent();
            ListCrud = Connect.data.method_crud.ToList();
            LV_ListView.ItemsSource = Connect.data.method_crud.ToList();
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

        private async void ListViewPage_Click(object sender, RoutedEventArgs e)
        {
            AllHide();
            ListViewPage.Background = (SolidColorBrush)FindResource("cyancolor");
            LV_ListView.ItemsSource = Connect.data.method_crud.ToList();
            await Task.Delay(600);
            anim(LV_ListView, 630);
        }

        private async void ListBoxPage_Click(object sender, RoutedEventArgs e)
        {
            AllHide();
            ListBoxPage.Background = (SolidColorBrush)FindResource("cyancolor");
            LB_ListBox.ItemsSource = Connect.data.method_crud.ToList();
            await Task.Delay(600);
            anim(LB_ListBox, 630);
        }

        private async void DataGridPage_Click(object sender, RoutedEventArgs e)
        {
            AllHide();
            DataGridPage.Background = (SolidColorBrush)FindResource("cyancolor");
            DG_DateGrid.ItemsSource = Connect.data.method_crud.ToList();
            await Task.Delay(600);
            anim(DG_DateGrid, 630);
        }
        private async void NativePage_Click(object sender, RoutedEventArgs e)
        {
            AllHide();
            NativePage.Background = (SolidColorBrush)FindResource("cyancolor");
            ListGenerate();
            await Task.Delay(600);
            anim(N_StackPanel, 630);
        }

        public void anim(dynamic UIElem, int prop)
        {
            DoubleAnimation animation = new DoubleAnimation()
            {
                From = UIElem.Height,
                To = prop,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new QuadraticEase(),
            };
            UIElem.BeginAnimation(HeightProperty, animation);
        }

        public void AllHide()
        {
            anim(LV_ListView, 0);
            anim(LB_ListBox, 0);
            anim(DG_DateGrid, 0);
            anim(N_StackPanel, 0);

            ListViewPage.Background = (SolidColorBrush)FindResource("buttonColor");
            ListBoxPage.Background = (SolidColorBrush)FindResource("buttonColor");
            DataGridPage.Background = (SolidColorBrush)FindResource("buttonColor");
            NativePage.Background = (SolidColorBrush)FindResource("buttonColor");
        }


        public void ListGenerate()
        {
            NV_StackPanel.Children.Clear();

            foreach (var item in ListCrud)
            {
                Border border = new Border();
                border.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("Black");
                border.Padding = new Thickness(5);
                border.Width = (double)550;
                border.Margin = new Thickness(0, 5, 0, 5);
                border.CornerRadius = new CornerRadius(10);

                StackPanel stackPanel_1 = new StackPanel();
                stackPanel_1.Orientation = Orientation.Horizontal;

                StackPanel stackPanel_2 = new StackPanel();

                StackPanel stackPanel_3 = new StackPanel();
                stackPanel_3.Orientation = Orientation.Horizontal;

                TextBlock textBlock_3_1 = new TextBlock()
                { Text = "Текст: ", };

                TextBlock textBlock_3_2 = new TextBlock()
                {
                    Text = item.row_text,
                    TextWrapping = TextWrapping.Wrap,
                    Width = (double)298,
                };

                StackPanel stackPanel_4 = new StackPanel();
                stackPanel_4.Orientation = Orientation.Horizontal;

                TextBlock textBlock_4_1 = new TextBlock()
                { Text = "Число: ", };

                TextBlock textBlock_4_2 = new TextBlock()
                { Text = item.row_int.ToString(), };

                StackPanel stackPanel_5 = new StackPanel();
                stackPanel_5.Orientation = Orientation.Horizontal;

                TextBlock textBlock_5_1 = new TextBlock()
                { Text = "Чекбокс: ", };

                CheckBox checkBox_5_2 = new CheckBox()
                { IsChecked = item.row_bool, };

                Button button = new Button()
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Style = (Style)FindResource("MainBtn"),
                    Content = "Кнопка",
                    Width = (double)200,
                };

                StackPanel stackPanel_20 = new StackPanel()
                { Margin = new Thickness(5, 0, 0, 0), };

                StackPanel stackPanel_6 = new StackPanel();
                stackPanel_6.Orientation = Orientation.Horizontal;

                TextBlock textBlock_6_1 = new TextBlock() 
                { Text = "Изображение: ", };

                Border border1 = new Border()
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("White"),
                    CornerRadius = new CornerRadius(5),
                };

                Image image_6_2 = new Image()
                { 
                    Width = (double)80,
                    Height = (double)80,
                    Source = item.picture, 
                };

                NV_StackPanel.Children.Add(border);
                border.Child = stackPanel_1;

                stackPanel_1.Children.Add(stackPanel_2);

                stackPanel_2.Children.Add(stackPanel_3);
                stackPanel_3.Children.Add(textBlock_3_1);
                stackPanel_3.Children.Add(textBlock_3_2);

                stackPanel_2.Children.Add(stackPanel_4);
                stackPanel_4.Children.Add(textBlock_4_1);
                stackPanel_4.Children.Add(textBlock_4_2);

                stackPanel_2.Children.Add(stackPanel_5);
                stackPanel_5.Children.Add(textBlock_5_1);
                stackPanel_5.Children.Add(checkBox_5_2);

                stackPanel_2.Children.Add(button);

                stackPanel_1.Children.Add(stackPanel_20);

                stackPanel_20.Children.Add(stackPanel_6);
                stackPanel_6.Children.Add(textBlock_6_1);
                stackPanel_6.Children.Add(border1);
                border1.Child = image_6_2;
            }
        }
    }
}
