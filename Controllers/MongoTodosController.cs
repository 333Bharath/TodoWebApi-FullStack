using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDoAPI.Models.MongoDB;
using AutoMapper;

using ToDoAPI.Dtos;

namespace ToDoAPI.Controllers
{

    public class MongoTodosController : ApiController
    {

        public IMongoDatabase mongoDb = new MongoClient(ConfigurationManager.AppSettings["connectionString"]).GetDatabase("Todo");

        [HttpGet]
        public IHttpActionResult Todos()
        {
            var collection = mongoDb.GetCollection<Todo>("Todos");
            var todos = collection.Find(new BsonDocument()).ToList().Select(Mapper.Map<Todo, MongoTodoDto>);
            return Ok(todos);
        }

        [HttpGet]
        public IHttpActionResult Todo(string id)
        {
            try
            {
                var collection = mongoDb.GetCollection<Todo>("Todos");
                //  collection.Find(Builders<Employee>.Filter.Where(s => s.Id == id)).FirstOrDefault()
                var todo = Mapper.Map<Todo, MongoTodoDto>(collection.Find(Builders<Todo>.Filter.Where(x => x.Id == id)).FirstOrDefault());
                return Ok(todo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Todo([FromBody] MongoTodoDto todo)
        {
            var collection = mongoDb.GetCollection<Todo>("Todos");
            var MongoTodo = Mapper.Map<MongoTodoDto, Todo>(todo);
            collection.InsertOneAsync(MongoTodo);
            todo.Id = MongoTodo.Id;
            return Created(new Uri(Request.RequestUri + "/" + todo.Id), todo);
        }

        [HttpPut]
        public IHttpActionResult TodoUpdate([FromUri] string id, [FromBody] MongoTodoDto todo)
        {
            var collecion = mongoDb.GetCollection<Todo>("Todos");
            var result = collecion.FindOneAndUpdateAsync(Builders<Todo>.Filter.Where(x => x.Id == id), Builders<Todo>.Update.Set("TodoData", todo.TodoData).Set("IsCompleted", todo.IsCompleted)).Result;
            // var res = collecion.FindOneAndReplace(Builders<Todo>.Filter.Eq("id",))
            return Ok(result);
        }

        [HttpDelete]
        public IHttpActionResult TodoDelete(string id)
        {
            var collection = mongoDb.GetCollection<Todo>("Todos");
            //  var obj = collection.FindAsync(Builders<Todo>.Filter.Where(x=>x.Id==id));
            //  var todo = collection.Find(Builders<Todo>.Filter.Where(x => x.Id == id)).FirstOrDefault();
            var result = collection.DeleteOne(Builders<Todo>.Filter.Where(x => x.Id == id));
            //  var result =  collection.DeleteOneAsync(Builders<Todo>.Filter.Eq("_id", id)).Result;
            return Ok(result);
        }


    }
}
