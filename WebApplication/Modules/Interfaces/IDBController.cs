using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApplication.Modules.Interfaces
{
    public interface IDBController
    {
        IHttpActionResult Get();
        IHttpActionResult Get(string id);
        IHttpActionResult Post([FromBody]string value);
        IHttpActionResult Put([FromBody]string value);
        IHttpActionResult Delete(string id);
    }
}
