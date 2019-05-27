﻿using System;
using System.ComponentModel;

namespace Quisco.Model
{
    public class InputText : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
            }
        }

        public bool isEnabled;

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
            }
        }

    }
}
