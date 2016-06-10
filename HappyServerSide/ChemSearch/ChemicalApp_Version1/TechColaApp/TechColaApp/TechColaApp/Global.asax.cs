using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml;
using System.IO;
using System.Reflection;

namespace TechColaApp
{
    public class Global : System.Web.HttpApplication
    {
        public List<ChemicalsDet> details = null;
        public List<PropertyDet> property = null;
        public List<Chemical_Property> chem_property = null;
        public List<HealthHazard> healthHazard=null;
        public List<FireHazard> fireHazard = null;
        public List<ReactivityHazard> reactivityHazard = null;
        public List<SpecialHazard> specialHazard = null;
        public List<string> checkAvailability = null;

        void Application_Start(object sender, EventArgs e)
        {
            details = new List<ChemicalsDet>();
            property = new List<PropertyDet>();
            chem_property = new List<Chemical_Property>();
            healthHazard = new List<HealthHazard>();
            fireHazard = new List<FireHazard>();
            reactivityHazard = new List<ReactivityHazard>();
            specialHazard = new List<SpecialHazard>();
             string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase), "ChemicalsFormatted.xml");
            //string path = "ChemicalsFormatted.xml";
            //XmlTextReader reader = new XmlTextReader(@"D:\Code\TechColaApp\ChemicalsFormatted.xml");
            XmlTextReader reader = new XmlTextReader(path);
            reader.WhitespaceHandling = WhitespaceHandling.None;
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();

            //Retrieve Chemical details

            XmlNodeList list = doc.GetElementsByTagName("Chemicals");
            for (int i = 0; i < list.Count; i++)
            {
                XmlNode node = list.Item(i);
                XmlNodeList innerNodes = node.ChildNodes;
                //MenuItem itemX = new MenuItem();
                ChemicalsDet data1 = new ChemicalsDet();
                for (int j = 0; j < innerNodes.Count; j++)
                {
                    XmlNode innernode = innerNodes.Item(j);
                    //ChemicalsDet data1 = new ChemicalsDet();
                    //if(j!=0)
                    //innernode = innernode.PreviousSibling;
                    if (innernode.Name == "Id")
                    {
                        data1.Id = Convert.ToInt32(innernode.InnerText);
                    }
                    //innernode = innernode.NextSibling;
                    if (innernode.Name == "Name")
                    {
                        data1.Name = innernode.InnerText;
                    }
                    //innernode = innernode.NextSibling;
                    if (innernode.Name == "URL")
                    {
                        data1.URL = innernode.InnerText;
                    }

                    if (innernode.Name == "HealthHazard_Value")
                    {
                        data1.HealthHazard_Value = Convert.ToInt32(innernode.InnerText);
                    }

                    if (innernode.Name == "FireHazard_Value")
                    {
                        data1.FireHazard_Value = Convert.ToInt32(innernode.InnerText);
                    }

                    if (innernode.Name == "ReactivityHazard_Value")
                    {
                        data1.ReactivityHazard_Value = Convert.ToInt32(innernode.InnerText);
                    }

                    if (innernode.Name == "SpecialHazard_Value")
                    {
                        data1.SpecialHazard_Value = Convert.ToInt32(innernode.InnerText);
                    }

                    //if (data1.Name != null || data1.URL != null)
                    //{
                    //    details.Add(data1);
                    //}
                }
                details.Add(data1);
            }
            this.Application["chemicalDetails"] = details;
            //Retrieve Propertty details

            XmlNodeList list1 = doc.GetElementsByTagName("Propertty");
            for (int i = 0; i < list1.Count; i++)
            {
                XmlNode node = list1.Item(i);
                XmlNodeList innerNodes = node.ChildNodes;
                //MenuItem itemX = new MenuItem();
                PropertyDet data2 = new PropertyDet();
                for (int j = 0; j < innerNodes.Count; j++)
                {
                    XmlNode innernode = innerNodes.Item(j);
                    //ChemicalsDet data1 = new ChemicalsDet();
                    //if(j!=0)
                    //innernode = innernode.PreviousSibling;
                    if (innernode.Name == "Id")
                    {
                        data2.Id = Convert.ToInt32(innernode.InnerText);
                    }
                    //innernode = innernode.NextSibling;
                    if (innernode.Name == "Type")
                    {
                        data2.Type = innernode.InnerText;
                    }
                }
                property.Add(data2);
            }
            this.Application["propertyDetails"] = property;

            //Retrieve ChemicalProperties details

            XmlNodeList list2 = doc.GetElementsByTagName("ChemicalProperties");
            for (int i = 0; i < list2.Count; i++)
            {
                XmlNode node = list2.Item(i);
                XmlNodeList innerNodes = node.ChildNodes;
                //MenuItem itemX = new MenuItem();
                Chemical_Property data3 = new Chemical_Property();
                for (int j = 0; j < innerNodes.Count; j++)
                {
                    XmlNode innernode = innerNodes.Item(j);
                    //ChemicalsDet data1 = new ChemicalsDet();
                    //if(j!=0)
                    //innernode = innernode.PreviousSibling;
                    if (innernode.Name == "Id")
                    {
                        data3.Id = Convert.ToInt32(innernode.InnerText);
                    }
                    //innernode = innernode.NextSibling;
                    if (innernode.Name == "Chemical_Id")
                    {
                        data3.ChemId = Convert.ToInt32(innernode.InnerText);
                    }

                    if (innernode.Name == "Property_Id")
                    {
                        data3.PropId = Convert.ToInt32(innernode.InnerText);
                    }

                    if (innernode.Name == "Description")
                    {
                        data3.Description = innernode.InnerText;
                    }
                }
                chem_property.Add(data3);
            }

