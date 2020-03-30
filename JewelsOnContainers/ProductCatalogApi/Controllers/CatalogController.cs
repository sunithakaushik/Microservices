using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductCatalogApi.ViewModels;
using ProductCatalogApi.Data;
using ProductCatalogApi.Domain;

namespace ProductCatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        // diff b/w readonly, const, static - moudule 13 - we are reading from the DB hence using the catalog context
        private readonly CatalogContext _context;
        private readonly IConfiguration _config;
        public CatalogController(CatalogContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // we need to paginate the data that is coming from the db onto the UI - module 13 8:10
        // api name is Items, it is a async , 3 ways to accept parameters -- from uri, from query, from body
        [HttpGet]
        [Route("[action]")]  // from query as parameters
      //  [Route("[action]/{pageIndex}/{pageSize}")] -- from uri
        public async Task<IActionResult> Items([FromQuery]int pageIndex = 0, [FromQuery]int pageSize = 6)
        {
            // LINQ query to get total number of records in the catalog items model
            // we are having long here as it might be millions of items in the db, rather than int
            // SELECT COUNT * FROM CATALOG_ITEMS table is the eq of below
            var itemsCount = await _context.CatalogItems.LongCountAsync();  

            var items = await _context.CatalogItems
                                                .Skip(pageIndex * pageSize)
                                                .Take(pageSize)
                                                .ToListAsync();
            items = ChangePictureUrl(items);
            //module 13 52.36 
            var model = new PaginatedItemsViewModel<CatalogItem>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Count = itemsCount,
                Data = items
            };
            return Ok(model);
        }

        private List<CatalogItem> ChangePictureUrl(List<CatalogItem> items)
        {
            items.ForEach(
                            c => c.PictureUrl =
                            c.PictureUrl.Replace("http://externalcatalogbaseurltobereplaced", _config["ExternalCatalogBaseUrl"])
                        );
            return items;
        }
 
    }
}