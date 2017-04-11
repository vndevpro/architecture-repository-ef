using System.Data.Entity.ModelConfiguration;
using GdNet.Domain.AppCommon;

namespace GdNet.Data.AppCommon.EF.Mappers
{
    public class AttachmentEntityConfiguration : EntityTypeConfiguration<Attachment>
    {
        public AttachmentEntityConfiguration()
        {
            // http://stackoverflow.com/questions/9613421/map-a-dictionary-in-entity-framework-code-first-approach
            Property(x => x.AttributesData).HasColumnType("xml");
        }
    }
}