using System;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;

using Microsoft.Win32;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        string cur_file_path = "0";
        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
            Loaded += MainWindow_Loaded;


        }
        const string filepath = "/Users/user/Documents/Learning/Labs/2_sem/TPP/Lab4,7/WpfApp2/doc1.txt";




        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string content = File.ReadAllText(filepath);
            MainTextField.Text = content;

            ThemeBox.SelectedIndex = 0;
            FontBox.SelectedIndex = 3;
            SizeBox.SelectedIndex = 0;
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            string textToSave = MainTextField.Text;
            while (textToSave.Contains("\r\n") || textToSave.Contains("\n\n"))
            {
                textToSave = textToSave.Replace("\r\n", "\n");
                textToSave = textToSave.Replace("\n\n", "\n");
            }
            try
            {
                File.WriteAllText(filepath, textToSave);


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ThemeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string selectedValue = ((ComboBoxItem)ThemeBox.SelectedItem).Content.ToString();
            switch (selectedValue)
            {
                case "Light":
                    MainTextField.Background = Brushes.White;
                    MainTextField.Foreground = Brushes.Black;
                    break;
                case "Dark":
                    Color darkC = (Color)ColorConverter.ConvertFromString("#222222");
                    Color TxtCol = (Color)ColorConverter.ConvertFromString("#B8B8B8");
                    MainTextField.Background = new SolidColorBrush(darkC);
                    MainTextField.Foreground = new SolidColorBrush(TxtCol);
                    break;
                case "Cream":
                    Color cream = (Color)ColorConverter.ConvertFromString("#FAEEDC");
                    MainTextField.Background = new SolidColorBrush(cream);
                    MainTextField.Foreground = Brushes.Black;
                    break;
            }

        }
        private void FontSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string selectedValue = ((ComboBoxItem)FontBox.SelectedItem).Content.ToString();
            MainTextField.FontFamily = new FontFamily(selectedValue);
            
        }
        private void SizeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedValue = ((ComboBoxItem)SizeBox.SelectedItem).Content.ToString();
            MainTextField.FontSize = int.Parse(selectedValue);

            
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openTxtDialog = new OpenFileDialog();
            if (openTxtDialog.ShowDialog() == true)
            {
                string filepath = openTxtDialog.FileName;
                MainTextField.Text = File.ReadAllText(filepath);
            }
        }
        private void SaveAs(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveTxtDialog = new SaveFileDialog();
            if (saveTxtDialog.ShowDialog() == true){
                string filepath = saveTxtDialog.FileName;
                using (StreamWriter writer = new StreamWriter(filepath))
                {
                    writer.Write(MainTextField.Text);
                }
                cur_file_path = filepath;
            }
            
        }
        private void SaveFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveTxtDialog = new SaveFileDialog();
            if (cur_file_path =="0") {
                SaveAs(sender, e);
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(cur_file_path))
                {
                    writer.Write(MainTextField.Text);
                }
            }
        }
        private void NewFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveTxtDialog = new SaveFileDialog();
            if (saveTxtDialog.ShowDialog() == true)
            {
                cur_file_path = saveTxtDialog.FileName;
            }
        }
        private void Voice(object sender, RoutedEventArgs e)
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Speak(MainTextField.Text);
        }
    }
}

