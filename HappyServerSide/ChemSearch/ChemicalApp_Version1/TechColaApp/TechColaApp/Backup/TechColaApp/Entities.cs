using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechColaApp
{
    public class ChemicalsDet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int HealthHazard_Value { get; set; }
        public int FireHazard_Value { get; set; }
        public int ReactivityHazard_Value { get; set; }
        public int SpecialHazard_Value { get; set; }
    }

    public class PropertyDet
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }

    public class Chemical_Property
    {
        public int Id { get; set; }
        public int ChemId { get; set; }
        public int PropId { get; set; }
        public string Description { get; set; }
    }

    public class HealthHazard
    {
        public int Value { get; set; }
        public string Description { get; set; }
    }

    public class FireHazard
    {
        public int Value { get; set; }
        public string Description { get; set; }
    }

    public class ReactivityHazard
    {
        public int Value { get; set; }
        public string Description { get; set; }
    }

    public class SpecialHazard
    {
        public int Value { get; set; }
        public string Description { get; set; }
    }

}