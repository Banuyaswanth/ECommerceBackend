﻿using ECommerce.DbContext;
using ECommerce.Models.DTOs;
using ECommerce.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    /// <summary>
    /// This API handles all the logic related to Product filtering
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly IProductService productRepositoryService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productRepositoryService"></param>
        public FilterController(IProductService productRepositoryService)
        {
            this.productRepositoryService = productRepositoryService;
        }

        /// <summary>
        /// Gets all the products after filtering with the provided conditions
        /// </summary>
        /// <param name="categoryId">Guid</param>
        /// <param name="filterConditions">FilterProductsQueryParametersDto Object</param>
        /// <param name="sortConditions">SortProductsDto object</param>
        /// <returns>Returns 200Ok response with List(PaginatedFilteredResults) if products are found with the given conditions otherwise 404NotFound</returns>
        /// <response code="200">Returns List of PaginatedFilterResults if products are found with filter conditions</response>
        /// <response code="404">Returns Not found when no products are found with the filter conditions</response>
        /// <response code="500">Returns Internal Server Error with Message when an exception occurs</response>
        [HttpGet("{categoryId}")]
        [ProducesResponseType(typeof(PaginatedFilterResults), 200)]
        public async Task<IActionResult> GetProductCards([FromRoute] Guid categoryId ,[FromQuery] FilterProductsQueryParametersDto filterConditions, [FromQuery] SortProductsDto sortConditions)
        {
            try
            {
                var productItems = await productRepositoryService.FilterProducts(categoryId, filterConditions, sortConditions);
                return productItems.TotalFilterResults > 0 ? Ok(productItems) : NotFound();
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
