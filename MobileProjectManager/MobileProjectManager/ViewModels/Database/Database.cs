using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        public static void SaveProjectToDB(Project project)
        {
            try
            {
                var collection = database.GetCollection<Project>("projects");
                System.Threading.Tasks.Task task = collection.InsertOneAsync(project);
                task.Wait();
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public async static void GetProjectsAllFromDB(ProjectListViewModel model)
        {
            var collection = database.GetCollection<Project>("projects");
            var filter = new BsonDocument();
            var projects = await collection.Find(filter).ToListAsync();
            foreach (var doc in projects)
            {
                Console.WriteLine(doc);
                model.AddProject(new ProjectViewModel(doc,model),false);
            }
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
        public static List<User> GetUsersFromTeam(ObjectId teamID)
        {
            var collectionTeam = database.GetCollection<Team>("teams");
            var collectionUser = database.GetCollection<User>("users");

            var filter = new BsonDocument();
            var team = collectionTeam.FindSync(filter).First();
            IEnumerable<ObjectId> ids = team.WorkersId.ToArray();
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


    }
}
