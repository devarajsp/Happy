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
    public class HAQDetailsController : ApiController
    {
        [HttpGet]
        public async Task<JsonResult<List<HAQDetails>>> GetAll()
        {
            List<HAQDetails> appList = new List<HAQDetails>();

            HAQDetailsRepository mRepo = new HAQDetailsRepository(HAQDetailsRepository.DEFAULT_TABLE_NAME, HAQDetailsRepository.DEFAULT_PART_KEY);
            appList = mRepo.GetAll();

            return Json<List<HAQDetails>>(appList);
        }


        [HttpGet]
        public async Task<JsonResult<List<HAQDetails>>> GetAllForRole(string role)
        {
            List<HAQDetails> appList = new List<HAQDetails>();

            HAQDetailsRepository mRepo = new HAQDetailsRepository(HAQDetailsRepository.DEFAULT_TABLE_NAME, HAQDetailsRepository.DEFAULT_PART_KEY);
            appList = mRepo.GetAllForRole(role);

            return Json<List<HAQDetails>>(appList);
        }


        [HttpPost]
        public async Task<JsonResult<HAQDetails>> InsertHAQDetails(HAQDetails mApp)
        {
            HAQDetailsRepository mRepo = new HAQDetailsRepository(HAQDetailsRepository.DEFAULT_TABLE_NAME, HAQDetailsRepository.DEFAULT_PART_KEY);
            HAQDetails haqDetails;
            char[] delim = { ',' };
            if (mApp.Name != null && mApp.HATopic != null)
            {
                haqDetails= new HAQDetails(mApp.Name, mApp.HATopic, mApp.Result);
                if (mApp.roles != null)
                {
                    haqDetails.roles = mApp.roles;
                    haqDetails.AllowedRoles = mApp.roles.Split(delim);
                }
                mRepo.InsertHAQDetails(haqDetails);
                mApp = haqDetails;
            }

            return Json<HAQDetails>(mApp);
        }


        [HttpPost]
        public async Task<JsonResult<HAQDetails>> InsertHAQDetailsList(List<HAQDetails> mApps)
        {
            HAQDetailsRepository mRepo = new HAQDetailsRepository(HAQDetailsRepository.DEFAULT_TABLE_NAME, HAQDetailsRepository.DEFAULT_PART_KEY);
            HAQDetails haqDetails;
            char[] delim = { ',' };
            HAQDetails temp = new HAQDetails();
            string unique_id = "_"+DateTime.Now.Ticks.ToString();

            foreach (HAQDetails mApp in mApps)
            {
                if (mApp.Name != null && mApp.HATopic != null)
                {
                    haqDetails = new HAQDetails(mApp.Name+ unique_id, mApp.HATopic, mApp.Result);
                    if (mApp.roles != null)
                    {
                        haqDetails.roles = mApp.roles;
                        haqDetails.AllowedRoles = mApp.roles.Split(delim);
                    }
                    mRepo.InsertHAQDetails(haqDetails);
                    temp = haqDetails;
                }
            }

            return Json<HAQDetails>(temp);
        }

    }
}
