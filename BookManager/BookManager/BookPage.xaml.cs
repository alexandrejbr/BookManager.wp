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
    public partial class BookPage : PhoneApplicationPage
    {
        public const string UriPath = "/BookPage.xaml";
        //private int _si;
        public BookPage()
        {
            InitializeComponent();

            //this.BackKeyPress -= new EventHandler<System.ComponentModel.CancelEventArgs>(BookPage_BackKeyPress);
        }

        //void BookPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    string uripath = String.Format("{0}?si={1}", MainPage.UriPath, this._si);
        //    NavigationService.Navigate(new Uri(uripath, UriKind.Relative));
        //}


        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            //string value = NavigationContext.QueryString["edited"];
            //if(!String.IsNullOrEmpty(value))
            //{
            //    if(Convert.ToBoolean(value))
            //    {
            //        this._si = Convert.ToInt32(NavigationContext.QueryString["si"]);
            //        this.BackKeyPress += new EventHandler<System.ComponentModel.CancelEventArgs>(BookPage_BackKeyPress);
            //    }
            //}

            App app = (Application.Current as App);
            if (app != null)
            {
                this.BookTitle.Text = app.CurrentBookViewModel.Title.ToUpper();
                this.Value_Publisher.Text = app.CurrentBookViewModel.Publisher;
                this.Value_Author.Text = app.CurrentBookViewModel.Author;
                this.Value_ISBN.Text = app.CurrentBookViewModel.ISBN;
                this.Value_Comments.Text = app.CurrentBookViewModel.Comments;
            }
            base.OnNavigatedTo(e);
        }

        private void Edit(object sender, EventArgs e)
        {
            string uriPath = String.Format("{1}?isNew={0}", false.ToString(), BookEditPage.UriPath);
            NavigationService.Navigate(new Uri(uriPath, UriKind.Relative));
        }

        private void Remove(object sender, EventArgs e)
        {
            App app = (Application.Current as App);
            if (app != null)
            {
                BookViewModel bvm = app.CurrentBookViewModel;
                switch (bvm.EntryType)
                {
                    case MainViewModel.EntryType.WishList:
                        App.ViewModel.WishListBooks.Remove(bvm);
                        break;
                    case MainViewModel.EntryType.LentBooks:
                        App.ViewModel.LentBooks.Remove(bvm);
                        break;
                    case MainViewModel.EntryType.BorrowedBooks:
                        App.ViewModel.BorrowedBooks.Remove(bvm);
                        break;
                }
                //string uripath = String.Format("{0}?si={1}", MainPage.UriPath, (int)bvm.EntryType);
                //NavigationService.Navigate(new Uri(uripath, UriKind.Relative));
            }
            //else
            //    NavigationService.Navigate(new Uri(MainPage.UriPath, UriKind.Relative));

            this.NavigationService.GoBack();
        }
    }
}