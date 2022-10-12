using cShapSolution.Data.Entities;
using cShopSolution.Application.Catalog.Products.Dtos.Manages;
using cShopSolution.Application.Catalog.Products.Dtos.Publics;
using cShopSolution.Application.Catalog.Products.InterfaceService;
using cShopSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace cShopSulution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManagedProductService _managedProductService;
        public ProductController(IPublicProductService publicProductService, IManagedProductService managedProductService)
        {
            _publicProductService = publicProductService;
            _managedProductService = managedProductService;

        }   
        #region Controllor Public
        //http://Localhost:post/product
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string LanguageId)
        {
            var products = await _publicProductService.GetAll(LanguageId);
            return Ok(products);
        }

        //http://Localhost:post/product/public-paging
        [HttpGet("public-paging")]
        public async Task<IActionResult> Get([FromQuery] GetPublicProductPagingRequest request)
        {
            var products = await _publicProductService.GetAllByCategoryId(request);
            return Ok(products);
        }
        #endregion

        #region Controler Manager

        //http://Localhost:post/product/id
        [HttpGet("{productId}/{LanguageId}")]
        public async Task<IActionResult> GetById(int productId, string LanguageId)
        {
            var products = await _managedProductService.GetById(productId, LanguageId);
            if (products == null )
            {
                return BadRequest("Not find Products");
            }
            else
            {
                return Ok(products);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ProductCreateRequet request)
        {
            var ProductId = await _managedProductService.Create(request);
            if (ProductId == 0)
            {
                return BadRequest();
            }
            else
                
            {
                var products = await _managedProductService.GetById(ProductId, request.LanguageId);
                return CreatedAtAction(nameof(GetById), new {id = ProductId }, products);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            var AffectedResult = await _managedProductService.Update(request);
            if (AffectedResult == 0)
            {
                return BadRequest();
            }
            else

            {
                
                return Ok();
            }
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var AffectedResult = await _managedProductService.Delete(Id);
            if (AffectedResult == 0)
            {
                return BadRequest();
            }
            else

            {

                return Ok();
            }
        }


        [HttpPut("price/{newPrice}")]
        public async Task<IActionResult> UpdatePrice([FromQuery]int id, decimal newPrice)
        {
            var isSuccessfull = await _managedProductService.UpdatePrice(id, newPrice);
            if (isSuccessfull == true)
            {
                return BadRequest();
            }
            else

            {

                return Ok();
            }
        }
        #endregion
    }
}
