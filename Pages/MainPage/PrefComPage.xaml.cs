﻿using MethodHelper.Controllers;
using MethodHelper.Pages.PrefComElement;
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

namespace MethodHelper.Pages.MainPage
{
    /// <summary>
    /// Логика взаимодействия для PrefComPage.xaml
    /// </summary>
    public partial class PrefComPage : Page
    {
        public PrefComPage()
        {
            InitializeComponent();
        }

        private void GoBasket_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.MainFrame.Navigate(new Basket());
        }
    }
}
