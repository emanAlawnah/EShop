using EShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EShop.DAL.DTO.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }
        public string UserCreated { get; set; }

        public string MainImage { get; set; }
        public List<ProductTranslationResponse> Translations { get; set; }
    }
}
