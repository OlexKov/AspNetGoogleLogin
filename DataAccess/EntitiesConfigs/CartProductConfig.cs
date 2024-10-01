using BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.EntitiesConfigs
{
    public class CartProductConfig : IEntityTypeConfiguration<CartProduct>
    {
        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Product).WithMany(x => x.InUsersCart);
            builder.HasOne(x => x.User).WithMany(x => x.Cart);
            builder.ToTable("tbl_cart_product");
        }
    }
}
