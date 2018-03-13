using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using MobileProjectManager.Views;
using MobileProjectManager.Models;

using MobileProjectManager.ViewModels.Database;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace MobileProjectManager.ViewModels
{
    public class ProjectListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ProjectViewModel> Projects { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreateProjectCommand { protected set; get; }
        public ICommand DeleteProjectCommand { protected set; get; }
        public ICommand SaveProjectCommand { protected set; get; }
        public ICommand AddProjectCommand { protected set; get; }
        public ICommand UpdateProjectCommand { protected set; get; }
        
        ProjectViewModel selectedProject;
        public ProjectViewModel currentProject { get; set; }

        public ProjectListViewModel()
        {
            Projects = new ObservableCollection<ProjectViewModel>();
            // TODO: get only user's projects
            Database.Database.GetProjectsAllFromDB(this);
            CreateProjectCommand = new Command(CreateProject);
        }
        public ProjectListViewModel(ObservableCollection<ProjectViewModel> list)
        {
            Projects = list;
            CreateProjectCommand = new Command(CreateProject);
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
                    currentProject = value;
                    NavigationUtil.Navigation.PushAsync(new ProjectInfoPage(currentProject));
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
            NavigationUtil.Navigation.PushAsync(new ProjectCreatePage(new ProjectViewModel() { ListViewModel = this }));
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
                    // TODO: update project in DB
                    Projects[Projects.IndexOf(pr)] = tempProject;
                    Debug.WriteLine(tempProject.Project.Name);
                    Database.Database.UpdateProject(tempProject.Project);
                    break;
                }
            }
        }
    }
}