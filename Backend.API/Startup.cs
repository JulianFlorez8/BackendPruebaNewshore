using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using Backend.DataAccess.Data;
using System.Web.Http;



namespace Backend.API
{
    public partial  class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(BackContext.Create);
           
        }
        
       
    }
}
