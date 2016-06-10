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
    public class AlertsController : ApiController
    {
        public static string MsgName = "Empty";
        public static string MsgContent = "Empty";

        [HttpGet]
        public async Task<JsonResult<List<HappyAlert>>> GetAll()
        {
            List<HappyAlert> appList = new List<HappyAlert>();

            HappyAlert ha = new HappyAlert(MsgName, MsgContent, "Alert Message");
            appList.Add(ha);
            //HappyAlertsRepository mRepo = new HappyAlertsRepository(HappyAlertsRepository.DEFAULT_TABLE_NAME, HappyAlertsRepository.DEFAULT_PART_KEY);
            //appList = mRepo.GetAll();

            return Json<List<HappyAlert>>(appList);
        }


        [HttpGet]
        public async Task<JsonResult<List<HappyAlert>>> GetAllForRole(string role)
        {
            List<HappyAlert> appList = new List<HappyAlert>();

            HappyAlert ha = new HappyAlert(MsgName, MsgContent, "Alert Message");
            appList.Add(ha);
            //HappyAlertsRepository mRepo = new HappyAlertsRepository(HappyAlertsRepository.DEFAULT_TABLE_NAME, HappyAlertsRepository.DEFAULT_PART_KEY);
            //appList = mRepo.GetAll();


            return Json<List<HappyAlert>>(appList);
        }


        [HttpPost]
        public async Task<JsonResult<HappyAlert>> InsertHappyAlert(HappyAlert mApp)
        {
            HappyAlertsRepository mRepo = new HappyAlertsRepository(HappyAlertsRepository.DEFAULT_TABLE_NAME, HappyAlertsRepository.DEFAULT_PART_KEY);
            HappyAlert HappyAlert;
            char[] delim = { ',' };
            if (mApp.Name != null && mApp.AlertMessage != null)
            {
                //HappyAlert = new HappyAlert(mApp.Name, mApp.AlertMessage, mApp.Description);
                //if (mApp.roles != null)
                //{
                //    HappyAlert.roles = mApp.roles;
                //    HappyAlert.AllowedRoles = mApp.roles.Split(delim);
                //}
                //mRepo.InsertHappyAlertDetails(HappyAlert);
                //mApp = HappyAlert;
                MsgName = mApp.Name;
                MsgContent = mApp.AlertMessage;
            }

            return Json<HappyAlert>(mApp);
        }

        [HttpGet]
        public async Task<JsonResult<HappyAlert>> Insert(string Name, string Message, string Desc)
        {
            HappyAlertsRepository mRepo = new HappyAlertsRepository(HappyAlertsRepository.DEFAULT_TABLE_NAME, HappyAlertsRepository.DEFAULT_PART_KEY);
            HappyAlert happyAlert = new HappyAlert();
            char[] delim = { ',' };
            if (Name != null && Message != null)
            {
                happyAlert = new HappyAlert(Name, Message, Desc);

                MsgName = Name;
                MsgContent = Message;
            }

            return Json<HappyAlert>(happyAlert);
        }

    }

}
