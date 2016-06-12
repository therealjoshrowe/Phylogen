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

        public int TaxaCount { get; set; }

        public TaxaPage()
        {
            this.InitializeComponent();
            //need to load data from model in case data was input in some other way or the taxa page was navigated back to
            taxaCount = 0;//gotta watch setting this as it affects how ite,s are removed from the stack panel.
            foreach (String s in App.o.T.TaxLabels)
            {
                Grid g = new Grid { Name = "Grid" + taxaCount, HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(0, 5, 0, 5) };
                g.Height = 75;
                g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                TextBox t = new TextBox { Name = "TextBox" + taxaCount};
                t.Text = s;
                t.FontSize = 24;
                Button b = new Button { Name = "Button" + taxaCount, Content = "Remove", HorizontalAlignment = HorizontalAlignment.Stretch };
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
            }
            taxaNumberBlock.Text = "Taxa#:" + taxaCount;  
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            taxaStackPanel.Children.Clear();
            taxaCount = 0; //gotta watch setting this as it affects how ite,s are removed from the stack panel.
            foreach (String s in App.o.T.TaxLabels)
            {
                Grid g = new Grid { Name = "Grid" + taxaCount, HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(0, 5, 0, 5) };
                g.Height = 75;
                g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                TextBox t = new TextBox { Name = "TextBox" + taxaCount };
                t.Text = s;
                t.FontSize = 24;
                Button b = new Button { Name = "Button" + taxaCount, Content = "Remove", HorizontalAlignment = HorizontalAlignment.Stretch };
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
            }
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

        public void taxaPageButton_Click(object sender, RoutedEventArgs e)
        {
            App.o.T.Dimensions = 0;
            App.o.T.TaxLabels.Clear();
            foreach (Grid g in taxaStackPanel.Children)
            {
                TextBox t = g.Children.ElementAt(0) as TextBox;
                App.o.T.TaxLabels.Add(t.Text);
                App.o.T.Dimensions++;
            }
            this.Frame.Navigate(typeof(TaxaPage), null);
        }

        public void charactersPageButton_Click(object sender, RoutedEventArgs e)
        {
            App.o.T.Dimensions = 0;
            App.o.T.TaxLabels.Clear();
            foreach (Grid g in taxaStackPanel.Children)
            {
                TextBox t = g.Children.ElementAt(0) as TextBox;
                App.o.T.TaxLabels.Add(t.Text);
                App.o.T.Dimensions++;
            }
            this.Frame.Navigate(typeof(CharactersPage));
        }
    }
}
