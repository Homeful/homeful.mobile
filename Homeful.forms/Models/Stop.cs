using System;
using Firebase.Database;
using System.ComponentModel;

namespace Homeful.mobile
{
    public class Stop : INotifyPropertyChanged, IFirebaseItem
    {
        public string Id { get; set; }
        public Camp Camp { get; set; }
        public bool Complete { get; set; }
        public bool InProgress { get; set; }
        public bool Open
        {
            get
            {
                return !Complete && !InProgress;
            }
        }
        public DateTime? CompletedAt { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
