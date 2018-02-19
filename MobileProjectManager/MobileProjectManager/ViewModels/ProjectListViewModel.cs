using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using MobileProjectManager.Views;
using MobileProjectManager.Models;

namespace MobileProjectManager.ViewModels
{
    public class ProjectListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ProjectViewModel> Projects { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreateProjectCommand { protected set; get; }
        public ICommand DeleteProjectCommand { protected set; get; }
        public ICommand SaveProjectCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }
        ProjectViewModel selectedProject;

        public INavigation Navigation { get; set; }

        public ProjectListViewModel()
        {
            Projects = new ObservableCollection<ProjectViewModel>();
            CreateProjectCommand = new Command(CreateProject);
            DeleteProjectCommand = new Command(DeleteProject);
            SaveProjectCommand = new Command(SaveProject);
            BackCommand = new Command(Back);
        }

        public ProjectViewModel SelectedProject
        {
            get { return selectedProject; }
            set
            {
                if (selectedProject != value)
                {
                    ProjectViewModel tempProject = value;
                    selectedProject = null;
                    OnPropertyChanged("SelectedProject");
                    Navigation.PushAsync(new ProjectPage(tempProject));
                }
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void CreateProject()
        {
            Navigation.PushAsync(new ProjectPage(new ProjectViewModel() { ListViewModel = this }));
        }
        private void Back()
        {
            Navigation.PopAsync();
        }
        private void SaveProject(object projectObject)
        {
            ProjectViewModel project = projectObject as ProjectViewModel;
            if (project != null && project.IsValid)
            {
                Projects.Add(project);
            }
            Back();
        }
        private void DeleteProject(object projectObject)
        {
            ProjectViewModel project = projectObject as ProjectViewModel;
            if (project != null)
            {
                Projects.Remove(project);
            }
            Back();
        }
    }
}