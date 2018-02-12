using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BookStore.Core.Interfaces;

namespace BookStore.Core.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IMainThreadDispatcherService _dispatcherService = GlobalDependencies.MainThreadDispatcherService;

        public bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

            storage = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var propertyChanded = PropertyChanged;

            if (propertyChanded != null)
                DispatchOnUIThread(() => propertyChanded.Invoke(this, new PropertyChangedEventArgs(propertyName)));
        }

        private void DispatchOnUIThread(Action action)
        {
            _dispatcherService.DispatchOnMainThread(action);
        }
    }
}
