using GdNet.Domain.AppCommon;
using System.Data.Entity;

namespace GdNet.Data.AppCommon.EF
{
    public interface IAttachmentDataContext
    {
        IDbSet<Attachment> Attachments { get; set; }

        int SaveChanges();
    }
}