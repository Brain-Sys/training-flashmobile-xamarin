using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WhatsAppCrossMobile.Responses;

namespace WhatsAppCrossMobile.Helpers
{
    public class MessagesConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            var ti = objectType.GetTypeInfo();
            return typeof(MessageBase).GetTypeInfo().IsAssignableFrom(ti);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject item = JObject.Load(reader);

            if (item.SelectToken("latitude") != null)
            {
                return item.ToObject<LocationMessage>();
            }
            else
            {
                return item.ToObject<TextMessage>();
            }

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

        }
    }
}