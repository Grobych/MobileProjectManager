using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using MobileProjectManager.Views;
using MobileProjectManager.Models;

using MobileProjectManager.ViewModels.Database;

namespace MobileProjectManager.ViewModels
{
    public class ProjectListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ProjectViewModel> Projects { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ChectDBConnectionCommand { protected set; get; }

        public ICommand CreateProjectCommand { protected set; get; }
        public ICommand DeleteProjectCommand { protected set; get; }
        public ICommand SaveProjectCommand { protected set; get; }
        public ICommand AddProjectCommand { protected set; get; }
        public ICommand UpdateProjectCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }
        
        ProjectViewModel selectedProject;
        public ProjectViewModel curerentProject { get; set; }

        public INavigation Navigation { get; set; }

        public ProjectListViewModel()
        {
            Projects = new ObservableCollection<ProjectViewModel>();
            Database.Database.GetProjectsAllFromDB(this);
            CreateProjectCommand = new Command(CreateProject);
            ChectDBConnectionCommand = new Command(Database.Database.Connect);
            BackCommand = new Command(Back);
        }

        public ProjectViewModel SelectedProject
        {
            get { return selectedProject; }
            set
            {
                if (selectedProject != value)
                {
                    selectedProject = null;
                    OnPropertyChanged("SelectedProject");
                    curerentProject = value;
                    Navigation.PushAsync(new ProjectInfoPage(curerentProject));
                }
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public void CreateProject()
        {
            Navigation.PushAsync(new ProjectCreatePage(new ProjectViewModel() { ListViewModel = this }));
        }
        public void Back()
        {
            Navigation.PopAsync();
        }
        public void AddProject(ProjectViewModel project)
        {
            Database.Database.SaveProjectToDB(project.Project);
            Projects.Add(project);
        }
        public void AddProject(ProjectViewModel project, bool saveToDB)
        {
            if (saveToDB) Database.Database.SaveProjectToDB(project.Project);
            Projects.Add(project);
        }
        public void SaveProject()
        {
            // TODO: define method SaveProject
        }
        public void DeleteProject(ProjectViewModel project)
        {
            if (project != null)
            {
                Database.Database.DeleteProject(project.Project.ID);
                Projects.Remove(project);
            }
        }

        public void UpdateProject(ProjectViewModel tempProject)
        {
            foreach (ProjectViewModel pr in Projects)
            {
                if (pr.Project.ID == tempProject.Project.ID)
                {
                    Projects[Projects.IndexOf(pr)] = tempProject;
                    break;
                }
            }
            // TODO: fix update ProjectInfoPage
        }
    }
}