using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Dto
{
    public class ApiSettings
    {
        public string SecretKeyAuth { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string ClientIdAuth { get; set; }
        public string StoredProcedureGetEmployees { get; set; }
        public string StoredProcedureEditEmployees { get; set; }
        public string StoredProcedureInsertEmployees { get; set; }
        public string StoredProcedureLogin { get; set; }
        public string SqlConnection { get; set; }
    }
}
