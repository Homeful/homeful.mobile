using System;
using Firebase.Database;
using System.ComponentModel;

namespace Homeful.mobile
{
    public class Stop : INotifyPropertyChanged
    {
        public Camp Camp { get; set; }
        public bool Complete { get; set; }
        public DateTime? CompletedAt { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
