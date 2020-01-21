using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiectNoSQL
{
    public class StudentService
    {
        private readonly IMongoCollection<Student> _students;

        public StudentService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("ExpenseStoreMongoDb"));
            var database = client.GetDatabase("proiectDB");
            _students = database.GetCollection<Student>("students");
        }

        public List<Student> Get()
        {
            return _students.Find(new BsonDocument()).ToList();
        }

        public Student Get(string id)
        {
            return _students.Find<Student>(Student => Student.Id == id).FirstOrDefault();
        }

        public Student Create(Student student)
        {
            _students.InsertOne(student);
            return student;
        }

        public void Update(string id, Student bookIn)
        {
            _students.ReplaceOne(Student => Student.Id == id, bookIn);
        }

        public void Remove(Student bookIn)
        {
            _students.DeleteOne(Student => Student.Id == bookIn.Id);
        }

        public void Remove(string id)
        {
            _students.DeleteOne(Student => Student.Id == id);
        }
    }
}
