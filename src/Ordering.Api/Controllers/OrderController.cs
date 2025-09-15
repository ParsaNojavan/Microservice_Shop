using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Contracts.Persistance;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersLists;

namespace Ordering.Api.Controllers
{
    [Route("api/v1/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        #region constructor
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public OrderController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        #endregion

        #region get all orders
        [HttpGet("getOrders/{userName}", Name = "GetOrders")]
        [ProducesResponseType(typeof(IEnumerable<OrdersDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrdersDto>>> GetOrderByUserName([FromRoute]string userName)
        {
           var query = new GetOrdersListQuery(userName);
            return Ok(await _mediator.Send(query));
        }
        #endregion

        #region checkout order
        [HttpPost("checkout",Name = "CheckoutOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CheckOutOrder([FromBody] CheckoutOrderCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        #endregion

        #region update order
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateOrder([FromBody]UpdateOrderCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
        #endregion

        #region delete order
        [HttpDelete("delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            await _mediator.Send(new IDeleteOrderCommand { Id = id });
            return NoContent();
        }
        #endregion

    }
}
