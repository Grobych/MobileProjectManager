using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using MobileProjectManager.Views;
using MobileProjectManager.Models;
using System.Reflection;
using System;

namespace MobileProjectManager.ViewModels
{
    public class ProjectViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation Navigation { get; set; }
        ProjectListViewModel lvm;
        public ObservableCollection<ProfileViewModel> WorkerList { get; set; }
        public ProfileViewModel selectedWorker;

        public ICommand EditProjectCommand { protected set; get; }
        public ICommand CancelEditCommand { protected set; get; }
        public ICommand CreateProjectCommand { protected set; get; }
        public ICommand UpdateProjectCommand { protected set; get; }
        public ICommand DeleteProjectCommand { protected set; get; }

        public Project Project { get; set ; }
        public Project EditableProject { get; set; }
        
        public ProjectViewModel(Project project)
        {
            Project = project;
            EditableProject = (Project)project.Clone();
            WorkerList = new ObservableCollection<ProfileViewModel>();
            EditProjectCommand = new Command(EditCommand);
            CancelEditCommand = new Command(CancelCommand);
            CreateProjectCommand = new Command(CreateCommand);
            UpdateProjectCommand = new Command(UpdateCommand);
            DeleteProjectCommand = new Command(DeleteCommand);
        }
        public ProjectViewModel()
        {
            Project = new Project();
            EditableProject = (Project)Project.Clone();
            WorkerList = new ObservableCollection<ProfileViewModel>();
            EditProjectCommand = new Command(EditCommand);
            CancelEditCommand = new Command(CancelCommand);
            CreateProjectCommand = new Command(CreateCommand);
            UpdateProjectCommand = new Command(UpdateCommand);
            DeleteProjectCommand = new Command(DeleteCommand);
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

        public long ID
        {
            get { return Project.ID; }
            set
            {
                if (Project.ID != value)
                {
                    Project.ID = value;
                    OnPropertyChanged("ID");
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
        public DateTime Deadline
        {
            get { return Project.Deadline; }
            set
            {
                if (Project.Deadline != value)
                {
                    Project.Deadline = value;
                    OnPropertyChanged("Deadline");
                }
            }
        }
        public DateTime Finish
        {
            get { return Project.Finish; }
            set
            {
                if (Project.Finish != value)
                {
                    Project.Finish = value;
                    OnPropertyChanged("Finish");
                }
            }
        }
        public string Status
        {
            get {
                if (Project.Status) return "Завершен";
                else return "В процессе";
            }
            //set
            //{
            //    if (Project.Status != value)
            //    {
            //        Project.Status = value;
            //        OnPropertyChanged("Status");
            //    }
            //}
        }
        public ProfileViewModel SelectedWorker
        {
            get { return selectedWorker; }
            set
            {
                
                    try
                    {
                    if (selectedWorker != value)
                    {
                        ProfileViewModel tempWorker = value;
                        selectedWorker = null;
                        OnPropertyChanged("SelectedWorker");
                        Navigation.PushAsync(new ProfilePage(tempWorker));
                    }
                    } catch (TargetInvocationException e)
                    {
                        System.Console.WriteLine(e.InnerException.StackTrace);
                        System.Console.WriteLine("--------------------------");
                        System.Console.WriteLine(e.InnerException.StackTrace);
                        System.Console.WriteLine("--------------------------");
                        System.Console.WriteLine(e.Message);
                    }

            }
        }


        public bool IsValid
        {
            get
            {
                return ((!string.IsNullOrEmpty(Name)) ||
                    (!string.IsNullOrEmpty(Client)) ||
                    (!string.IsNullOrEmpty(Description)));
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public void EditCommand(object projectObject)
        {
            EditableProject = (Project)Project.Clone();
            lvm.Navigation.PushAsync(new ProjectEditPage(this));// { EditableProject = (Project) this.Project.Clone()});
        }

        public void CancelCommand()
        {
            lvm.Back();
        }
        public void CreateCommand(object project)
        {
            if (project is ProjectViewModel projectViewModel && projectViewModel.IsValid)
            {
                lvm.AddProject(projectViewModel);
                lvm.Back();
            }
            else
            {
                // TODO: Make toast
            }
        }
        public void DeleteCommand()
        {
            lvm.DeleteProject(this);
            lvm.Back();
            lvm.Back();
        }
        public void UpdateCommand(object project)
        {
            ProjectViewModel pvm = project as ProjectViewModel;
            Project = EditableProject;
            lvm.curerentProject.Project = (Project)EditableProject.Clone();
            // TODO: fix return to prefious page
            lvm.UpdateProject(project as ProjectViewModel);
            lvm.Back();
            lvm.Back();
            lvm.Navigation.PushAsync(new ProjectInfoPage(this));
        }

    }
}