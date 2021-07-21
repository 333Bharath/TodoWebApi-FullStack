using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoAPI.Models;
using ToDoAPI.Dtos;
using MongoTodo = ToDoAPI.Models.MongoDB.Todo;

namespace ToDoAPI.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Todo, TodoDto>();
            Mapper.CreateMap<TodoDto, Todo>();
            Mapper.CreateMap<MongoTodo, MongoTodoDto>();
            Mapper.CreateMap<MongoTodoDto, MongoTodo >();
        }
    }
}