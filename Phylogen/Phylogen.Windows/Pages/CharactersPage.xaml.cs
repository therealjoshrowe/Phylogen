using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;



namespace Phylogen
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CharactersPage : Page
    {
        private int equateSymbolCount;
        public CharactersPage()
        {
            this.InitializeComponent();
            equateSymbolCount = 0;
        }

        public void taxaPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataValidated())
            {
                saveCharactersDataToModel();
                this.Frame.Navigate(typeof(TaxaPage));
            }
            else
            {
                showErrorDisloag();
            }
        }

        public void matrixPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataValidated())
            {
                saveCharactersDataToModel();
                this.Frame.Navigate(typeof(MatrixPage));
            }
            else
            {
                showErrorDisloag();
            }
        }

        public void charactersPageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void gapTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //validate the text entered.
            if (gapTextBox.Text.Length > 1)
            {

                SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.Red);
                gapTextBox.Background = brush;
            }
            else {
                SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.Black);
                gapTextBox.Background = brush;
            }
        }

        private void missingTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //validate the text entered.
            if (missingTextBox.Text.Length > 1)
            {

                SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.Red);
                missingTextBox.Background = brush;
            }
            else
            {
                SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.Black);
                missingTextBox.Background = brush;
            }
        }

        private void nCharTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!isNumericString(nCharTextBox.Text))
            {
                SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.Red);
                nCharTextBox.Background = brush;
            }
            else
            {
                SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.Black);
                nCharTextBox.Background = brush;
            }
        }

        public void interleaveCountTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!isNumericString(interleaveCountTextBox.Text))
            {
                SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.Red);
                interleaveCountTextBox.Background = brush;
            }
            else
            {
                SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.Black);
                interleaveCountTextBox.Background = brush;
            }
        }

        public void equateSymbolTextBox_TextChanged(object sender, TextChangedEventArgs e) //<<-- this needs to be decided / verified by input given by Mr. Taylor (what is a symbol, what characters make up an equate)
        {
            TextBox b = sender as TextBox;
            string s = b.Text;
            
            if (s != null)
            {
                if (s.Length == 1)
                {
                    if (s.Length== 0)
                    {
                        SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.White);
                        b.Background = brush;
                    }
                    else if (!Char.IsLetter(s[0]))
                    {
                        SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.Red);
                        b.Background = brush;
                    }
                    else
                    {
                        SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.White);
                        b.Background = brush;
                    }
                }
                else if (s.Length == 2)
                {
                    if (!Char.IsLetter(s[0]) || s[1] != '=')
                    {
                        SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.Red);
                        b.Background = brush;
                    }
                    else
                    {
                        SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.White);
                        b.Background = brush;
                    }
                }
                else if (s.Length > 2)
                {
                    if (!Char.IsLetter(s[0]) || s[1] != '=' || !isNumericString(s.Substring(2)))
                    {
                        SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.Red);
                        b.Background = brush;
                    }
                    else
                    {
                        SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.White);
                        b.Background = brush;
                    }
                }
            }
            else
            {
                throw new NullReferenceException("The text should not be null, something is wrong!");
            }
        }

        private bool isNumericString(string s)
        {
            bool isNumeric = true;
            foreach (char c in s)
            {
                if (!Char.IsDigit(c))
                {
                    isNumeric = false;
                    break;
                }
            }
            return isNumeric;
        }

        private void addEquateSymbolButton_Click(object sender, RoutedEventArgs e)
        {
            Grid g = new Grid { Name = "Grid" + equateSymbolCount, HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(0, 5, 0, 5) };
            g.Height = 50;
            g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            TextBox t = new TextBox { Name = "TextBox" + equateSymbolCount };
            t.FontSize = 16;
            t.TextChanged += new TextChangedEventHandler(equateSymbolTextBox_TextChanged);
            Button b = new Button { Name = "Button" + equateSymbolCount, Content = "Remove", HorizontalAlignment = HorizontalAlignment.Stretch };
            b.Width = g.Width / 2; //This needs to be resolved to calculate or fill automatically.
            b.Height = 50;
            b.FontSize = 18;
            b.Click += new RoutedEventHandler(removeEquateButton_Click);
            Grid.SetColumn(t, 0);
            Grid.SetColumn(b, 1);
            g.Children.Add(t);
            g.Children.Add(b);
            equateSymbolStack.Children.Add(g);
            equateSymbolCount++;
        }

        public void removeEquateButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            string s = b.Name.Substring(6);
            equateSymbolStack.Children.Remove((UIElement)this.FindName("Grid" + s));
            equateSymbolCount--;
        }

        private bool dataValidated()
        {
            bool nCharTextBoxValidated = true;
            bool comboBoxValidated = true;
            bool interleaveCountTextBoxValidated = true;

            if (nCharTextBox.Text.Length == 0 || nCharTextBox.Background.Equals(new SolidColorBrush(Windows.UI.Colors.Red)))
            {
                nCharTextBoxValidated = false;
            }

            if (comboBox.SelectedItem == null)
            {
                comboBoxValidated = false;
            }

            if (toggleSwitch_Copy.IsOn)
            {
                if (interleaveCountTextBox.Text.Length == 0 || interleaveCountTextBox.Background.Equals(new SolidColorBrush(Windows.UI.Colors.Red)))
                {
                    interleaveCountTextBoxValidated = false;
                }
            }

            if (nCharTextBoxValidated && comboBoxValidated && interleaveCountTextBoxValidated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void showErrorDisloag()
        {
            bool nCharTextBoxValidated = true;
            bool comboBoxValidated = true;
            bool interleaveCountTextBoxValidated = true;
            string s = "";

            if (nCharTextBox.Text.Length == 0 || nCharTextBox.Background.Equals(new SolidColorBrush(Windows.UI.Colors.Red)))
            {
                nCharTextBoxValidated = false;
                s += "\n You need to correctly specify the number of characters per taxa for the matrix";
            }

            if (comboBox.SelectedItem == null)
            {
                comboBoxValidated = false;
                s += "\n You need to specify a datatype that your matrix will be using";
            }

            if (toggleSwitch_Copy.IsOn)
            {
                if (interleaveCountTextBox.Text.Length == 0 || interleaveCountTextBox.Background.Equals(new SolidColorBrush(Windows.UI.Colors.Red)))
                {
                    interleaveCountTextBoxValidated = false;
                    s += "\n You need to specify the length of interleave rows for the matrix";
                }
            }

            if (!(nCharTextBoxValidated && comboBoxValidated && interleaveCountTextBoxValidated))
            {
                var messageDialog = new MessageDialog(s);
                messageDialog.Title = "Input Error:";
                await messageDialog.ShowAsync();
            }
        }

        private void toggleSwitch_Copy_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch t = sender as ToggleSwitch;
            if (t.IsOn)
            {
                interleaveCountTextBox.Visibility = Visibility.Visible;
            }
            else
            {
                interleaveCountTextBox.Visibility = Visibility.Collapsed;
                interleaveCountTextBox.Text = "";
            }
        }

        private void saveCharactersDataToModel()
        {
            App.o.C.Equates.Clear();
            App.o.C.Dimensions = Int32.Parse(nCharTextBox.Text); // need to be wearly of overflow
            TextBlock t = comboBox.SelectedItem as TextBlock;
            App.o.C.DataType = t.Text;

            if (toggleSwitch.IsOn)
            {
                App.o.C.RespectCase = true;
            }
            else
            {
                App.o.C.RespectCase = false;
            }

            if (toggleSwitch_Copy.IsOn)
            {
                App.o.C.Interleave = true;
                App.o.C.InterleaveLength = Int32.Parse(interleaveCountTextBox.Text); //overflow aware here too.
            }
            else
            {
                App.o.C.Interleave = false;
                App.o.C.InterleaveLength = 0;
            }

            if (gapTextBox.Text.Length > 0)
            {
                App.o.C.Gap = gapTextBox.Text[0];
            }

            if (missingTextBox.Text.Length > 0)
            {
                App.o.C.Missing = missingTextBox.Text[0];
            }

            foreach(Grid g in equateSymbolStack.Children)
            {
                TextBox b = g.Children[0] as TextBox;
                App.o.C.Equates.Add(b.Text);
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            loadCharactersDataFromModel();
        }
        private void loadCharactersDataFromModel()
        {
            nCharTextBox.Text = App.o.C.Dimensions.ToString();

            toggleSwitch.IsOn = App.o.C.RespectCase;
            toggleSwitch_Copy.IsOn = App.o.C.Interleave;
            interleaveCountTextBox.Text = App.o.C.InterleaveLength.ToString();
            equateSymbolCount = 0;

            foreach (string s in App.o.C.Equates)
            {
                Grid g = new Grid { Name = "Grid" + equateSymbolCount, HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(0, 5, 0, 5) };
                g.Height = 50;
                g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                TextBox t = new TextBox { Name = "TextBox" + equateSymbolCount };
                t.Text = s;
                t.FontSize = 16;
                t.TextChanged += new TextChangedEventHandler(equateSymbolTextBox_TextChanged);
                Button b = new Button { Name = "Button" + equateSymbolCount, Content = "Remove", HorizontalAlignment = HorizontalAlignment.Stretch };
                b.Width = g.Width / 2; //This needs to be resolved to calculate or fill automatically.
                b.Height = 50;
                b.FontSize = 18;
                b.Click += new RoutedEventHandler(removeEquateButton_Click);
                Grid.SetColumn(t, 0);
                Grid.SetColumn(b, 1);
                g.Children.Add(t);
                g.Children.Add(b);
                equateSymbolStack.Children.Add(g);
                equateSymbolCount++;
            }

            string str = App.o.C.DataType;

            if (str.Equals("STANDARD"))
            {
                comboBox.SelectedIndex = 0;
            }
            else if (str.Equals("DNA")) {
                comboBox.SelectedIndex = 1;
            }
            else if (str.Equals("RNA"))
            {
                comboBox.SelectedIndex = 2;
            }
            else if (str.Equals("NUCLEOTIDE"))
            {
                comboBox.SelectedIndex = 3;
            }
            else if (str.Equals("PROTEIN"))
            {
                comboBox.SelectedIndex = 4;
            }
            else if (str.Equals("CONTINUOUS"))
            {
                comboBox.SelectedIndex = 5;
            }
            else
            {
                comboBox.SelectedIndex = -1;
            }

            gapTextBox.Text = App.o.C.Gap.ToString();
            missingTextBox.Text = App.o.C.Missing.ToString();
        }
    }
}
