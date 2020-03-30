using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    // Copy of the same as Paginated View Model.cs in Viewmodels of api microservice only different is the list of catalog item
    public class Catalog
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public long Count { get; set; }
        public List<CatalogItem> Data { get; set; }
    }
}
