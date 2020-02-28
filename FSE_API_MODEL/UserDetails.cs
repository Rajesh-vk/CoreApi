using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSE_API_MODEL
{
    public class UserDetails
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string EmailId { get; set; }

        public int UserRoleId { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
