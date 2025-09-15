using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services.Interfaces;

namespace Shopping.Aggregator.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public ShoppingController(ICatalogService catalogService, IBasketService basketService
            , IOrderService orderService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
            _orderService = orderService;
        }

        [HttpGet("{userName}", Name = "GetShopping")]
        [ProducesResponseType(typeof(ShoppingModel), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<ShoppingModel>> GetShopping(string userName)
        {
            var basket = await _basketService.GetBasket(userName);
            if(basket!=null)
                foreach (var item in basket.Items)
                {
                    var product = await _catalogService.GetCatalogById(item.ProductId);

                    item.ProductName = product.Name;
                    item.Price = product.Price;
                    item.Description = product.Description;
                    item.Summary = product.Summary;
                    item.ImageFile = product.ImageFile;
                }
            var orders = await _orderService.GetOrderByUserName(userName);

            var shoppingModel = new ShoppingModel
            {
                UserName = userName,
                BasketWithProduct = basket,
                Orders = orders
            };

            return Ok(shoppingModel);
        }

    }
}
