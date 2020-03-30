using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    // copied from domain in api of microservice
    public class CatalogItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        //Instead of hardcoding the image file name we give the url string where the pictures are stored, instead of enum
        public string PictureUrl { get; set; }
        //Foreign keys for Catalog Type and Catalog Brand Module 9
        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }

        //Composition - has - A relationship (29th Feb - Module 9)
        // Navigational property is done by adding virtual keyword
        // this does not take physical space, it will navigate
        public string CatalogType { get; set; }
        public string CatalogBrand { get; set; }
    }
}
