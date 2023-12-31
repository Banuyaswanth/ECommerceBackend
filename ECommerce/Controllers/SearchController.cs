﻿using ECommerce.DbContext;
using ECommerce.Models.Domain;
using ECommerce.Models.DTOs;
using ECommerce.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ECommerce.Controllers
{
    /// <summary>
    /// This API Controller handles all the logic related to the Product search
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IProductService productRepositoryService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productRepositoryService"></param>
        public SearchController(IProductService productRepositoryService)
        {
            this.productRepositoryService = productRepositoryService;
        }

        /// <summary>
        /// Searches the productItems in the database based on the search string provided in the query parameters
        /// </summary>
        /// <param name="sortConditions">SortProductsDto object</param>
        /// <param name="search">string</param>
        /// <param name="page">int</param>
        /// <returns>Returns 200Ok response with PaginatedSearchResultsDto if the products exist which contain the search string otherwise 404NotFound</returns>
        /// <response code="200">Returns List of PaginatedSearchResultsDto if products are found with search query</response>
        /// <response code="404">Returns Not found when no products are found with the search query</response>
        /// <response code="500">Returns Internal Server Error with Message when an exception occurs</response>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedSearchResultsDto), 200)]
        public async Task<IActionResult> SearchProducts([FromQuery] SortProductsDto sortConditions, [FromQuery] string? search = null, [FromQuery] int page = 1)
        {
            try
            {
                var searchResults = !string.IsNullOrEmpty(search) ? await productRepositoryService.SearchProductItem(search, page, sortConditions) : new PaginatedSearchResultsDto();
                return searchResults.TotalSearchResults > 0 ? Ok(searchResults) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (new
                {
                    ex.Message
                }));
            }
        }
    }
}
