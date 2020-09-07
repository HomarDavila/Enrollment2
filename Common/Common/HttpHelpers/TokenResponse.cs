using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.HttpHelpers
{
    public class TokenResponse
    {
        #region Properties
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "HasDefaultCredentials")]
        public string HasDefaultCredentials { get; set; }

        [JsonProperty(PropertyName = ".issued")]
        public DateTime Issued { get; set; }

        [JsonProperty(PropertyName = ".expires")]
        public DateTime Expires { get; set; }

        [JsonProperty(PropertyName = "CreatedBy")]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "error_description")]
        public string ErrorDescription { get; set; }
        #endregion
    }
    public class TokenErrorResponse
    {
        #region Properties
        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }

        [JsonProperty(PropertyName = "error_description")]
        public string ErrorDescription { get; set; }
        #endregion
    }
}
