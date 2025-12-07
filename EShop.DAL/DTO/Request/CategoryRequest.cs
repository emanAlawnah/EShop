using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.DTO.Request
{
    public class CategoryRequest
    {
        public List<CategoryTransilationRequest> Translations {  get; set; }
    }
}
