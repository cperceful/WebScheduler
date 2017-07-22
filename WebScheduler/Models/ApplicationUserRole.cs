using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebScheduler.Models
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public override string UserId { get; set; }

    }
}
