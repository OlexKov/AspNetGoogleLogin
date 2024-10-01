using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Entities;

namespace DataAccess.EntitiesConfigs
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x=>x.Products).WithOne(x => x.Category);
            builder.ToTable("tbl_categories");
        }
    }
}
