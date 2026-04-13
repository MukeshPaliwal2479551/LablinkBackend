using System;

namespace LabLinkBackend.DTO
{
    public class TestDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int? DepartmentId { get; set; }
        public int? SpecimenTypeId { get; set; }
        public int ContainerTypeId { get; set; }
        public double? VolumeReq { get; set; }
        public int? Units { get; set; }
        public int MaxNormalValue { get; set; }
        public int MinNormalValue { get; set; }
        public int? TatTargetMinutes { get; set; }
        public string? RefRangeJson { get; set; }
        public bool IsActive { get; set; }
    }
}
