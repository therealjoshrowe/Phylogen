using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Phylogen
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MatrixPage : Page
    {
        private int taxaCount;
        public MatrixPage()
        {
            taxaCount = 0;
            matrixStackPanel = new StackPanel();
            loadPageDataFromModel();
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            loadPageDataFromModel();
        }

        public void taxaPageButton_Click(object sender, RoutedEventArgs e)
        {
            savePageDataToModel();
            this.Frame.Navigate(typeof(TaxaPage), null);
        }

        public void charactersPageButton_Click(object sender, RoutedEventArgs e)
        {
            savePageDataToModel();
            this.Frame.Navigate(typeof(CharactersPage));
        }

        private void matrixPageButton_Click(object sender, object e)
        {
            savePageDataToModel();
            this.Frame.Navigate(typeof(MatrixPage));
        }

        private void savePageDataToModel()
        {
        }

        private void loadPageDataFromModel()
        {
            taxaCount = 0;
            foreach(String s in App.o.T.TaxLabels)
            {
                Grid g = new Grid { Name = "Grid" + taxaCount, HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(0, 5, 0, 5) };
                g.Height = 75;
                g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                TextBlock t = new TextBlock { Name = "TextBlock" + taxaCount, Text = s};
                t.FontSize = 24;
                TextBox b = new TextBox{ Name = "TextBox" + taxaCount, Text = "", HorizontalAlignment = HorizontalAlignment.Stretch };
                b.Width = g.Width / 2; //This needs to be resolved to calculate or fill automatically.
                b.Height = 75;
                b.FontSize = 24;
                b.TextChanged += new TextChangedEventHandler(matrixTextBox_TextChanged);
                Grid.SetColumn(t, 0);
                Grid.SetColumn(b, 1);
                g.Children.Add(t);
                g.Children.Add(b);
                matrixStackPanel.Children.Add(g);
                taxaCount++;
            }
        }

        private void matrixTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            TextBox b = sender as TextBox;
            char[] dnaLetters = { 'a', 'c', 'g', 't', 'r', 'y', 'm', 'k', 's','w', 'h','b', 'v', 'd', 'n', 'x'};
            char[] rnaLetters = { 'a', 'c', 'g', 'u', 'r', 'y', 'm', 'k', 's', 'w', 'h', 'b', 'v', 'd', 'n', 'x' };
            char[] proteinLetters = { 'a', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'y', '*', 'b', 'z' };
            string s = App.o.C.DataType;
            if (b.Text.Length == 0)
            {
                SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.White);
                b.Background = brush;
            }
            else
            {
                if (s.Equals("STANDARD"))
                {
                    //figure out a way to validate this.
                }
                else if (s.Equals("DNA"))
                {
                    foreach (char c in b.Text.ToLower())
                    {
                        if (Array.IndexOf(dnaLetters, c) < 0)
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
                else if (s.Equals("RNA"))
                {
                    foreach (char c in b.Text.ToLower())
                    {
                        if (Array.IndexOf(rnaLetters, c) < 0)
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
                else if (s.Equals("PROTEIN"))
                {
                    foreach (char c in b.Text.ToLower())
                    {
                        if (Array.IndexOf(proteinLetters, c) < 0)
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
                    throw new NotImplementedException();
                }
            }
        }
    }
}
