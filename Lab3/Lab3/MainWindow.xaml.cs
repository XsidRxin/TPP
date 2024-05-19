using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /*
        private void FirstButton_Click(object sender, RoutedEventArgs e)
        {
            // Устанавливаем ширину первого столбца в 2
            

            // Скрываем вторую кнопку
            secondColumn.Width = new GridLength(0);
            SecondButton.Visibility = Visibility.Collapsed;
        }
        */
        ObservableCollection<string> list_log = new ObservableCollection<string>();
        private void OffBtn(object sender, RoutedEventArgs e)
        {
            ColumnToHide.Width = new GridLength(0);
            
        }
        private void OnBtn(object sender, RoutedEventArgs e)
        {
            ColumnToHide.Width = new GridLength(1, GridUnitType.Star);
        }

        private void LogClearBtn_click(object sender, RoutedEventArgs e)
        {
            list_log.Clear();
            
        }

        string numbers = "1234567890";
        string actions = "+-*/";

        int brackets;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                string cur_val = TextBox.Text;
                string val = button.Content.ToString();

                if (numbers.Contains(val))
                {
                    if (cur_val.Length > 0)
                    {
                        if (')' == cur_val[cur_val.Length - 1])
                        {
                            TextBox.Text += "*";
                        }
                    }

                    TextBox.Text += val;
                }
                else if (actions.Contains(val))
                {
                    if (cur_val.Length > 0)
                    {
                        if (actions.Contains(TextBox.Text[TextBox.Text.Length - 1]))
                        {
                            TextBox.Text = cur_val.Substring(0, cur_val.Length - 1) + val;
                        }
                        else
                        {
                            TextBox.Text += val;
                        }
                    }
                    
                }
                else
                {
                    switch (val)
                    {
                        case "C":
                            TextBox.Text = "";
                            brackets = 0;
                            break;
                        case "BackSpace":
                            if (TextBox.Text != "")
                            {
                                switch(TextBox.Text[TextBox.Text.Length - 1])
                                {
                                    case ')':
                                        brackets++;
                                        break;
                                    case '(':
                                        brackets--;
                                        break;
                                }
                                TextBox.Text = cur_val.Substring(0, cur_val.Length - 1);
                                
                            }

                            break;
                        case "(":
                            if (cur_val.Length > 0 && numbers.Contains(cur_val[cur_val.Length - 1]))
                            {
                                TextBox.Text += "*";
                            }

                            TextBox.Text += val;
                            brackets++;
                            break;
                        case ")":
                            if (brackets > 0)
                            {
                                TextBox.Text += val;
                                brackets--;
                            }
                            break;
                        case "=":
                            if (brackets != 0)
                            {
                                MessageBox.Show("Закройте скобки");
                                break;
                            }
                            while (cur_val.Contains("()"))
                            {
                                cur_val = cur_val.Replace("()", "");
                            }
                            if (cur_val.Length > 0)
                            {
                                if ("+-*/".Contains(cur_val[cur_val.Length - 1]))
                                {
                                    cur_val = cur_val.Substring(0, cur_val.Length - 1);
                                }
                                DataTable table = new DataTable();
                                string expression = cur_val;
                                DataRow row = table.NewRow();
                                table.Rows.Add(row);
                                table.Columns.Add("Expression", typeof(float), expression);
                                float result = (float)row["Expression"];
                                TextBox.Text = result.ToString();

                                if (CheckRecording.IsChecked == true)
                                {
                                    list_log.Add(cur_val + "=" + result.ToString());
                                    LogList.ItemsSource = list_log;
                                    LogList.Items.Refresh();
                                }
                                


                                break;
                            }
                            break;
                        case ".":

                            //сделать проверку с итерацией вниз и если первый жлемент не цифра - точка, то ставить нельзя
                            TextBox.Text += val;
                            break;
                    }
                    
                }


            }
        }
       
    }
}
