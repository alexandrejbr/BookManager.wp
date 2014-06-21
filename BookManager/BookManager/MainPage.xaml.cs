using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace BookManager
{
    public partial class MainPage : PhoneApplicationPage
    {
        public const string UriPath = "/MainPage.xaml";
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            //this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        //private void MainPage_Loaded(object sender, RoutedEventArgs e)
        //{
        //    if (!App.ViewModel.IsDataLoaded)
        //    {
        //        App.ViewModel.LoadData();
        //    }
        //}

        private void StackPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;

            if (tb != null)
            {
                BookViewModel book = tb.DataContext as BookViewModel;

                ((App) Application.Current).CurrentBookViewModel = book;

                //string uriPath = String.Format("/BookPage.xaml?value={0}", tb.Text);
                //NavigationService.Navigate(new Uri(BookPage.UriPath, UriKind.Relative));
                //string uripath = String.Format("{0}?edited=False", BookPage.UriPath);
                NavigationService.Navigate(new Uri(BookPage.UriPath, UriKind.Relative));
            }
        }

        private void AddBook(object sender, EventArgs e)
        {
            string uriPath = String.Format("/{0}?isNew={1}&selectedEntrys={2}", 
                BookEditPage.UriPath, true.ToString(), this.EntrysPivot.SelectedIndex);
            NavigationService.Navigate(new Uri(uriPath, UriKind.Relative));
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri(AboutPage.UriPath, UriKind.Relative));
        }

        //protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        //{
        //    //string s = e.Uri.AbsolutePath;
        //        if(NavigationContext.QueryString.ContainsKey("si"))
        //            this.EntrysPivot.SelectedIndex = Convert.ToInt32(NavigationContext.QueryString["si"]);
        //    base.OnNavigatedTo(e);

        //}
    }
}