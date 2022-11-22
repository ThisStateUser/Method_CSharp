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

namespace MethodHelper.Pages
{
    /// <summary>
    /// Логика взаимодействия для MethodPage.xaml
    /// </summary>
    public partial class MethodPage : Page
    {
        public MethodPage()
        {
            InitializeComponent();

        }

        private void GoCrud_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.MainFrame.Navigate(new CRUD());
        }
    }
}
