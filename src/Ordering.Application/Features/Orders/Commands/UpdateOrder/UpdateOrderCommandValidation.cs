using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidation : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidation()
        {
            RuleFor(a => a.UserName)
                .NotEmpty().WithMessage("Username is required")
                .MaximumLength(50).WithMessage("UserName must not exceed 50 chars");

            RuleFor(a => a.EmailAddress)
                .NotEmpty().WithMessage("Email is required");

            RuleFor(a => a.TotalPrice)
                .NotEmpty().WithMessage("TotalPrice is required")
                .GreaterThan(0).WithMessage("TotalPrice should be greater than 0");
        }
    }
}
