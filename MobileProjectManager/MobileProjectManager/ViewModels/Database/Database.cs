using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using MobileProjectManager.Models;

namespace MobileProjectManager.ViewModels.Database
{
    public class Database
    {
        private static MongoClient client;
        private static IMongoDatabase database;
        private static string connectionString = "mongodb://grobych:grob1995grob@cluster0-shard-00-00-6h2to.mongodb.net:27017,cluster0-shard-00-01-6h2to.mongodb.net:27017,cluster0-shard-00-02-6h2to.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin";

        public static void Connect()
        {
            try
            {
                client = new MongoClient(connectionString);
                database = client.GetDatabase("ProjectManager");
                Console.WriteLine("OK");
            } catch (MongoConnectionException e)
            {
                Console.WriteLine("Connection Error!" + e);
            }
            
        }
        public static bool CheckConnection()
        {
            if (client == null) return false;
            return true;
        }
        public static User GetUserFromId(ObjectId id)
        {
            var collection = database.GetCollection<User>("users");
            var filter = Builders<User>.Filter.Eq("ID", id);
            var result = collection.Find(filter);
            if (result.Count() > 0)
            {
                return collection.Find(filter).First();
            }
            else
            {
                return null;
            }
        }
        public static List<User> GetProjectWorkers(Project project)
        {
            var collectionUser = database.GetCollection<User>("users");
            IEnumerable<ObjectId> ids = project.WorkerIDList.ToArray();
            var filterID = Builders<User>.Filter.In(user => user.ID, ids).ToBsonDocument();
            List<User> result = collectionUser.Find(filterID).ToList();
            return result;
        }
        public static void UpdateProject(Project project)
        {
            var collection = database.GetCollection<Project>("projects");
            var filter = Builders<Project>.Filter.Eq(s => s.ID, project.ID);
            var result = collection.ReplaceOneAsync(filter, project);
        }
        public static void SaveProjectToDB(Project project)
        {
            try
            {
                var collection = database.GetCollection<Project>("projects");
                collection.InsertOneAsync(project);
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public static Project GetProject(ObjectId id)
        {
            var collection = database.GetCollection<Project>("projects");
            var filter = Builders<Project>.Filter.Eq("_id", id);
            var result = collection.Find(filter);
            if (result.Count() > 0)
            {
                return collection.Find(filter).First();
            }
            else
            {
                return null;
            }
        }
        public static List<Project> GetProjects(ObjectId UserID)
        {
            var collection = database.GetCollection<Project>("projects");
            var builder = Builders<Project>.Filter;
            var filter = builder.Eq("ProjectManager", UserID) | builder.Eq("WorkerIDList", UserID);
            List<Project> res = collection.FindSync(filter).ToList();
            return res;
        }
        public async static void DeleteTask(Models.Task task)
        {
            var collection = database.GetCollection<Models.Task>("tasks");
            var filter = Builders<Models.Task>.Filter.Eq("ID", task.ID);
            var tasks = await collection.DeleteOneAsync(filter);
        }
        public async static void DeleteProject(ObjectId ID)
        {
            var collection = database.GetCollection<Project>("projects");
            var filter = Builders<Project>.Filter.Eq("ID", ID);
            var projects = await collection.DeleteOneAsync(filter);
        }
        private static async void GetDatabaseNames(MongoClient client)
        {
            using (var cursor = await client.ListDatabasesAsync())
            {
                var databaseDocuments = await cursor.ToListAsync();
                foreach (var databaseDocument in databaseDocuments)
                {
                    Console.WriteLine(databaseDocument["name"]);
                }
            }
        }
        private static async void GetCollectionsNames(MongoClient client)
        {
            using (var cursor = await client.ListDatabasesAsync())
            {
                var dbs = await cursor.ToListAsync();
                foreach (var db in dbs)
                {
                    Console.WriteLine("В базе данных {0} имеются следующие коллекции:", db["name"]);

                    IMongoDatabase database = client.GetDatabase(db["name"].ToString());

                    using (var collCursor = await database.ListCollectionsAsync())
                    {
                        var colls = await collCursor.ToListAsync();
                        foreach (var col in colls)
                        {
                            Console.WriteLine(col["name"]);
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
        internal static Team GetTeam(ObjectId id)
        {
            var collection = database.GetCollection<Team>("teams");
            var builder = Builders<Team>.Filter;
            var filter = builder.Eq("_id", id);
            var result = collection.Find(filter);
            if (result.Count() > 0)
            {
                Team user = collection.Find(filter).First();
                return user;
            }
            else
            {
                return null;
            }
        }
        public static void AddUser(ref User user)
        {
            try
            {
                var collection = database.GetCollection<User>("users");
                System.Threading.Tasks.Task task = collection.InsertOneAsync(user);
                task.Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public static bool GetUser(ref User user)
        {
            var collection = database.GetCollection<User>("users");
            var builder = Builders<User>.Filter;
            var filter = builder.Eq("Name", user.Name) & builder.Eq("Password", user.Password);
            var result = collection.Find(filter);
            if (result.Count() > 0)
            {
                User people = collection.Find(filter).First();
                user = people;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static User GetUser(ObjectId id)
        {
            var collection = database.GetCollection<User>("users");
            var builder = Builders<User>.Filter;
            var filter = builder.Eq("_id", id);
            var result = collection.Find(filter);
            if (result.Count() > 0)
            {
                User user = collection.Find(filter).First();
                return user;
            }
            else
            {
                return null;
            }
        }
        public static User GetUser(string name)
        {
            var collection = database.GetCollection<User>("users");
            var builder = Builders<User>.Filter;
            var filter = builder.Eq("Name", name);
            var result = collection.Find(filter);
            if (result.Count() > 0)
            {
                User user = collection.Find(filter).First();
                return user;
            }
            else
            {
                return null;
            }
        }
        public static List<User> GetUsersFromTeam(ObjectId teamID)
        {
            var collectionTeam = database.GetCollection<Team>("teams");
            var collectionUser = database.GetCollection<User>("users");

            var filter = new BsonDocument();
            // TODO: rewrite (IN FIRST!)
            var team = collectionTeam.FindSync(filter).First();
            IEnumerable<ObjectId> ids = team.WorkersID.ToArray();
            var filterID = Builders<User>.Filter.In(user => user.ID, ids).ToBsonDocument();
            List<User> result = collectionUser.Find(filterID).ToList();
            return result;
        }
        public static bool CheckLogin(User user)
        {
            var collection = database.GetCollection<User>("users");

            var builder = Builders<User>.Filter;
            var filter = builder.Eq("Name", user.Name);
            var result = collection.Find(filter);
            if (result.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void AddTeamToDB(ref Team team)
        {
            var collection = database.GetCollection<Team>("teams");
            collection.InsertOne(team);
        }
        public static List<Team> GetTeamsFromDB(User user)
        {
            var collection = database.GetCollection<Team>("teams");
            var builder = Builders<Team>.Filter;
            var filter = builder.Eq("ManagerID", user.ID) | builder.Eq("WorkersID", user.ID);
            List<Team> res = collection.FindSync(filter).ToList();
            return res;
        }
        public static void UpdateTeam(Team team)
        {
            var collection = database.GetCollection<Team>("teams");
            var filter = Builders<Team>.Filter.Eq(s => s.ID, team.ID);
            var result = collection.ReplaceOneAsync(filter, team);
        }
        public static void AddNotification(ref Notification notification)
        {
            var collection = database.GetCollection<Notification>("notifications");
            collection.InsertOne(notification);
        }
        public static void DeleteNotification(ObjectId ID)
        {
            var collection = database.GetCollection<Notification>("notifications");
            var filter = Builders<Notification>.Filter.Eq("ID", ID);
            var projects = collection.DeleteOneAsync(filter);
        }
        public static List<Notification> getNotifications(User user)
        {
            var collection = database.GetCollection<Notification>("notifications");
            var builder = Builders<Notification>.Filter;
            var filter = builder.Eq("To", user.ID);
            List<Notification> res = collection.FindSync(filter).ToList();
            return res;
        }
        public static void AddTaskToProject(Project project, ref Models.Task task)
        {
            var taskCollection = database.GetCollection<Models.Task>("tasks");
            taskCollection.InsertOne(task);
            project.TaskListID.Add(task.ID);
            UpdateProject(project);
        }
        public static List<Models.Task> GetTaskFromProject(Project project)
        {
            var collection = database.GetCollection<Models.Task>("tasks");
            var builder = Builders<Models.Task>.Filter;
            var filter = builder.Eq("ProjectID", project.ID);
            var ret = collection.FindSync(filter);
            List <Models.Task> res = ret.ToList();
            return res;
        }
        public static List<Models.Task> GetTaskFromUser(User user)
        {
            List<Project> ProjectsList = GetProjects(user.ID);
            List<ObjectId> TaskID = new List<ObjectId>();
            foreach (var pr in ProjectsList)
            {
                TaskID.AddRange(pr.TaskListID);
            }
            var collection = database.GetCollection<Models.Task>("tasks");
            var builder = Builders<Models.Task>.Filter;
            var filter = builder.Eq("_id", TaskID);
            List<Models.Task> res = collection.FindSync(filter).ToList();
            return res;
        }
        public static void UpdateTask(Models.Task task)
        {
            var collection = database.GetCollection<Models.Task>("tasks");
            var filter = Builders<Models.Task>.Filter.Eq(s => s.ID, task.ID);
            var result = collection.ReplaceOneAsync(filter, task);
        }
    }
}
