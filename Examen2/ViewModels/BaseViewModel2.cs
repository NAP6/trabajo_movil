using Examen2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Examen2.Services;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace Examen2.ViewModels
{
    public class BaseViewModel2:INotifyPropertyChanged
    {
        public IDataStore<ItemsListaInver> DataStore => DependencyService.Get<IDataStore<ItemsListaInver>>();
        public IDataStore<ambiente> DataStoreH => DependencyService.Get<IDataStore<ambiente>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            Debug.WriteLine(value);

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
