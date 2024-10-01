
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System;

namespace BusinessLogic.Entities
{
    public class CartProduct
    {
        public long Id { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
