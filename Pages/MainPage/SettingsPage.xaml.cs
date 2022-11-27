using MethodHelper.Controllers;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using MethodHelper.BD;

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
            StartupPage.ItemsSource = Connect.data.start_page_desk.ToList();
            StartupPage.SelectedIndex = WinObj.settings.start_page.Value;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var setting = Connect.data.app_settings.Where(x => x.user_id == WinObj.settings.user_id).FirstOrDefault();
            if (setting == null)
                return;
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
                setting.start_page = StartupPage.SelectedIndex;
                Connect.data.SaveChanges();
                Win.method.ShowErrorMessage("Успех", "Изменения сохранены", "ok", 3);
            }
            catch (Exception ex)
            {
                WinObj.fatalError(ex);
            }
        }
    }
}
