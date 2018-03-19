using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using MobileProjectManager.Models;
using MobileProjectManager.ViewModels.Utils;
using MongoDB.Bson;
using Xamarin.Forms;

namespace MobileProjectManager.ViewModels
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        public Task Task { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public TaskListViewModel tlv;
        public Project TaskProject { get; set; }

        public User Implementor { get; set; }

        public ICommand SaveTaskCommand { protected set; get; }
        public ICommand GetTaskCommand { protected set; get; }

        private void CommandInit()
        {
            SaveTaskCommand = new Command(SaveTask);
            GetTaskCommand = new Command(GetTaskByMyself);
        }
        public TaskViewModel(TaskListViewModel viewModel)
        {
            tlv = viewModel;
            TaskProject = tlv.pvm.Project;
            Task = new Task
            {
                StartTime = DateTime.Now,
                Deadline = DateTime.Now
            };
            Implementor = Database.Database.GetUser(Task.Implementer);
            CommandInit();
        }
        public TaskViewModel(Models.Task task)
        {
            this.Task = task;
            TaskProject = Database.Database.GetProject(Task.ProjectID);
            Implementor = Database.Database.GetUser(Task.Implementer);
            CommandInit();
        }


        private void GetTaskByMyself()
        {
            Debug.WriteLine("GetTask");
            Task.Implementer = Auth.CurrentUser.ID;
            this.Implementor = Auth.CurrentUser;
            Database.Database.UpdateTask(Task);
        }

        private void SaveTask()
        {
            Task temp = this.Task;
            TaskProject = tlv.pvm.Project;
            temp.ProjectID = TaskProject.ID;
            Database.Database.AddTaskToProject(TaskProject,ref temp);
            tlv.AddTask(this);
            NavigationUtil.Navigation.PopAsync();
        }

        public ObjectId ID
        {
            get { return Task.ID; }
            set
            {
                if (Task.ID != value)
                {
                    Task.ID = value;
                    OnPropertyChanged("ID");
                }
            }
        }
        public string Name
        {
            get { return Task.Name; }
            set
            {
                if (Task.Name != value)
                {
                    Task.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public string Description
        {
            get { return Task.Description; }
            set
            {
                if (Task.Description != value)
                {
                    Task.Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
        public TaskStatus TaskStatus
        {
            get { return Task.Status; }
            set
            {
                if (Task.Status != value)
                {
                    Task.Status = value;
                    OnPropertyChanged("TaskStatus");
                }
            }
        }
        public DateTime StartTime
        {
            get { return Task.StartTime; }
        }
        public DateTime Deadline
        {
            get { return Task.Deadline; }
            set
            {
                if (Task.Deadline != value)
                {
                    Task.Deadline = value;
                    OnPropertyChanged("Deadline");
                }
            }
        }
        public DateTime FinishedTime
        {
            get { return Task.FinishedTime; }
            set
            {
                if (Task.FinishedTime != value)
                {
                    Task.FinishedTime = value;
                    OnPropertyChanged("FinishedTime");
                }
            }
        }
        public float Cost
        {
            get { return Task.Cost; }
            set
            {
                if (Task.Cost != value)
                {
                    Task.Cost = value;
                    OnPropertyChanged("Cost");
                }
            }
        }
        public string Project
        {
            get
            {
                return TaskProject.Name;
            }
        }
        public bool IsHadImplementor
        {
            get
            {
                return ((Task.Implementer != null) && (Task.Implementer != ObjectId.Empty));
            }
        }
        public bool IsHadNotImplementor
        {
            get { return !IsHadImplementor; }
        }
        public string ImplementorName
        {
            get { return Implementor.Name; }
            set
            {
                if (Implementor.Name != value)
                {
                    Implementor.Name = value;
                    OnPropertyChanged("ImplementorName");
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
