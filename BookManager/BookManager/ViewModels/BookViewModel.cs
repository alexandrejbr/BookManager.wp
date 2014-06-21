using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Runtime.Serialization;

namespace BookManager
{
    [DataContract]
    public class BookViewModel : INotifyPropertyChanged
    {
        [DataMember]
        private string _title;
        [DataMember]
        private string _author;
        [DataMember]
        private string _publisher;
        [DataMember]
        private string _comments;
        [DataMember]
        private string _isbn;
        [DataMember]
        private MainViewModel.EntryType _entryType;

        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                if(value != this._title)
                {
                    this._title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        public string Author
        {
            get
            {
                return this._author;
            }
            set
            {
                if(value != this._author)
                {
                    this._author = value;
                    NotifyPropertyChanged("Author");
                }
            }
        }

        public string Publisher
        {
            get
            {
                return this._publisher;
            }
            set
            {
                if(value != this._publisher)
                {
                    this._publisher = value;
                    NotifyPropertyChanged("Publisher");
                }
            }
        }

        public string Comments
        {
            get
            {
                return this._comments;
            }
            set
            {
                if(value != this._comments)
                {
                    this._comments = value;
                    NotifyPropertyChanged("Comments");
                }
            }
        }

        public string ISBN
        {
            get
            {
                return this._isbn;
            }
            set
            {
                if(value != this._isbn)
                {
                    this._isbn = value;
                    NotifyPropertyChanged("ISBN");
                }
            }
        }

        public MainViewModel.EntryType EntryType
        {
            get
            {
                return this._entryType;
            }
            set
            {
                if(value != this._entryType)
                {
                    this._entryType = value;
                    NotifyPropertyChanged("EntryType");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}