using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Employee.Dto
{
    public class JwtDto
    {
        public JwtDto()
        {
            Token_Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Token_Id { get; set; }
        public string EndPointName { get; set; }
        public string AccessToken { get; set; }
        public DateTime Expires { get; set; }
    }
}
