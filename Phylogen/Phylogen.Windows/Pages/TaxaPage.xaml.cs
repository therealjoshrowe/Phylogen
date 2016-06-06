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

namespace Phylogen
{
    
    public sealed partial class TaxaPage : Page
    {
        private int taxaCount;
        private List<string> taxLabels;

        public int TaxaCount { get; set; }

        public TaxaPage()
        {
            this.InitializeComponent();
            taxaCount = 0;
            taxLabels = new List<string>();
            taxaNumberBlock.Text = "Taxa#:" + taxaCount;
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            string s = b.Name.Substring(6);
            taxaStackPanel.Children.Remove((UIElement)this.FindName("Grid" + s));
            taxaCount--;
            taxaNumberBlock.Text = "Taxa#:" + taxaCount;
        }

        private void addTaxonButton_Click(object sender, RoutedEventArgs e)
        {
            Grid g = new Grid { Name = "Grid" + taxaCount, HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(0, 5, 0, 5)};
            g.Height = 75;
            g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)});
            g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)});
            TextBox t = new TextBox { Name = "TextBox" + taxaCount};
            t.FontSize = 24;
            Button b = new Button { Name = "Button" + taxaCount, Content = "Remove", HorizontalAlignment = HorizontalAlignment.Stretch};
            b.Width = g.Width / 2; //This needs to be resolved to calculate or fill automatically.
            b.Height = 75;
            b.FontSize = 24;
            b.Click += new RoutedEventHandler(removeButton_Click);
            Grid.SetColumn(t, 0);
            Grid.SetColumn(b, 1);
            g.Children.Add(t);
            g.Children.Add(b);
            taxaStackPanel.Children.Add(g);
            taxaCount++;
            taxaNumberBlock.Text = "Taxa#:" + taxaCount; 
        }
    }
}
