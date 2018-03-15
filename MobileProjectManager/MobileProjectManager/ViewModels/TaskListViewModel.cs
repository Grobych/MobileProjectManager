using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace MobileProjectManager.ViewModels
{
    class TaskListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TaskViewModel> TaskList = new ObservableCollection<TaskViewModel>();
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
