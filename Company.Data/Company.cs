using Company.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Data
{
    public class Company : IEntity
    {
        public int Id { get; set; }
        [MaxLength(100), Required]
        public string CompanyName { get; set; }
    }
}
