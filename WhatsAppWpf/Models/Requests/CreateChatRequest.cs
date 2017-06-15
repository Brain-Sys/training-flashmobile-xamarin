using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppWpf.Models
{
    public class CreateChatRequest : RequestBase
    {
        public string[] CallerIndentifiers { get; set; }

        public override FormUrlEncodedContent GetFormData()
        {
            List<KeyValuePair<string, string>> formData = new List<KeyValuePair<string, string>>();
            formData.Add(new KeyValuePair<string, string>("CallerIdentifier", this.CallerIdentifier));
            formData.Add(new KeyValuePair<string, string>("DeviceIdentifier", this.DeviceIdentifier));

            foreach (var id in this.CallerIndentifiers)
            {
                formData.Add(new KeyValuePair<string, string>("CallerIndentifiers[]", id));
            }

            FormUrlEncodedContent result = new FormUrlEncodedContent(formData);

            return result;
        }
    }
}