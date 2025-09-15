using System.Net;
using Discount.api.Entities;
using Discount.api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discount.api.Controllers
{
    [Route("api/v1/discount")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        #region constructor
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        #endregion

        #region get coupon
        [HttpGet("getcoupon/{productName}",Name ="GetCoupon")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetCoupon(string productName)
        {
            var coupon = await _discountRepository.GetDiscount(productName);
            return Ok(coupon);
        }
        #endregion

        #region create coupon
        [HttpPost("create")]
        public async Task<ActionResult> CreateCoupon([FromBody]Coupon coupon)
        {
            await _discountRepository.CreateDiscount(coupon);
            return CreatedAtRoute("GetCoupon",new {ProductName = coupon.ProductName},coupon);
        }
        #endregion

        #region update discount
        [HttpPut("edit")]
        [ProducesResponseType(typeof(bool),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
        {
            return Ok(await _discountRepository
                .UpdateDiscount(coupon));
        }
        #endregion

        #region delete discount
        [HttpDelete("delete/{productName}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> UpdateDiscount(string productName)
        {
            return Ok(await _discountRepository
                .DeleteDiscount(productName));
        }
        #endregion
    }
}
