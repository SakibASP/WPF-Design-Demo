using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SocialBladeDashboard.Models
{
    public class Members 
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Mobile { get; set; }
        public string? Mobile_2 { get; set; }
        public string? Blood_Group { get; set; }
        public double? Amount { get; set; }
        public int? TypeId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
