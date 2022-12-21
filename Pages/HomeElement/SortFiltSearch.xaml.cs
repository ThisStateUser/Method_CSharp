using MethodHelper.Controllers;
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

namespace MethodHelper.Pages.HomeElement
{
    /// <summary>
    /// Логика взаимодействия для SortFiltSearch.xaml
    /// </summary>
    public partial class SortFiltSearch : Page
    {
        public SortFiltSearch()
        {
            InitializeComponent();
            LV_ListView.ItemsSource = Connect.data.product.ToList();
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

        private void ProductList()
        {
            var product = Connect.data.product.ToList();
        }
    }
}
