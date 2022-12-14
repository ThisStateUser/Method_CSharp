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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MethodHelper.Controllers;
using MethodHelper.Pages.MethodElement;

namespace MethodHelper.Pages.MainPage
{
    /// <summary>
    /// Логика взаимодействия для MethodPage.xaml
    /// </summary>
    public partial class MethodPage : Page
    {           
        List<Button> AllButton = new List<Button>();
        public MethodPage()
        {
            InitializeComponent();
            foreach (var item in WrapMethodBtn.Children)
            {
                AllButton.Add((Button)item);
            };
        }

        private void GoCrud_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.MainFrame.Navigate(new CRUD());
        }
        private void GoCrudRead_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.MainFrame.Navigate(new CRUD_Read());
        }
        private void GoCrudCreate_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.MainFrame.Navigate(new CRUD_Create());
        }

        private void SearchMethod_TextChanged(object sender, TextChangedEventArgs e)
        {
            WrapMethodBtn.Children.Clear();
            List<Button> SBtn = AllButton.Where(x => 
                            x.Content.ToString().ToLower().Contains(SearchMethod.Text.ToLower().Trim()) || 
                            x.Tag.ToString().ToLower().Contains(SearchMethod.Text.ToLower().Trim())).ToList();
            if (SBtn != null)
            {
                foreach (var item in SBtn)
                {
                    WrapMethodBtn.Children.Add(item);
                }
            }
        }


    }
}
