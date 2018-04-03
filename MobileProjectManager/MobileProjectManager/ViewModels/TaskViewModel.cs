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
        public string Comment { get; set; }

        public User Implementor { get; set; }

        public ICommand SaveTaskCommand { protected set; get; }
        public ICommand GetTaskCommand { protected set; get; }
        public ICommand SetReportCommand { protected set; get; }
        public ICommand AcceptTaskCommand { protected set; get; }
        public ICommand DeclineTaskCommand { protected set; get; }
        public ICommand DeleteTaskCommand { protected set; get; }

        private void CommandInit()
        {
            SaveTaskCommand = new Command(SaveTask);
            GetTaskCommand = new Command(GetTaskByMyself);
            SetReportCommand = new Command(SetReport);
            AcceptTaskCommand = new Command(CheckTaskTrue);
            DeclineTaskCommand = new Command(CheckTaskFalse);
            DeleteTaskCommand = new Command(DeleteTask);
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

        // TODO: setup upgating task page
        private void GetTaskByMyself()
        {
            Debug.WriteLine("GetTask");
            Task.Implementer = Auth.CurrentUser.ID;
            this.Implementor = Auth.CurrentUser;
            Task.Status = TaskStatus.InProgress;
            Database.Database.UpdateTask(Task);
            Notification notification = new Notification()
            {
                From = Auth.CurrentUser.ID,
                To = this.tlv.pvm.ProjectManager.ID,
                Type = NotificationType.GetTaskReport,
                Line = new BsonDocument()
                .Add("TaskName", Task.Name)
                .Add("UserName", Auth.CurrentUser.Name)
            };
            Database.Database.AddNotification(ref notification);
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
        private void DeleteTask()
        {
            tlv.DeleteTask(this);
            Database.Database.DeleteTask(this.Task);
            NavigationUtil.Navigation.PopAsync();
        }

        private void SetReport()
        {
            Notification notification = new Notification
            {
                From = Auth.CurrentUser.ID,
                To = tlv.pvm.ProjectManager.ID,
                Type = NotificationType.TaskCompleteReport,
                Line = new BsonDocument()
                .Add("Comment", Comment)
                .Add("ProjectName",tlv.pvm.Project.Name)
                .Add("UserName",Auth.CurrentUser.Name)
                .Add("Task",Task.Name)
            };
            Database.Database.AddNotification(ref notification);
            Task.Status = TaskStatus.Completed;
            Database.Database.UpdateTask(Task);
            Toast.ShowToast("Complete!", "Report has been sended");
        }


        // TODO: Rewrite
        private void CheckTaskTrue()
        {
            CheckTask(true);
        }
        private void CheckTaskFalse()
        {
            CheckTask(false);
        }

        private void CheckTask(bool accept)
        {
            if (accept)
            {
                Task.Status = TaskStatus.Confirmed;
                Notification notification = new Notification
                {
                    From = Auth.CurrentUser.ID,
                    To = Task.Implementer,
                    Type = NotificationType.TaskReportApproved,
                    Line = new BsonDocument()
                    .Add("TaskName",Task.Name)
                };
                Database.Database.AddNotification(ref notification);
                Toast.ShowToast("Complete!", "Task hes been approved");
            } else
            {
                Task.Status = TaskStatus.InProgress;
                string Comment = InputDialog.InputBox(NavigationUtil.Navigation).Result;
                Notification notification = new Notification
                {
                    From = Auth.CurrentUser.ID,
                    To = Task.Implementer,
                    Type = NotificationType.TaskReportDeclined,
                    Line = new BsonDocument()
                    .Add("TaskName",Task.Name)
                    .Add("Comment",Comment)
                };
                Database.Database.AddNotification(ref notification);
                Toast.ShowToast("Complete!", "Task still in progress");
            }
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
            get
            {
                if (Implementor != null)
                    return Implementor.Name;
                else return "";
            }
            set
            {
                if (Implementor.Name != value)
                {
                    Implementor.Name = value;
                    OnPropertyChanged("ImplementorName");
                }
            }
        }
        public bool IsUserImplementor
        {
            // TODO: Check 1
            get {
                if (Implementor == null) return false;
                return Auth.CurrentUser.ID == Implementor.ID;
            }
        }
        public bool IsUserProjectManager
        {
            get { return (Auth.CurrentUser.ID == this.tlv.pvm.ProjectManager.ID); }
        }
        public bool IsUserPMandTaskCompleted
        {
            get
            {
                return (IsUserProjectManager && (Task.Status == TaskStatus.Completed));
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
