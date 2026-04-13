namespace LabLinkBackend.DTO;

public class ResultEntryCreateDto
{
    public int OrderItemId { get; set; }
    public int TestId { get; set; }
    public string? Analyte { get; set; }
    public string? Value { get; set; }
    public string? Units { get; set; }
    public string? Source { get; set; }
}
