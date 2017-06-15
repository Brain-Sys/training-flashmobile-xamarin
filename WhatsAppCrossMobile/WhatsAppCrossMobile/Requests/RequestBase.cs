using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppCrossMobile.Requests
{
    public class RequestBase
    {
        public string CallerIdentifier { get; set; }
        public string DeviceIdentifier { get; set; }

        public virtual FormUrlEncodedContent GetFormData()
        {
            List<KeyValuePair<string, string>> formData = new List<KeyValuePair<string, string>>();

            var ti = this.GetType().GetRuntimeProperties();

            foreach (PropertyInfo pi in ti)
            {
                var value = pi.GetValue(this);

                if (value != null)
                {
                    formData.Add(new KeyValuePair<string, string>(pi.Name, value.ToString()));
                }
                else
                {
                    formData.Add(new KeyValuePair<string, string>(pi.Name, string.Empty));
                }
            }

            FormUrlEncodedContent result = new FormUrlEncodedContent(formData);

            return result;
        }
    }
}