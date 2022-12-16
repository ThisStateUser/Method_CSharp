using MethodHelper.BD;
using MethodHelper.Controllers;
using MethodHelper.Views;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MethodHelper.Pages.UIElement
{
    /// <summary>
    /// Логика взаимодействия для GridAndPanel.xaml
    /// </summary>
    public partial class GridAndPanel : Page
    {
        Panel panel;
        public GridAndPanel()
        {
            InitializeComponent();
            panel = GridUI;
            ShowWH();
            WinObj.deskHelp(s_description, description, Title);
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

        private void AnimGridAndPanel(Panel UIelement, string WidthOrHeight, int property)
        {
            switch (WidthOrHeight.ToLower())
            {
                case "width":
                    DoubleAnimation resize = new DoubleAnimation()
                    {
                        From = UIelement.Width,
                        To = property,
                        Duration = TimeSpan.FromSeconds(3),
                        EasingFunction = new QuadraticEase(),
                    };
                    UIelement.BeginAnimation(WidthProperty, resize);
                    break;
                case "height":
                    DoubleAnimation resize2 = new DoubleAnimation()
                    {
                        From = UIelement.Height,
                        To = property,
                        Duration = TimeSpan.FromSeconds(3),
                        EasingFunction = new QuadraticEase(),
                    };
                    UIelement.BeginAnimation(HeightProperty, resize2);
                    break;
                default:
                    MessageBox.Show("ERROR");
                    break;
            }

        }

        private void ResizeFirst_Click(object sender, RoutedEventArgs e)
        {
            AnimGridAndPanel(panel, "width", 400);
            AnimGridAndPanel(panel, "height", 400);
        }

        private void ResizeUpDown_Click(object sender, RoutedEventArgs e)
        {
            AnimGridAndPanel(panel, "height", 600);
        }

        private void ResizeLeftRight_Click(object sender, RoutedEventArgs e)
        {
            AnimGridAndPanel(panel, "width", 600);
        }

        private void ResizeLast_Click(object sender, RoutedEventArgs e)
        {
            AnimGridAndPanel(panel, "width", 600);
            AnimGridAndPanel(panel, "height", 600);
        }

        private void ResizeWrap_Click(object sender, RoutedEventArgs e)
        {
            AnimGridAndPanel(WrapVert3, "height", 120);
        }
        private void ResizeWrap1_Click(object sender, RoutedEventArgs e)
        {
            AnimGridAndPanel(WrapVert3, "height", 190);
        }
        private void ResizeWrap2_Click(object sender, RoutedEventArgs e)
        {
            AnimGridAndPanel(WrapVert3, "height", 80);
        }

        private void RepeatMethod()
        {
            GridUI.Visibility = Visibility.Collapsed;
            StackPanelUI.Visibility = Visibility.Collapsed;
            DockPanelUI.Visibility = Visibility.Collapsed;
            WrapPanelUI.Visibility = Visibility.Collapsed;

            GridPage.Background = (SolidColorBrush)FindResource("buttonColor");
            StackPage.Background = (SolidColorBrush)FindResource("buttonColor");
            DockPage.Background = (SolidColorBrush)FindResource("buttonColor");
            WrapPage.Background = (SolidColorBrush)FindResource("buttonColor");

            WrapChange.Visibility = Visibility.Collapsed;
            ShowWH();
        }

        private void ShowWH()
        {
            StackPanel stackPanel = XShowWH;
            stackPanel.Children.Clear();

            Binding bindingWidth = new Binding();
            bindingWidth.ElementName = panel.Name.ToString();
            bindingWidth.Path = new PropertyPath("ActualWidth");

            Binding bindingHeight = new Binding();
            bindingHeight.ElementName = panel.Name.ToString();
            bindingHeight.Path = new PropertyPath("ActualHeight");

            TextBlock textBlockWidth = new TextBlock();
            textBlockWidth.Text = "Width: ";

            TextBlock textWidth = new TextBlock();
            textWidth.Margin = new Thickness(0, 0, 10, 0);
            textWidth.MaxWidth = (double)26;
            textWidth.SetBinding(TextBlock.TextProperty, bindingWidth);

            TextBlock textBlockHeight = new TextBlock();
            textBlockHeight.Text = "Height: ";

            TextBlock textHeight = new TextBlock();
            textHeight.MaxWidth = (double)26;
            textHeight.SetBinding(TextBlock.TextProperty, bindingHeight);

            stackPanel.Children.Add(textBlockWidth);
            stackPanel.Children.Add(textWidth);
            stackPanel.Children.Add(textBlockHeight);
            stackPanel.Children.Add(textHeight);
        }

        private void GridPage_Click(object sender, RoutedEventArgs e)
        {
            panel = GridUI;
            RepeatMethod();
            GridPage.Background = (SolidColorBrush)FindResource("cyancolor");
            GridUI.Visibility = Visibility.Visible;
        }

        private void StackPage_Click(object sender, RoutedEventArgs e)
        {
            panel = StackPanelUI;
            RepeatMethod();
            StackPage.Background = (SolidColorBrush)FindResource("cyancolor");
            StackPanelUI.Visibility = Visibility.Visible;
        }

        private void DockPage_Click(object sender, RoutedEventArgs e)
        {
            panel = DockPanelUI;
            RepeatMethod();
            DockPage.Background = (SolidColorBrush)FindResource("cyancolor");
            DockPanelUI.Visibility = Visibility.Visible;
        }

        private void WrapPage_Click(object sender, RoutedEventArgs e)
        {
            panel = WrapPanelUI;
            RepeatMethod();
            WrapPage.Background = (SolidColorBrush)FindResource("cyancolor");
            WrapChange.Visibility = Visibility.Visible;
            WrapPanelUI.Visibility = Visibility.Visible;
        }
    }
}
