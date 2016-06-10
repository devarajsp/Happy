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
    public class HappyTweetsController : ApiController
    {
        [HttpGet]
        public async Task<JsonResult<List<HappyTweet>>> GetAll()
        {
            List<HappyTweet> appList = new List<HappyTweet>();

            HappyTweetsRepository mRepo = new HappyTweetsRepository(HappyTweetsRepository.DEFAULT_TABLE_NAME, HappyTweetsRepository.DEFAULT_PART_KEY);
            appList = mRepo.GetAll();

            return Json<List<HappyTweet>>(appList);
        }


        [HttpGet]
        public async Task<JsonResult<List<HappyTweet>>> GetAllForRole(string role)
        {
            List<HappyTweet> appList = new List<HappyTweet>();

            HappyTweetsRepository mRepo = new HappyTweetsRepository(HappyTweetsRepository.DEFAULT_TABLE_NAME, HappyTweetsRepository.DEFAULT_PART_KEY);
            appList = mRepo.GetAllForRole(role);

            return Json<List<HappyTweet>>(appList);
        }


        [HttpPost]
        public async Task<JsonResult<HappyTweet>> InsertHappyTweet(HappyTweet mApp)
        {
            HappyTweetsRepository mRepo = new HappyTweetsRepository(HappyTweetsRepository.DEFAULT_TABLE_NAME, HappyTweetsRepository.DEFAULT_PART_KEY);
            HappyTweet HappyTweet;
            char[] delim = { ',' };
            if (mApp.Name != null && mApp.TweetMessage != null)
            {
                HappyTweet = new HappyTweet(mApp.Name, mApp.TweetMessage, mApp.Description);
                if (mApp.roles != null)
                {
                    HappyTweet.roles = mApp.roles;
                    HappyTweet.AllowedRoles = mApp.roles.Split(delim);
                }
                mRepo.InsertHappyTweetDetails(HappyTweet);
                mApp = HappyTweet;
            }

            return Json<HappyTweet>(mApp);
        }

    }
}
