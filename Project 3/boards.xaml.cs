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
using System.Windows.Shapes;

namespace Project_3
{
    /// <summary>
    /// Логика взаимодействия для boards.xaml
    /// </summary>
    public partial class boards : Window
    {
        public boards()
        {
            InitializeComponent();
        }
        private void go_to_add_board(object sender, RoutedEventArgs e)
        {
            AddTask addTask = new AddTask();
            addTask.Show();
            
        }
    }
}
