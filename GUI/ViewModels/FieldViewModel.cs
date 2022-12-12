﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using TheSphinx.Core.Models;

namespace GUI.ViewModels
{
    internal class FieldViewModel : INotifyPropertyChanged
    {
        public Field Field { get; private set; }

        private string key;
        public string Key
        {
            get => key;
            set
            {
                key = value;
                OnPropertyChanged("Key");
            }
        }

        public string Value
        {
            get => Field.Value;
            set
            {
                Field.Value = value;
                OnPropertyChanged("Value");
            }
        }

        public bool Encrypted
        {
            get => Field.Encrypted;
            set
            {
                Field.Encrypted = value;
                OnPropertyChanged("Encrypted");
                OnPropertyChanged("IsEncrypted");
                OnPropertyChanged("IsOpen");
                OnPropertyChanged("IsClosed");
            }
        }

        private bool isShowed;
        public bool IsShowed
        {
            get => isShowed;
            set
            {
                isShowed = value;
                OnPropertyChanged("IsShowed");
                OnPropertyChanged("IsOpen");
                OnPropertyChanged("IsClosed");
            }
        }

        public Visibility IsEncrypted => Encrypted ? Visibility.Visible : Visibility.Hidden;
        public Visibility IsOpen => !Encrypted || IsShowed ? Visibility.Visible : Visibility.Hidden;
        public Visibility IsClosed => Encrypted && !IsShowed ? Visibility.Visible : Visibility.Hidden;

        internal FieldViewModel(Field field, string key)
        {
            Field = field;
            this.key = key;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
