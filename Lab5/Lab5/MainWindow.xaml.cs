using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using Microsoft.Win32;
using System.Security.Cryptography;

namespace HelloWpfApp
{
    public partial class MainWindow : Window
    {
        string path = @"";
        string prev = "";

        public MainWindow()
        {
            InitializeComponent();
            //if (File.Exists(path)) { tb.Text = File.ReadAllText(path); }
        }

        public void MySave()
        {
            File.WriteAllText(path, tb.Text);
            prev = tb.Text;
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            if (path != @"" && tb.Text != prev)
            {
                MessageBoxResult result = MessageBox.Show("Сохранить изменения в файле?", "Уведомление", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        MySave();
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void File_Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files|*.txt";
            if (ofd.ShowDialog() == true)
            {
                path = ofd.FileName;
                tb.Text = File.ReadAllText(path);
                prev = tb.Text;
            }
        }

        private void File_Save(object sender, RoutedEventArgs e)
        {
            if (path == @"")
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Text Files|*.txt";
                if (sfd.ShowDialog() == true)
                {
                    path = sfd.FileName;
                    MySave();
                }
            }
            else
            {
                MySave();
            }
        }

        private void File_Close(object sender, RoutedEventArgs e)
        {
            path = @"";
            tb.Text = "";
            prev = "";
        }

        private void btnSettings_click(object sender, RoutedEventArgs e)
        {
            Settings.Visibility = Visibility.Hidden;
        }

        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            Settings.Visibility = Visibility.Visible;
        }

        private void CloseSettings(object sender, RoutedEventArgs e)
        {
            Settings.Visibility = Visibility.Hidden;
        }

        private void cb1SelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cb1.SelectedIndex)
            {
                case 0:
                    tb.Background = Brushes.Bisque;
                    break;
                case 1:
                    tb.Background = Brushes.Black;
                    break;
                case 2:
                    tb.Background = Brushes.White;
                    break;
                case 3:
                    tb.Background = Brushes.Red;
                    break;
                case 4:
                    tb.Background = Brushes.Blue;
                    break;
                case 5:
                    tb.Background = Brushes.Green;
                    break;
                default:
                    tb.Background = Brushes.Bisque;
                    break;
            }
        }

        private void cb2SelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cb2.SelectedIndex)
            {
                case 0:
                    tb.Foreground = Brushes.Black;
                    break;
                case 1:
                    tb.Foreground = Brushes.White;
                    break;
                case 2:
                    tb.Foreground = Brushes.Red;
                    break;
                case 3:
                    tb.Foreground = Brushes.Blue;
                    break;
                case 4:
                    tb.Foreground = Brushes.Green;
                    break;
                default:
                    tb.Foreground = Brushes.Black;
                    break;
            }
        }

        private void cb3SelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cb3.SelectedIndex)
            {
                case 0:
                    tb.FontSize = 16;
                    break;
                case 1:
                    tb.FontSize = 20;
                    break;
                case 2:
                    tb.FontSize = 24;
                    break;
                case 3:
                    tb.FontSize = 28;
                    break;
                case 4:
                    tb.FontSize = 32;
                    break;
                default:
                    tb.FontSize = 16;
                    break;
            }
        }

        private void cb4SelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cb4.SelectedIndex)
            {
                case 0:
                    tb.FontFamily = new FontFamily("Segoe UI");
                    break;
                case 1:
                    tb.FontFamily = new FontFamily("Comfortaa");
                    break;
                case 2:
                    tb.FontFamily = new FontFamily("Corbel");
                    break;
                case 3:
                    tb.FontFamily = new FontFamily("Impact");
                    break;
                case 4:
                    tb.FontFamily = new FontFamily("Papyrus");
                    break;
                default:
                    tb.FontFamily = new FontFamily("Segoe UI");
                    break;
            }
        }

        private void tb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
