using MobileProjectManager.Views.TaskViews;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileProjectManager.ViewModels
{
    public class TaskListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TaskViewModel> TaskList { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ToCreateTaskPageCommand { protected set; get; }

        public ProjectViewModel pvm { get; set; }

        TaskViewModel selectedTask;
        public TaskViewModel currentTask { get; set; }

        public TaskListViewModel(List<Models.Task> Tasks)
        {
            TaskList = new ObservableCollection<TaskViewModel>();
            foreach (var item in Tasks)
            { 
                TaskList.Add(new TaskViewModel(item));
            }
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
        public void DeleteTask(TaskViewModel task)
        {
            TaskList.Remove(task);
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
                    currentTask.tlv = this;
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
