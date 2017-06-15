using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppWpf.Models
{
    public class RequestBase
    {
        public string CallerIdentifier { get; set; }
        public string DeviceIdentifier { get; set; }

        public virtual FormUrlEncodedContent GetFormData()
        {
            List<KeyValuePair<string, string>> formData = new List<KeyValuePair<string, string>>();
            this.GetType().GetProperties().ToList().ForEach(
                p => formData.Add(new KeyValuePair<string, string>(p.Name, p.GetValue(this).ToString())));

            FormUrlEncodedContent result = new FormUrlEncodedContent(formData);

            return result;
        }
    }
}