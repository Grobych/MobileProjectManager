﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using MobileProjectManager.Models;
using MongoDB.Bson;

namespace MobileProjectManager.ViewModels
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        public Task Task { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public TaskViewModel()
        {
            Task = new Task
            {
                StartTime = DateTime.Now,
                Deadline = DateTime.Now
            };
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
