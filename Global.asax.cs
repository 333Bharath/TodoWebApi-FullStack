using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using AutoMapper;
using ToDoAPI.App_Start;

namespace ToDoAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Mapper.Initialize(x => x.AddProfile<MappingProfile>());
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
