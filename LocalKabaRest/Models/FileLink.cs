using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace LocalKabaRest.Models
{
    public class FileLink
    {
        [JsonProperty(PropertyName = "FileLink")]
        public string Link;

        public string FileType;
    
    }
}