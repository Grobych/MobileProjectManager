using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using MobileProjectManager.Models;
using MongoDB.Bson;
using Xamarin.Forms;

namespace MobileProjectManager.ViewModels
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        public Task Task { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public TaskListViewModel tlv;

        public User Implementor { get; set; }

        public ICommand SaveTaskCommand { protected set; get; }

        public TaskViewModel(TaskListViewModel viewModel)
        {
            tlv = viewModel;
            Task = new Task
            {
                StartTime = DateTime.Now,
                Deadline = DateTime.Now
            };
            SaveTaskCommand = new Command(SaveTask);
        }
        
        private void SaveTask()
        {
            tlv.AddTask(this);
            Database.Database.AddTaskToProject()
            NavigationUtil.Navigation.PopAsync();
        }

        public TaskViewModel(Task task)
        {
            this.Task = task;
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

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
