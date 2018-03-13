using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using MobileProjectManager.Views;
using MobileProjectManager.Models;
using System.Reflection;
using System;

using MongoDB.Bson;
using MobileProjectManager.ViewModels.Utils;

namespace MobileProjectManager.ViewModels
{
    public class ProjectViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ProjectListViewModel lvm;
        public ObservableCollection<ProfileViewModel> WorkerList { get; set; }
        public ProfileViewModel selectedWorker;

        public ICommand EditProjectCommand { protected set; get; }
        public ICommand CancelEditCommand { protected set; get; }
        public ICommand CreateProjectCommand { protected set; get; }
        public ICommand UpdateProjectCommand { protected set; get; }
        public ICommand DeleteProjectCommand { protected set; get; }
        public ICommand ToProjectManagerPage { protected set; get; }

        public Project Project { get; set ; }
        public Project EditableProject { get; set; }
 //       public ObjectId ProjectManager { get; set; }
        
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
            ToProjectManagerPage = new Command(ToPMCommand);
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
            ToProjectManagerPage = new Command(ToPMCommand);
        }
        public ProjectViewModel(Project project, ProjectListViewModel listViewModel)
        {
            Project = project;
            lvm = listViewModel;
            EditableProject = (Project)project.Clone();
            WorkerList = new ObservableCollection<ProfileViewModel>();
            EditProjectCommand = new Command(EditCommand);
            CancelEditCommand = new Command(CancelCommand);
            CreateProjectCommand = new Command(CreateCommand);
            UpdateProjectCommand = new Command(UpdateCommand);
            DeleteProjectCommand = new Command(DeleteCommand);
            ToProjectManagerPage = new Command(ToPMCommand);
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

        public ObjectId ID
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
        public float Price
        {
            get { return Project.Price; }
            set
            {
                if (Project.Price != value)
                {
                    Project.Price = value;
                    OnPropertyChanged("Price");
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
                if (Project.Status) return "��������";
                else return "� ��������";
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

        public bool IsUserPM
        {
            get
            {
                bool res = (Auth.CurrentUser.ID == Project.ProjectManager.ID);
                return res;
            }
        }

        public string ProjectManagerName
        {
            get {
                return Project.ProjectManager.Name;
            }
            set
            {
                if (Project.ProjectManager.Name != value)
                {
                    Project.ProjectManager.Name = value;
                    OnPropertyChanged("ProjectManagerName");
                }
            }
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
                        NavigationUtil.Navigation.PushAsync(new ProfilePage(tempWorker));
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
                return ((!string.IsNullOrEmpty(Name)) &
                    (!string.IsNullOrEmpty(Client)) &
                    (!string.IsNullOrEmpty(Description))) &
                    (!float.IsNaN(Price));
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
            NavigationUtil.Navigation.PushAsync(new ProjectEditPage(this));
        }

        public void CancelCommand()
        {
            NavigationUtil.Navigation.PopAsync();
        }
        public void CreateCommand(object project)
        {
            ProjectViewModel pvm = project as ProjectViewModel;
            if (pvm.IsValid)
            {
                pvm.Project.ProjectManager = Auth.CurrentUser;
                lvm.AddProject(pvm);
                NavigationUtil.Navigation.PopAsync();
            }
            else
            {
                Utils.Toast.ShowToast("Error", "Check input fields", false);
            }
        }
        public void DeleteCommand()
        {
            lvm.DeleteProject(this);
            NavigationUtil.Navigation.PopAsync();
            NavigationUtil.Navigation.PopAsync();
        }
        public void UpdateCommand(object project)
        {
            Project = EditableProject;
            lvm.currentProject.Project = (Project)EditableProject.Clone();
            ProjectViewModel pvm = project as ProjectViewModel;
            lvm.UpdateProject(pvm);
            NavigationUtil.Navigation.InsertPageBefore(new ProjectInfoPage(this),NavigationUtil.getPreviousPage());
            NavigationUtil.Navigation.RemovePage(NavigationUtil.getPreviousPage());
            NavigationUtil.Navigation.PopAsync();
            // TODO: mb create BackToNewPage() func?
        }

        private void ToPMCommand(object obj)
        {
            NavigationUtil.Navigation.PushAsync(new ProfilePage(new ProfileViewModel(Project.ProjectManager)));
        }
    }
}