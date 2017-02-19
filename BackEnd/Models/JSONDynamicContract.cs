using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BackEnd.Models
{
    public class JSONDynamicContract : DefaultContractResolver
    {
        private List<string> propertiesToInclude;
        private List<string> propertiesNotToInclude;

        public JSONDynamicContract()
        {
            propertiesToInclude = new List<string>();
            propertiesNotToInclude = new List<string>();
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);
            List<JsonProperty> selectedProperties = new List<JsonProperty>();

            // only serializer properties that start with the specified character
            if (propertiesToInclude.Count == 0)
            {
                foreach (string propertyName in propertiesNotToInclude)
                    foreach (JsonProperty property in properties)
                        if (property.PropertyName == propertyName)
                            properties.Remove(property);
            }
            if (propertiesNotToInclude.Count == 0)
            {
                foreach (string propertyName in propertiesToInclude)
                    foreach (JsonProperty property in properties)
                        if (property.PropertyName == propertyName)
                            selectedProperties.Add(property);
                properties = selectedProperties;
            }

            //changing property name InverseParent into Children. needed for frontend model
            JsonProperty ChildrenProperty = properties.FirstOrDefault(p => p.PropertyName.Contains("InverseParent") == true);
            if (ChildrenProperty != null)
                ChildrenProperty.PropertyName = "children";
            return properties;
        }

        public void IncludeProperties(List<string> properties)
        {
            propertiesToInclude = properties;
        }

        public void ExcludeProperties(List<string> properties)
        {
            propertiesNotToInclude = properties;
        }
    }
}
