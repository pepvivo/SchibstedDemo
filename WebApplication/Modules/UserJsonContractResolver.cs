using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebApplication.Modules
{
    public class UserJsonContractResolver : DefaultContractResolver
    {

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);

            if (!prop.Readable)
            {
                var property = member as PropertyInfo;
                if (property != null)
                {
                    var hasPrivateGetter = property.GetGetMethod(true) != null;
                    prop.Readable = hasPrivateGetter;
                }
            }

            return prop;
        }
    }
}