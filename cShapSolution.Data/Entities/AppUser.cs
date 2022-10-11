using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace cShapSolution.Data.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirsName { get; set; }
        public string LastName { get; set; }   
        public DateTime Dob { get; set; } //Ngày sinh

        public List<Cart> Carts { get; set; }
        public List<Order> orders { get; set; }
        public List<Transaction> Transactions { get; set; } 
    }
}
