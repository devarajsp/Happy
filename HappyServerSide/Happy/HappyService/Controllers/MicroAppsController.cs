using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Converters;

using HappyCommon;

namespace HappyService.Controllers
{
    public class MicroAppsController : ApiController
    {

        [HttpGet]
        public async Task<JsonResult<List<MicroApp>>> GetAll()
        {
            List<MicroApp> appList = new List<MicroApp>();

            MicroApp app1 = new MicroApp("app1", "app1/index.html1", "testa pp");
            MicroApp app2 = new MicroApp("app2", "app2/index.html1", "testa pp");

            /* string[] roles1 = { "Adult", "Medium" };
            string[] roles2 = { "Young", "Medium" };
            string[] roles3 = { "Adult" };

            app1.AllowedRoles = roles1;
            app2.AllowedRoles = roles3;

            appList.Add(app1); appList.Add(app2);
            */

            MicroAppsRepository mRepo = new MicroAppsRepository(MicroAppsRepository.DEFAULT_TABLE_NAME, MicroAppsRepository.DEFAULT_PART_KEY);
            appList = mRepo.GetAll();

            return Json<List<MicroApp>>(appList);
        }


        [HttpGet]
        public async Task<JsonResult<List<MicroApp>>> GetAllForRole(string role)
        {
            List<MicroApp> appList = new List<MicroApp>();
            
            MicroAppsRepository mRepo = new MicroAppsRepository(MicroAppsRepository.DEFAULT_TABLE_NAME, MicroAppsRepository.DEFAULT_PART_KEY);
            appList = mRepo.GetAllForRole(role);

            return Json<List<MicroApp>>(appList);
        }


        [HttpPost]
        public async Task<JsonResult<MicroApp>> InsertMicroApp(MicroApp mApp)
        {
            MicroAppsRepository mRepo = new MicroAppsRepository(MicroAppsRepository.DEFAULT_TABLE_NAME, MicroAppsRepository.DEFAULT_PART_KEY);
            MicroApp microApp;
            char[] delim = { ',' };
            if (mApp.Name != null && mApp.Url != null)
            {
                microApp = new MicroApp(mApp.Name, mApp.Url, mApp.Description);
                if (mApp.roles != null)
                {
                    microApp.roles = mApp.roles;
                    microApp.AllowedRoles = mApp.roles.Split(delim);
                }
                mRepo.InsertMicroAppDetails(microApp);
                mApp = microApp;
            }
            
            return Json<MicroApp>(mApp);
        }

        // GET: api/MicroApps
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MicroApps/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MicroApps
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MicroApps/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MicroApps/5
        public void Delete(int id)
        {
        }
    }
}
