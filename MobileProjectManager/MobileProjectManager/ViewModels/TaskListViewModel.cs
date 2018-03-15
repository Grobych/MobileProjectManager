using MobileProjectManager.Views.TaskViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileProjectManager.ViewModels
{
    public class TaskListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TaskViewModel> TaskList { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ToCreateTaskPageCommand { protected set; get; }
        public ICommand DeleteTask { protected set; get; }

        TaskViewModel selectedTask;
        public TaskViewModel currentTask { get; set; }

        public TaskListViewModel()
        {
            TaskList = new ObservableCollection<TaskViewModel>();
            ToCreateTaskPageCommand = new Command(ToCreateTaskPage);
        }

        private void ToCreateTaskPage()
        {
            NavigationUtil.Navigation.PushAsync(new CreateTaskPage(new TaskViewModel(this)));
        }
        public void AddTask(TaskViewModel task)
        {
            TaskList.Add(task);
        }

        public TaskViewModel SelectedTask
        {
            get { return selectedTask; }
            set
            {
                if (selectedTask != value)
                {
                    selectedTask = null;
                    OnPropertyChanged("SelectedTask");
                    currentTask = value;
                    NavigationUtil.Navigation.PushAsync(new TaskPage(currentTask));
                }
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
