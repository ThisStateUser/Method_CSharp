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

namespace MethodHelper.Pages
{
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            MenuAnim.IsChecked = WinObj.settings.menu_anim.Value;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var setting = Connect.data.app_settings.Where(x => x.id == WinObj.settings.id).FirstOrDefault();
            try
            {
                bool v_menu_anim = WinObj.settings.menu_anim.Value;
                switch (MenuAnim.IsChecked)
                {
                    case true:
                        v_menu_anim = true;
                        break;
                    case false:
                        v_menu_anim = false;
                        break;
                    default:
                        break;
                }
                setting.menu_anim = v_menu_anim;
                Connect.data.SaveChanges();
                WinObj.settings = Connect.data.app_settings.Where(x => x.id == 1).FirstOrDefault();
                MessageBox.Show("Изменения сохранены");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
