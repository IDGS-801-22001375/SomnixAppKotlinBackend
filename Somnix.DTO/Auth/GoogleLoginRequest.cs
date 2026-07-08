using System;
using System.Collections.Generic;
using System.Text;

namespace Somnix.DTO.Auth
{
    public class GoogleLoginRequest
    {
        public string IdToken { get; set; } = string.Empty;
    }
}
