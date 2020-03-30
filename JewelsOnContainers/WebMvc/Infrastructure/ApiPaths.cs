﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Infrastructure
{
    public class ApiPaths
    {
        public static class Catalog
        {
            public static string GetAllCatalogItems(string baseUri, int page, int take)
            {
                return $"{baseUri}items?pageIndex={page}&pageSize={take}";
            }
        }
        
        public static  class Basket
        {

        }
    }
}
