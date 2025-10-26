using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Infrastructure.Data.Models
{
    public class Security
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