            this.Application["chem_propertyDetails"] = chem_property;

            //Retrieve HealthHazards details

            XmlNodeList list3 = doc.GetElementsByTagName("HealthHazards");
            for (int i = 0; i < list3.Count; i++)
            {
                XmlNode node = list3.Item(i);
                XmlNodeList innerNodes = node.ChildNodes;
                //MenuItem itemX = new MenuItem();
                HealthHazard data4 = new HealthHazard();
                for (int j = 0; j < innerNodes.Count; j++)
                {
                    XmlNode innernode = innerNodes.Item(j);
                    //ChemicalsDet data1 = new ChemicalsDet();
                    //if(j!=0)
                    //innernode = innernode.PreviousSibling;
                    if (innernode.Name == "Value")
                    {
                        data4.Value = Convert.ToInt32(innernode.InnerText);
                    }
                    //innernode = innernode.NextSibling;
                    if (innernode.Name == "Description")
                    {
                        data4.Description = innernode.InnerText;
                    }
                }
                healthHazard.Add(data4);
            }

            this.Application["healthHazardDetails"] = healthHazard;

            //Retrieve FireHazards details

            XmlNodeList list4 = doc.GetElementsByTagName("FireHazards");
            for (int i = 0; i < list4.Count; i++)
            {
                XmlNode node = list4.Item(i);
                XmlNodeList innerNodes = node.ChildNodes;
                //MenuItem itemX = new MenuItem();
                FireHazard data5 = new FireHazard();
                for (int j = 0; j < innerNodes.Count; j++)
                {
                    XmlNode innernode = innerNodes.Item(j);
                    //ChemicalsDet data1 = new ChemicalsDet();
                    //if(j!=0)
                    //innernode = innernode.PreviousSibling;
                    if (innernode.Name == "Value")
                    {
                        data5.Value = Convert.ToInt32(innernode.InnerText);
                    }
                    //innernode = innernode.NextSibling;
                    if (innernode.Name == "Description")
                    {
                        data5.Description = innernode.InnerText;
                    }
                }
                fireHazard.Add(data5);
            }

            this.Application["fireHazardDetails"] = fireHazard;

            //Retrieve ReactivityHazards details

            XmlNodeList list5 = doc.GetElementsByTagName("ReactivityHazards");
            for (int i = 0; i < list5.Count; i++)
            {
                XmlNode node = list5.Item(i);
                XmlNodeList innerNodes = node.ChildNodes;
                //MenuItem itemX = new MenuItem();
                ReactivityHazard data6 = new ReactivityHazard();
                for (int j = 0; j < innerNodes.Count; j++)
                {
                    XmlNode innernode = innerNodes.Item(j);
                    //ChemicalsDet data1 = new ChemicalsDet();
                    //if(j!=0)
                    //innernode = innernode.PreviousSibling;
                    if (innernode.Name == "Value")
                    {
                        data6.Value = Convert.ToInt32(innernode.InnerText);
                    }
                    //innernode = innernode.NextSibling;
                    if (innernode.Name == "Description")
                    {
                        data6.Description = innernode.InnerText;
                    }
                }
                reactivityHazard.Add(data6);
            }

            this.Application["reactivityHazardDetails"] = reactivityHazard;

            //Retrieve SpecialHazards details

            XmlNodeList list6 = doc.GetElementsByTagName("SpecialHazards");
            for (int i = 0; i < list6.Count; i++)
            {
                XmlNode node = list6.Item(i);
                XmlNodeList innerNodes = node.ChildNodes;
                //MenuItem itemX = new MenuItem();
                SpecialHazard data7 = new SpecialHazard();
                for (int j = 0; j < innerNodes.Count; j++)
                {
                    XmlNode innernode = innerNodes.Item(j);
                    //ChemicalsDet data1 = new ChemicalsDet();
                    //if(j!=0)
                    //innernode = innernode.PreviousSibling;
                    if (innernode.Name == "Value")
                    {
                        data7.Value = Convert.ToInt32(innernode.InnerText);
                    }
                    //innernode = innernode.NextSibling;
                    if (innernode.Name == "Description")
                    {
                        data7.Description = innernode.InnerText;
                    }
                }
                specialHazard.Add(data7);
            }

            this.Application["specialHazardDetails"] = specialHazard;
        }

        void Application_End(object sender, EventArgs e)
        {
            details = null;
            property = null;
            chem_property = null;
            healthHazard = null;
            fireHazard = null;
            reactivityHazard = null;
            specialHazard = null;
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            checkAvailability = new List<string>();
            this.Session["checkAvailability"] = checkAvailability;
        }

        void Session_End(object sender, EventArgs e)
        {
            checkAvailability = null;
        }

    }
}
