﻿using System;
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
using System.Windows.Media.Animation;
using System.Xml.Linq;
using System.Runtime.InteropServices;

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

        private void Collapse()
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
        }

        private void GridPage_Click(object sender, RoutedEventArgs e)
        {
            Collapse();
            panel = GridUI;
            GridPage.Background = (SolidColorBrush)FindResource("color2");
            GridUI.Visibility = Visibility.Visible;
        }

        private void StackPage_Click(object sender, RoutedEventArgs e)
        {
            Collapse();
            panel = StackPanelUI;
            StackPage.Background = (SolidColorBrush)FindResource("color2");
            StackPanelUI.Visibility = Visibility.Visible;
        }

        private void DockPage_Click(object sender, RoutedEventArgs e)
        {
            Collapse();
            panel = DockPanelUI;
            DockPage.Background = (SolidColorBrush)FindResource("color2");
            DockPanelUI.Visibility = Visibility.Visible;
        }

        private void WrapPage_Click(object sender, RoutedEventArgs e)
        {
            Collapse();
            WrapChange.Visibility = Visibility.Visible;
            panel = WrapPanelUI;
            WrapPage.Background = (SolidColorBrush)FindResource("color2");
            WrapPanelUI.Visibility = Visibility.Visible;
        }
    }
}
