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

namespace MethodHelper.Pages
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        List<Button> AllButton = new List<Button>();
        public HomePage()
        {
            InitializeComponent();
        }

        private void SearchMain_TextChanged(object sender, TextChangedEventArgs e)
        {
            WrapHomeBtn.Children.Clear();
            List<Button> SBtn = AllButton.Where(x =>
                            x.Content.ToString().ToLower().Contains(SearchMain.Text.ToLower().Trim()) ||
                            x.Tag.ToString().ToLower().Contains(SearchMain.Text.ToLower().Trim())).ToList();
            if (SBtn != null)
            {
                foreach (var item in SBtn)
                {
                    WrapHomeBtn.Children.Add(item);
                }
            }
        }
    }
}
