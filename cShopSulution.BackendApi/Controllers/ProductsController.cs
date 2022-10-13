using cShapSolution.Data.Entities;
using cShopSolution.Application.Catalog.Products.Dtos.Manages;
using cShopSolution.Application.Catalog.Products.Dtos.Publics;
using cShopSolution.Application.Catalog.Products.InterfaceService;
using cShopSolution.ViewModels.Catalog.ProductImages;
using cShopSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace cShopSulution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManagedProductService _managedProductService;

        public ProductsController(IPublicProductService publicProductService, IManagedProductService managedProductService)
        {
            _publicProductService = publicProductService;
            _managedProductService = managedProductService;
        }

        #region Prducts Public

        //http://Localhost:post/product/?pageIndext=1&pageSize = 10
        [HttpGet("{LanguageId}")]
        public async Task<IActionResult> GetAllPaging(string LanguageId, [FromQuery] GetPublicProductPagingRequest request)
        {
            var products = await _publicProductService.GetAllByCategoryId(LanguageId, request);
            return Ok(products);
        }

        #endregion Prducts Public

        /// <summary>
        ///
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="LanguageId"></param>
        /// <returns></returns>

        #region Controler Manager

        //http://Localhost:post/product/id
        [HttpGet("{productId}/{LanguageId}")]
        public async Task<IActionResult> GetById(int productId, string LanguageId)
        {
            var products = await _managedProductService.GetById(productId, LanguageId);
            if (products == null)
            {
                return BadRequest("Not find Products");
            }
            else
            {
                return Ok(products);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequet request)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var ProductId = await _managedProductService.Create(request);
                if (ProductId == 0)
                {
                    return BadRequest();
                }
                else

                {
                    var products = await _managedProductService.GetById(ProductId, request.LanguageId);
                    return CreatedAtAction(nameof(GetById), new { id = ProductId }, products);
                }
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            else
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
        }

        [HttpDelete("{ProductId}")]
        public async Task<IActionResult> Delete(int ProductId)
        {
            var AffectedResult = await _managedProductService.Delete(ProductId);
            if (AffectedResult == 0)
            {
                return BadRequest();
            }
            else

            {
                return Ok();
            }
        }

        [HttpPatch("{ProductId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int ProductId, decimal newPrice)
        {
            var isSuccessfull = await _managedProductService.UpdatePrice(ProductId, newPrice);
            if (isSuccessfull == true)
            {
                return Ok();
            }
            else

            {
                return BadRequest();
            }
        }

        #endregion Controler Manager

        /// <summary>
        ///
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="request"></param>
        /// <returns></returns>

        #region Products Images

        [HttpPost("{productId}/images")]
        public async Task<IActionResult> CreateImage(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var imageId = await _managedProductService.AddImage(productId, request);
                if (imageId == 0)
                {
                    return BadRequest();
                }
                else
                {
                    var image = await _managedProductService.GetImageById(imageId);

                    return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
                }
            }
        }

        [HttpPut("{productId}/images/{imageId}")]
        [Authorize]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var result = await _managedProductService.UpdateImage(imageId, request);
                if (result == 0)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok();
                }
            }
        }

        [HttpGet("{productId}/images/{imnageId}")]
        public async Task<IActionResult> GetImageById(int productId, int imnageId)
        {
            var image = await _managedProductService.GetImageById(imnageId);
            if (image == null)
            {
                return BadRequest("Not find Products");
            }
            else
            {
                return Ok(image);
            }
        }

        #endregion Products Images
    }
}