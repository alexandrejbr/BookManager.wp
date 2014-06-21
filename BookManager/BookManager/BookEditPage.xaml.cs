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
    public partial class BookEditPage : PhoneApplicationPage
    {
        public const string UriPath = "/BookEditPage.xaml";
        public BookEditPage()
        {
            InitializeComponent();
            this.LP_EntryType.ItemsSource = MainViewModel.Entrys;
        }

        private void SaveChanges(object sender, EventArgs e)
        {
            string value = NavigationContext.QueryString["isNew"];
            bool isNew = /*String.IsNullOrEmpty(value) &&*/ Convert.ToBoolean(value);
            if (!isNew)
            {
                App app = (Application.Current as App);
                if (app != null)
                {
                    app.CurrentBookViewModel.Title = this.Value_Title.Text;
                    app.CurrentBookViewModel.Publisher = this.Value_Publisher.Text;
                    app.CurrentBookViewModel.Author = this.Value_Author.Text;
                    app.CurrentBookViewModel.ISBN = this.Value_ISBN.Text;
                    app.CurrentBookViewModel.Comments = this.Value_Comments.Text;
                }
                //string uripath = String.Format("{0}?edited=True&si={1}", BookPage.UriPath, this.LP_EntryType.SelectedIndex);
                //NavigationService.Navigate(new Uri(uripath, UriKind.Relative));
                this.NavigationService.GoBack();
            }
            else
            {
                if (String.IsNullOrEmpty(this.Value_Title.Text))
                {
                    if (String.IsNullOrEmpty(this.Value_Publisher.Text) && String.IsNullOrEmpty(this.Value_Author.Text)
                        && String.IsNullOrEmpty(this.Value_ISBN.Text) && String.IsNullOrEmpty(this.Value_Comments.Text)
                        )
                        MessageBox.Show("Não pode inserir um livro sem dados!");
                    else
                        MessageBox.Show("É obrigatório inserir o nome do livro.");
                }
                else
                {
                    BookViewModel bvm = new BookViewModel
                                            {
                                                Title = this.Value_Title.Text,
                                                Publisher = this.Value_Publisher.Text,
                                                Author = this.Value_Author.Text,
                                                ISBN = this.Value_ISBN.Text,
                                                Comments = this.Value_Comments.Text
                                            };
                    switch (this.LP_EntryType.SelectedIndex)
                    {
                        case (int) MainViewModel.EntryType.WishList:
                            bvm.EntryType = MainViewModel.EntryType.WishList;
                            App.ViewModel.WishListBooks.Add(bvm);
                            break;
                        case (int) MainViewModel.EntryType.LentBooks:
                            bvm.EntryType = MainViewModel.EntryType.LentBooks;
                            App.ViewModel.LentBooks.Add(bvm);
                            break;
                        case (int) MainViewModel.EntryType.BorrowedBooks:
                            bvm.EntryType = MainViewModel.EntryType.BorrowedBooks;
                            App.ViewModel.BorrowedBooks.Add(bvm);
                            break;
                    }
                    //App.ViewModel.WishListBooks.Add(bvm);
                    //NavigationService.Navigate(new Uri(MainPage.UriPath, UriKind.Relative));
                    this.NavigationService.GoBack();
                }
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string value = NavigationContext.QueryString["isNew"];
            bool isNew = /*String.IsNullOrEmpty(value) &&*/ Convert.ToBoolean(value);
            if (!isNew)
            {
                this.LP_EntryType.IsEnabled = false;
                App app = (Application.Current as App);
                if (app != null)
                {
                    this.Value_Title.Text = app.CurrentBookViewModel.Title;
                    this.Value_Publisher.Text = app.CurrentBookViewModel.Publisher;
                    this.Value_Author.Text = app.CurrentBookViewModel.Author;
                    this.Value_ISBN.Text = app.CurrentBookViewModel.ISBN;
                    this.Value_Comments.Text = app.CurrentBookViewModel.Comments;
                    this.LP_EntryType.SelectedIndex = (int) app.CurrentBookViewModel.EntryType;
                }
            }
            else
            {
                this.LP_EntryType.IsEnabled = true;
                value = NavigationContext.QueryString["selectedEntrys"];
                if (!String.IsNullOrEmpty(value))
                    this.LP_EntryType.SelectedIndex = Convert.ToInt32(value);

                this.Value_Title.Text = String.Empty;
                this.Value_Publisher.Text = String.Empty;
                this.Value_Author.Text = String.Empty;
                this.Value_ISBN.Text = String.Empty;
                this.Value_Comments.Text = String.Empty;
            }

            base.OnNavigatedTo(e);
        }
    }
}