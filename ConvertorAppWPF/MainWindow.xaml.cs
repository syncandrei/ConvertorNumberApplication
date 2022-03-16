using ConvertorAppWPF.API;
using ConvertorAppWPF.DTO;
using Newtonsoft.Json;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ConvertorAppWPF
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

        private void Button_Convert(object sender, RoutedEventArgs e)
        {
            this.GetNumberToWords();
        }

        private void MainTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // The allowed characters that can be input by the user
            string result = "";
            char[] validChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '.', ' ' };
            foreach (char c in MainTextBox.Text)
            {
                if (Array.IndexOf(validChars, c) != -1)
                    result += c;
            }

            MainTextBox.Text = result;
        }

        private void GetNumberToWords()
        {
            if (MainTextBox.Text != "")
            {
                var textInput = FilterUserInput(MainTextBox.Text);
                var response = WebApi.GetConvertNumberCall("convertor?number=" + textInput);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = response.Result.Content.ReadAsStringAsync().Result;
                    var wordNumber = JsonConvert.DeserializeObject<ConvertNumberDTO>(result);

                    WordText.Text = wordNumber.WordNumber;
                }
                else
                {
                    WordText.Text = "Something went wrong";
                }
            }
            else
            {
                WordText.Text = "Input a number";
            }         
        }

        private string FilterUserInput(string textInput)
        {
            var filteredInput = textInput;
            if (textInput.Contains(" "))
            {
                filteredInput = textInput.Replace(" ", "");
            }

            if (textInput.Contains("."))
            {
                filteredInput = textInput.Replace('.', ',');
            }

            if (String.IsNullOrEmpty(filteredInput.Substring(filteredInput.IndexOf(",") + 1)))
            {
                filteredInput = String.Format("{0}{1}", filteredInput, "0");
            }

            return filteredInput;
        }
    }
}
