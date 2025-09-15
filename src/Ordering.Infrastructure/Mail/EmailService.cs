using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;

namespace Ordering.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendEmail(Email email)
        {
            return true;
        }
    }
}
