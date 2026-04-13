namespace LabLinkBackend.DTO;

public class ResultEntryResponseDto
{
    public int ResultId { get; set; }
    public int OrderItemId { get; set; }
    public int TestId { get; set; }
    public string? Analyte { get; set; }
    public string? Value { get; set; }
    public string? Units { get; set; }
    public string? Source { get; set; }
    public string? Flag { get; set; }
    public string? EnteredByUsername { get; set; }
    public DateTime? EnteredDate { get; set; }
}
