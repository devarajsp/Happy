using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Xml;
using System.Web.Services;
using System.Text;

namespace TechColaApp
{
    public partial class Samplesol : System.Web.UI.Page
    {
        public static List<ChemicalsDet> chemdetails = null;
        public static List<PropertyDet> property = null;
        public static List<Chemical_Property> chemicalproperty = null;
        public static List<HealthHazard> healthhazard = null;
        public static List<FireHazard> firehazard = null;
        public static List<ReactivityHazard> reactivityhazard = null;
        public static List<SpecialHazard> specialhazard = null;
        public static List<string> checklist = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ViewState["lblChemicalName"] = "";
                ViewState["stringssss"] = "";
                chemdetails = Application["chemicalDetails"] as List<ChemicalsDet>;
                property = Application["propertyDetails"] as List<PropertyDet>;
                chemicalproperty = Application["chem_propertyDetails"] as List<Chemical_Property>;
                healthhazard = Application["healthHazardDetails"] as List<HealthHazard>;
                firehazard = Application["fireHazardDetails"] as List<FireHazard>;
                reactivityhazard = Application["reactivityHazardDetails"] as List<ReactivityHazard>;
                specialhazard = Application["specialHazardDetails"] as List<SpecialHazard>;
                checklist = new List<string>();
            }
        }

        [WebMethod]
        public static void clearResults()
        {
            foreach (var item in checklist)
            {
                checklist.Remove(item);
            }
        }

        [WebMethod]
        public static string getChemicalName(string searchterm)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(chemdetails.Where(e => e.Name.ToLower().StartsWith(searchterm.ToLower())));
        }

        [WebMethod]
        public static string getChemicalData(string id)
        {
            if (id != "" && CheckAvailabilty(id))
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, string> data = new Dictionary<string, string>();
                var query = (from abc in chemicalproperty
                             join pro in property
                             on abc.PropId equals pro.Id
                             where abc.ChemId == Convert.ToInt32(id)
                             select new
                             {
                                 Property = pro.Type,
                                 Description = abc.Description
                             }).ToList();
                foreach (var item in query)
                {
                    data.Add(item.Property, item.Description);
                }
                data.Add("Health Hazard", (from che in chemdetails
                                           join hea in healthhazard
                                           on che.HealthHazard_Value equals hea.Value
                                           where che.Id == Convert.ToInt32(id)
                                           select hea.Description).First().ToString());
                data.Add("Fire Hazard", (from che in chemdetails
                                         join fir in firehazard
                                         on che.FireHazard_Value equals fir.Value
                                         where che.Id == Convert.ToInt32(id)
                                         select fir.Description).First().ToString());
                data.Add("Reactivity Hazard", (from che in chemdetails
                                               join rea in reactivityhazard
                                               on che.ReactivityHazard_Value equals rea.Value
                                               where che.Id == Convert.ToInt32(id)
                                               select rea.Description).First().ToString());
                data.Add("Special Hazard", (from che in chemdetails
                                            join spe in specialhazard
                                            on che.SpecialHazard_Value equals spe.Value
                                            where che.Id == Convert.ToInt32(id)
                                            select spe.Description).First().ToString());
                data.Add("Health Hazard Code", (from che in chemdetails
                                                where che.Id == Convert.ToInt32(id)
                                                select che.HealthHazard_Value).First().ToString());
                data.Add("Fire Hazard Code", (from che in chemdetails
                                              where che.Id == Convert.ToInt32(id)
                                              select che.FireHazard_Value).First().ToString());
                data.Add("Reactivity Hazard Code", (from che in chemdetails
                                                    where che.Id == Convert.ToInt32(id)
                                                    select che.ReactivityHazard_Value).First().ToString());
                data.Add("Special Hazard Code", (from che in chemdetails
                                                 where che.Id == Convert.ToInt32(id)
                                                 select che.SpecialHazard_Value).First().ToString());
                data.Add("URL", (from che in chemdetails
                                 where che.Id == Convert.ToInt32(id)
                                 select che.URL).First().ToString());
                return jss.Serialize(data);
            }
            return "";
        }

        protected static bool CheckAvailabilty(string id)
        {
            if (checklist.Contains(id))
                return false;
            else
            {
                checklist.Add(id);
                return true;
            }
        }

    }

}