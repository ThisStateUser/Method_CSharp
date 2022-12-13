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
using MethodHelper.Pages.UIElement;

namespace MethodHelper.Pages.MainPage
{
    /// <summary>
    /// Логика взаимодействия для UIPage.xaml
    /// </summary>
    public partial class UIPage : Page
    {
        List<Button> AllButton = new List<Button>();
        public UIPage()
        {
            InitializeComponent();
            foreach (var item in WrapUIBtn.Children)
            {
                AllButton.Add((Button)item);
            };
        }

        private void GridAndPanel_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.MainFrame.Navigate(new GridAndPanel());
        }

        private void SearchUI_TextChanged(object sender, TextChangedEventArgs e)
        {
            WrapUIBtn.Children.Clear();
            List<Button> SBtn = AllButton.Where(x =>
                            x.Content.ToString().ToLower().Contains(SearchUI.Text.ToLower().Trim()) ||
                            x.Tag.ToString().ToLower().Contains(SearchUI.Text.ToLower().Trim())).ToList();
            if (SBtn != null)
            {
                foreach (var item in SBtn)
                {
                    WrapUIBtn.Children.Add(item);
                }
            }
        }
    }
}
