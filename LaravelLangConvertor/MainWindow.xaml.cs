using Microsoft.Win32;
using System.IO;
using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;
using LaravelLangConvertor.Translator;
using LaravelLangConvertor.Parser.LangParsers;
using System.Configuration;

namespace LaravelLangConvertor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            APIKey = ConfigurationManager.AppSettings["Apikey"];
        }
        Dictionary<string, string> fileValues = new Dictionary<string, string>();
        List<string> translatedValues = new List<string>();
        private string selectedLanguage = "en";
        private string APIKey; 
        private void FilePickerButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                SelectedFileTextBlock.Text = openFileDialog.FileName;

                // Load the selected file and display its content in the list view
                var parser = new StringParser();
                fileValues = parser.GetParsedLaravelKeyValuePair(openFileDialog.FileName);
                foreach(var item in fileValues)
                {
                    ListView.Items.Add(new { Column1 = item.Key, Column2 = item.Value, Column3 = "" });

                }
            }
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow();
            if (settingsWindow.ShowDialog() == true)
            {
                string apiKey = settingsWindow.ApiKey;
                
            }
        }

        private void WriteButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a dictionary with the data from your UI

            // Define the file path and name for the PHP language file
            string filePath =  SelectedFileTextBlock.Text;
            string filenameFromPath = Path.GetFileName(filePath);
            string directory = Path.GetDirectoryName(filePath);
            filePath = Path.Combine(directory, selectedLanguage + filenameFromPath);
            // Create the PHP language file and write the dictionary data to it
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("<?php");
                writer.WriteLine("return [");
                var dictionary = new Dictionary<string, string>();
                int i = 0;
                foreach(var item in fileValues) { 
                    string key = item.Key;
                    string value = translatedValues[i].Replace("'", "\\'");
                    if(i < fileValues.Count - 1)
                        writer.WriteLine($"'{key}' => '{value}',");
                    else
                        writer.WriteLine($"'{key}' => '{value}'");
                    i++;
                }

                writer.WriteLine("];");
            }

            MessageBox.Show("PHP language file generated successfully.");
        }
        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = LanguageComboBox.SelectedItem as ComboBoxItem;
            selectedLanguage = selectedItem.Tag as string;

            // Do something with the selected language code (e.g., store it in a variable)
        }
        private async void TranslateButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedItem = LanguageComboBox.SelectedItem as ComboBoxItem;
            string targetLanguageCode = selectedItem.Tag as string;
            string translator = ((ComboBoxItem)TranslatorComboBox.SelectedItem).Tag.ToString();
            ILangTranslator translatorObject = null;
            if (translator == "deepl")
            {
                 translatorObject = new DeepLTranslator(APIKey);
            }
            if (translatorObject == null)
                return;
            translatedValues = await translatorObject.TranslateToLanguage(fileValues, selectedLanguage);
            ListView.Items.Clear();
            if(translatedValues.Count > 0)
            {
                int index = 0;
                foreach(var item in fileValues)
                {
                    ListView.Items.Add(new { Column1 = item.Key, Column2 = item.Value, Column3 = translatedValues[index] });

                    index++;
                }

            }
        }
      
    }
}
