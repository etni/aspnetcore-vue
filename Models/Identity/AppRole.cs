using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcore_vue.Models.Identity
{
    public class AppRole : IdentityRole<int>
    {
        public int Description { get; set; }
    }
}
