using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.DTO.Request
{
    public class ProductTransilationRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }

    }
}
