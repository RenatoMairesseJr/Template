namespace Domain.DbModels.Common;

public class AuditingHistoryChange
{
    public int AuditingHistoryChangeId { get; set; }
    public int AuditingHistoryId { get; set; }
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string PropertyName { get; set; } = string.Empty;
    public string TableName { get; set; } = string.Empty;
}
