using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDoAPI.Models;
using System.Web.Http.Cors;
using AutoMapper;
using ToDoAPI.Dtos;

namespace ToDoAPI.Controllers
{
    // [DisableCors]
    // [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class TodosController : ApiController
    {
        ToDoContext _context;

        public TodosController()
        {
            _context = new ToDoContext();
        }


        [HttpGet]
        public IHttpActionResult GetToDos()
        {
            IEnumerable<TodoDto> todolist = _context.Todos.ToList().Select(Mapper.Map<Todo, TodoDto>);

            return Ok(todolist);

        }
        [HttpGet]
        public IHttpActionResult GetToDo(int id)
        {
            try
            {

                var todo = Mapper.Map<Todo, TodoDto>(_context.Todos.FirstOrDefault(x => x.Id == id));
                if (todo != null)
                    return Ok(todo);
                //  return Request.CreateResponse(HttpStatusCode.OK, todo);
                else
                    return NotFound();
                //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Todo id" + id.ToString() + "not found");
            }
            catch (Exception e)
            {
                return NotFound();
                // return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }

        }

        [HttpGet]
        public IHttpActionResult GetToDo(string iscompleted)
        {
            try
            {
               
                switch (iscompleted.ToLower())
                {
                    case "true":
                        var todos = _context.Todos.Where(x => x.IsCompleted == true).ToList().Select(Mapper.Map<Todo, TodoDto>);
                        return Ok(todos);
                    case "false":
                       var  todo = _context.Todos.Where(x => x.IsCompleted == false).ToList();
                        return Ok(todo);
                    case "all":
                        todo = _context.Todos.ToList();
                        return Ok(todo);
                    default:
                        return NotFound();

                }

            }
            catch (Exception e)
            {
                return NotFound();
            }

        }



        [HttpPost]
        public IHttpActionResult CreateToDo([FromBody] TodoDto todo)
        {
            if (ModelState.IsValid)
            {
                if (todo.TodoData == "")
                    return BadRequest("Please enter the todoData field");
                var dbtodo = Mapper.Map<TodoDto, Todo>(todo);
                _context.Todos.Add(dbtodo);
                _context.SaveChanges();
                todo.Id = dbtodo.Id;
                return Created(new Uri(Request.RequestUri + "/" + todo.Id), todo);

            }
            return BadRequest();
        }

        [HttpPut]
        public IHttpActionResult UpdateToDo(int id, TodoDto todo)
        {
            if (ModelState.IsValid)
            {

                var dbTodo = _context.Todos.FirstOrDefault(x => x.Id == id);
                if (dbTodo == null)
                {
                    return NotFound();
                }
                Mapper.Map(todo, dbTodo);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public IHttpActionResult DeleteToDo(int id)
        {

            var todo = _context.Todos.FirstOrDefault(x => x.Id == id);
            if (todo == null)
            {
                return BadRequest();
            }
            _context.Todos.Remove(todo);
            _context.SaveChanges();
            return Ok();
        }

    }
}
