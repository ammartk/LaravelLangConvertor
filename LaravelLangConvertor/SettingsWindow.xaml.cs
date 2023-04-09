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
using System.Windows.Shapes;
using System.Runtime;
using System.Configuration;
namespace LaravelLangConvertor
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public string ApiKey { get; set; }

        public SettingsWindow()
        {
            InitializeComponent();
            ApiKey = "";
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Store the entered API key in cache
            string apiKey = ApiKeyTextBox.Text;
            ConfigurationManager.AppSettings["Apikey"]= apiKey;
            // Set the DialogResult to true and close the window
            DialogResult = true;
            Close();
        }

    }

}
