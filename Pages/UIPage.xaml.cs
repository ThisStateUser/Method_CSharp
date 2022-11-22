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

namespace MethodHelper.Pages
{
    /// <summary>
    /// Логика взаимодействия для UIPage.xaml
    /// </summary>
    public partial class UIPage : Page
    {
        public UIPage()
        {
            InitializeComponent();
        }

        private void GridAndPanel_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.MainFrame.Navigate(new GridAndPanel());
        }
    }
}
