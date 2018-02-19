using System.ComponentModel;
using MobileProjectManager.Models;

namespace MobileProjectManager.ViewModels
{
    public class ProjectViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ProjectListViewModel lvm;

        public Project Project { get; private set; }

        public ProjectViewModel()
        {
            Project = new Project();
        }

        public ProjectListViewModel ListViewModel
        {
            get { return lvm; }
            set
            {
                if (lvm != value)
                {
                    lvm = value;
                    OnPropertyChanged("ListViewModel");
                }
            }
        }
        public string Name
        {
            get { return Project.Name; }
            set
            {
                if (Project.Name != value)
                {
                    Project.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public string Client
        {
            get { return Project.Client; }
            set
            {
                if (Project.Client != value)
                {
                    Project.Client = value;
                    OnPropertyChanged("Client");
                }
            }
        }
        public string Description
        {
            get { return Project.Description; }
            set
            {
                if (Project.Description != value)
                {
                    Project.Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public bool IsValid
        {
            get
            {
                return ((!string.IsNullOrEmpty(Name.Trim())) ||
                    (!string.IsNullOrEmpty(Client.Trim())) ||
                    (!string.IsNullOrEmpty(Description.Trim())));
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}