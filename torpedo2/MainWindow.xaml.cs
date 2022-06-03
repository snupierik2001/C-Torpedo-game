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

namespace torpedo2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new System.Uri("pages/SinglePlayer.xaml", UriKind.Relative));
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new System.Uri("pages/SinglePlayer.xaml", UriKind.Relative));
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new System.Uri("pages/MultiPlayer.xaml", UriKind.Relative));
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new System.Uri("pages/Record.xaml", UriKind.Relative));
        }
    }
}
