using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity_Models
{
    public class ConfigurationDetail : IAuditEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string StringValue { get; set; }
        public string AdditionalStringValue { get; set; }
        public double? NumericValue { get; set; }
        public double? AdditionalNumericValue { get; set; }
        public int ConfigurationId { get; set; }
        [JsonIgnore]
        [ForeignKey("ConfigurationId")]
        public Configuration Configuration { get; set; }
        public int? ParentConfigurationDetailId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Enabled { get; set; }
    }
}
