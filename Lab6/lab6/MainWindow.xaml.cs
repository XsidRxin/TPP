using Microsoft.Win32;
using System;
using System.IO;
using System.IO.Packaging;
using System.Windows;
using TagLib;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Numerics;

namespace lab6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            slider.ValueChanged += Slider_ValueChanged;

            
        }
        List<string> selectedFiles = new List<string>();
        private MediaPlayer mediaplayer = new MediaPlayer();
        bool is_playing = false;
        int now_playing;

        
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void Hide_btn(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Close_btn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ImportAudio(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Audio Files|*.mp3;*.wav|All Files|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                selectedFiles.AddRange(openFileDialog.FileNames);
                string[] names = new string[selectedFiles.Count];
                for (int i = 0; i < selectedFiles.Count; i++)
                {
                    names[i] = Path.GetFileNameWithoutExtension(selectedFiles[i]);
                }
                for (int i = 0;i < names.Length; i++)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Height = 30;
                    item.VerticalAlignment = VerticalAlignment.Center;
                    TextBlock nameTB = new TextBlock();
                    nameTB.Text = names[i];
                    nameTB.FontSize = 14;
                    nameTB.FontWeight = FontWeights.DemiBold;
                    nameTB.Margin = new Thickness(0,10,0,0);
                    nameTB.VerticalAlignment = VerticalAlignment.Center;
                    item.Content = nameTB;
                    TrackList.Items.Add(item);
                }

            }
        }

        private void ChangeTrack(object sender, RoutedEventArgs e)
        {
            if (is_playing)
            {
                mediaplayer.Pause();
                mediaplayer.Close();
                is_playing = false;
            }
            int selectedIndex = TrackList.SelectedIndex;
            mediaplayer = new MediaPlayer();
            mediaplayer.Open(new Uri(selectedFiles[selectedIndex]));
            now_playing = selectedIndex;
            mediaplayer.MediaOpened += (sender, e) =>
            {
                // Получить продолжительность трека
                TimeSpan duration = mediaplayer.NaturalDuration.TimeSpan;
                mediaplayer.MediaOpened += (sender, e) =>
                {
                    // Получить продолжительность трека
                    slider.Maximum = mediaplayer.NaturalDuration.TimeSpan.TotalSeconds;

                };

            };
            mediaplayer.Play();
            is_playing = true;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += (sender, e) =>
            {
                slider.Value = mediaplayer.Position.TotalSeconds;
            };
            timer.Start();
            


        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Устанавливаем позицию медиаплеера в соответствии со значением Slider
            mediaplayer.Position = TimeSpan.FromSeconds(slider.Value);
        }
        private void PlayPause(object sender, RoutedEventArgs e)
        {
            if(is_playing)
            {
                mediaplayer.Pause();
            }
            else { mediaplayer.Play(); }
            is_playing=!is_playing;
        }
        private void Next(object sender, RoutedEventArgs e)
        {
            if (selectedFiles.Count > now_playing+1) {
                mediaplayer.Stop();
                mediaplayer.Close();
                mediaplayer.Open(new Uri(selectedFiles[now_playing + 1]));
                mediaplayer.Play();
                is_playing = true;
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(100);
                timer.Tick += (sender, e) =>
                {
                    slider.Value = mediaplayer.Position.TotalSeconds;
                };
                timer.Start();
                now_playing += 1;
            }
            
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            if (now_playing>0)
            {
                mediaplayer.Stop();
                mediaplayer.Close();
                mediaplayer.Open(new Uri(selectedFiles[now_playing - 1]));
                mediaplayer.Play();
                is_playing = true;
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(100);
                timer.Tick += (sender, e) =>
                {
                    slider.Value = mediaplayer.Position.TotalSeconds;
                };
                timer.Start();
                now_playing -= 1;  
            }
        }

    }
}
