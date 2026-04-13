namespace LabLinkBackend.DTO
{
    public class SpecimenCreateDTO
    {
        public int OrderID { get; set; }
        public int OrderItemId { get; set; }
        public int? SpecimenTypeId { get; set; }
        public int? ContainerTypeId { get; set; }
        public int? CollectedBy { get; set; }
        public DateTime? CollectedDate { get; set; }
        public string? RejectionReason { get; set; } // Optional
        public bool IsActive { get; set; } = true;
    }
}