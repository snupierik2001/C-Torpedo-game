using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using torpedo2.data;
using torpedo2.json;

namespace Torpedo.pages
{
    /// <summary>
    /// Interaction logic for Record.xaml
    /// </summary>
    public partial class Record : Page
    {
        JSONData dataWriter;
        public Record()
        {
            InitializeComponent();
            WriteToTextBox();
        }

        public void WriteToTextBox()
        {
            dataWriter = new JSONData();
            //jsonTextBox.Text = dataWriter.ReadJsonString();
            IList<WinState> gameOutcomes = dataWriter.ReadJson();
            dataGrid.ItemsSource = gameOutcomes;

        }
    }
}
