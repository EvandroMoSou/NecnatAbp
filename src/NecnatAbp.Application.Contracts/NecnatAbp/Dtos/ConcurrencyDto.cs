using Volo.Abp.Application.Dtos;

namespace NecnatAbp.Dtos
{
    public abstract class ConcurrencyDto
    {
        public string? ConcurrencyStamp { get; set; }
    }

    public abstract class ConcurrencyEntityDto : EntityDto
    {
        public string? ConcurrencyStamp { get; set; }
    }

    public abstract class ConcurrencyEntityDto<TPrimaryKey> : EntityDto<TPrimaryKey>
    {
        public string? ConcurrencyStamp { get; set; }
    }

    public abstract class ConcurrencyCreationAuditedEntityDto : CreationAuditedEntityDto
    {
        public string? ConcurrencyStamp { get; set; }
    }

    public abstract class ConcurrencyCreationAuditedEntityDto<TPrimaryKey> : CreationAuditedEntityDto<TPrimaryKey>
    {
        public string? ConcurrencyStamp { get; set; }
    }

    public abstract class ConcurrencyAuditedEntityDto : AuditedEntityDto
    {
        public string? ConcurrencyStamp { get; set; }
    }

    public abstract class ConcurrencyAuditedEntityDto<TPrimaryKey> : AuditedEntityDto<TPrimaryKey>
    {
        public string? ConcurrencyStamp { get; set; }
    }
}
