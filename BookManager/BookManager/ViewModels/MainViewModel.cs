using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Xml.Serialization;


namespace BookManager
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private const string WishListFile = "WhishList.dat";
        private const string LentBooksFile = "LentBooks.dat";
        private const string BorrowedBooksFile = "BorrowedBooks.dat";

        public enum EntryType
        {
            WishList = 0,
            LentBooks = 1,
            BorrowedBooks = 2
        }

        public static readonly string[] Entrys = { "Livro que quero", "Livro que emprestei", "Livro que me foi emprestado" };

        public MainViewModel()
        {
            this.WishListBooks = new ObservableCollection<BookViewModel>();
            this.BorrowedBooks = new ObservableCollection<BookViewModel>();
            this.LentBooks = new ObservableCollection<BookViewModel>();
        }

        /// <summary>
        /// A collection for BookViewModel objects.
        /// </summary>
        public ObservableCollection<BookViewModel> WishListBooks { get; private set; }
        public ObservableCollection<BookViewModel> BorrowedBooks { get; private set; }
        public ObservableCollection<BookViewModel> LentBooks { get; private set; }

        //public bool IsDataLoaded
        //{
        //    get;
        //    private set;
        //}

        /// <summary>
        /// Creates and adds a few BookViewModel objects into the books collections.
        /// </summary>
        //public void LoadData()
        //{
        //    // Sample data; replace with real data
        //    this.WishListBooks.Add(new BookViewModel()
        //    {
        //        Author = "Somerset Maugham",
        //        Title = "Servidão Humana",
        //        Publisher = "ASA",
        //        Comments = "",
        //        ISBN = "",
        //        EntryType = MainViewModel.EntryType.WishList
        //    });
        //    this.WishListBooks.Add(new BookViewModel()
        //    {
        //        Author = "Somerset Maugham",
        //        Title = "O Véu Pintado",
        //        Publisher = "ASA",
        //        Comments = "",
        //        ISBN = "",
        //        EntryType = MainViewModel.EntryType.WishList
        //    });
        //    this.WishListBooks.Add(new BookViewModel()
        //    {
        //        Author = "Somerset Maugham",
        //        Title = "O Fio da Navalha",
        //        Publisher = "ASA",
        //        Comments = "",
        //        ISBN = "",
        //        EntryType = MainViewModel.EntryType.WishList
        //    });
        //    this.WishListBooks.Add(new BookViewModel()
        //    {
        //        Author = "Winston Churchill",
        //        Title = "Memórias da II Guerra Mundial",
        //        Publisher = "Texto Editores",
        //        Comments = "",
        //        ISBN = "",
        //        EntryType = MainViewModel.EntryType.WishList
        //    });
        //    this.BorrowedBooks.Add(new BookViewModel()
        //    {
        //        Author = "Winston Churchill",
        //        Title = "Os meus primeiros anos",
        //        Publisher = "Guerra & Paz",
        //        Comments = "",
        //        ISBN = "",
        //        EntryType = MainViewModel.EntryType.BorrowedBooks
        //    });
        //    this.BorrowedBooks.Add(new BookViewModel()
        //    {
        //        Author = "John Steinbeck",
        //        Title = "Ratos e Homens",
        //        Publisher = "Livros do Brasil",
        //        Comments = "",
        //        ISBN = "",
        //        EntryType = MainViewModel.EntryType.BorrowedBooks
        //    });
        //    this.LentBooks.Add(new BookViewModel()
        //    {
        //        Author = "George Orwell",
        //        Title = "A Quinta dos Animais",
        //        Publisher = "Antígona",
        //        Comments = "",
        //        ISBN = "",
        //        EntryType = MainViewModel.EntryType.LentBooks
        //    });
        //    this.LentBooks.Add(new BookViewModel()
        //    {
        //        Author = "George Orwell",
        //        Title = "1984",
        //        Publisher = "Antígona",
        //        Comments = "",
        //        ISBN = "",
        //        EntryType = MainViewModel.EntryType.LentBooks
        //    });
        //    this.BorrowedBooks.Add(new BookViewModel()
        //    {
        //        Author = "George Orwell",
        //        Title = "Na Penúria em Paris e em Londres",
        //        Publisher = "Antígona",
        //        Comments = "",
        //        ISBN = "",
        //        EntryType = MainViewModel.EntryType.LentBooks
        //    });
        //    this.LentBooks.Add(new BookViewModel()
        //    {
        //        Author = "George Orwell",
        //        Title = "Homenagem à Catalunha",
        //        Publisher = "Antígona",
        //        Comments = "",
        //        ISBN = "",
        //        EntryType = MainViewModel.EntryType.LentBooks
        //    });
        //    this.WishListBooks.Add(new BookViewModel()
        //    {
        //        Author = "Máximo Gorki",
        //        Title = "A Mãe",
        //        Publisher = "Círculo de leitores",
        //        Comments = "",
        //        ISBN = "",
        //        EntryType = MainViewModel.EntryType.WishList
        //    });
        //    this.LentBooks.Add(new BookViewModel()
        //    {
        //        Author = "Milan Kundera",
        //        Title = "O Livro do Riso e do Esquecimento",
        //        Publisher = "ASA",
        //        Comments = "",
        //        ISBN = "",
        //        EntryType = MainViewModel.EntryType.WishList
        //    });
        //    this.BorrowedBooks.Add(new BookViewModel()
        //    {
        //        Author = "Os Maias",
        //        Title = "Eça de Queiroz",
        //        Publisher = "Livros do Brasil",
        //        Comments = "",
        //        ISBN = "",
        //        EntryType = MainViewModel.EntryType.LentBooks
        //    });

        //    this.IsDataLoaded = true;
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //public void LoadPersistedData()
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(List<BookViewModel>));
        //    //FileStream file = new FileStream(strPath, FileMode.OpenOrCreate);

        //    List<BookViewModel> saved = new List<BookViewModel>();

        //    using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
        //    {
        //        using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(WishListFile, 
        //            System.IO.FileMode.OpenOrCreate, isf))
        //        {
        //            if (stream.Length > 0)
        //            {
        //                saved = (List<BookViewModel>)serializer.Deserialize(stream);
        //            }
        //        }
        //    }

        //    foreach (BookViewModel s in saved)
        //    {
        //        this.WishListBooks.Add(s);
        //    }
        //}

        

        //public void PersistData()
        //{
        //    using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
        //    {
        //        MemoryStream ms = new MemoryStream();
        //        XmlSerializer xs = new XmlSerializer(typeof(List<BookViewModel>));

        //        List<BookViewModel> wishList = this.WishListBooks.ToList();

        //        xs.Serialize(ms, wishList);

        //        ms.Seek(0, SeekOrigin.Begin);
        //        using (ms)
        //        {
        //            IsolatedStorageFileStream file_stream = isf.CreateFile(WishListFile);
        //            ms.WriteTo(file_stream);
        //        }
        //    }
        //}

        private static void PersistData(List<BookViewModel> bvm, XmlSerializer xs, string file)
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                MemoryStream ms = new MemoryStream();

                xs.Serialize(ms, bvm);

                ms.Seek(0, SeekOrigin.Begin);
                using (IsolatedStorageFileStream fileStream = isf.CreateFile(file))
                {
                    using (ms)
                    {
                        ms.WriteTo(fileStream);
                    }
                }
            }    
        }

        public void PersistData()
        {
            //List<BookViewModel> wishList = this.WishListBooks.ToList();
            //List<BookViewModel> lentBooks = this.LentBooks.ToList();
            //List<BookViewModel> borro = this.BorrowedBooks.ToList();

            XmlSerializer xs = new XmlSerializer(typeof(List<BookViewModel>));

            PersistData(this.WishListBooks.ToList(), xs, MainViewModel.WishListFile);
            PersistData(this.LentBooks.ToList(), xs, MainViewModel.LentBooksFile);
            PersistData(this.BorrowedBooks.ToList(), xs, MainViewModel.BorrowedBooksFile);
        }

        public static List<BookViewModel> LoadPersistedData(XmlSerializer serializer, string file)
        {
            List<BookViewModel> saved = new List<BookViewModel>();

            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(file,
                    System.IO.FileMode.OpenOrCreate, isf))
                {
                    if (stream.Length > 0)
                    {
                        saved = (List<BookViewModel>)serializer.Deserialize(stream);
                    }
                }
            }
            return saved;
        }


        public void LoadPersistedData()
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<BookViewModel>));

            List<BookViewModel> wishList = LoadPersistedData(xs, MainViewModel.WishListFile);
            List<BookViewModel> lentBooks = LoadPersistedData(xs, MainViewModel.LentBooksFile);
            List<BookViewModel> borrowedBooks = LoadPersistedData(xs, MainViewModel.BorrowedBooksFile);

            foreach (var bookViewModel in borrowedBooks)
            {
                this.BorrowedBooks.Add(bookViewModel);
            }

            foreach (var bookViewModel in lentBooks)
            {
                this.LentBooks.Add(bookViewModel);    
            }

            foreach (var bookViewModel in wishList)
            {
                this.WishListBooks.Add(bookViewModel);
            }
        }
    }
}