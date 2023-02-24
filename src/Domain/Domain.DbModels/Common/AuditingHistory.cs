namespace Domain.DbModels.Common;
public class AuditingHistory
{
    public AuditingHistory()
    {
        Changes = new HashSet<AuditingHistoryChange>();
    }

    public int AuditingHistoryId { get; set; }
    public Guid AuditingHistoryGuid { get; set; }
    public string Rationale { get; set; } = string.Empty;
    public string Referrer { get; set; } = string.Empty;
    public DateTime? Date { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string ChangeType { get; set; } = string.Empty;
    public string RecordPK { get; set; } = string.Empty;

    public ICollection<AuditingHistoryChange> Changes { get; set; }
}