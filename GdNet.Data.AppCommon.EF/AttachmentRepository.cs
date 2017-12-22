using GdNet.Data.EF;
using GdNet.Data.EF.Strategies;
using GdNet.Domain.AppCommon;
using System;
using System.Data.Entity;

namespace GdNet.Data.AppCommon.EF
{
    public class AttachmentRepository : EfRepositoryBase<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(IDbSet<Attachment> entities)
            : base(entities)
        {
        }

        public AttachmentRepository(IDbSet<Attachment> entities, ISavingStrategy savingStrategy,
            IDeletionStrategy<Attachment> deletionStrategy, IFilterStrategy<Attachment, Guid> filterStrategy)
            : base(entities, savingStrategy, deletionStrategy, filterStrategy)
        {
        }
    }
}
