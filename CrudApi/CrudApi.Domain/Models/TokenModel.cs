using System;
using System.Collections.Generic;
using System.Text;

namespace CrudApi.Domain.Models
{
    public class TokenModel
    {
        public string Token { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
