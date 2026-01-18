using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public DateTime CreateAt { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("CreatedBy")]
        public AplecationUser User { get; set; }



    }
}
