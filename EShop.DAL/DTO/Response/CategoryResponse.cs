using EShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EShop.DAL.DTO.Response
{
    public class CategoryResponse
    {
        public int Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }
        public string UserCreated { get; set; }
        public List<CategoryTranslationResponce> Translations { get; set; }



    }
}
