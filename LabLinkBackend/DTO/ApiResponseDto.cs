using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabLinkBackend.DTO;

public class ApiResponseDto<T>
{
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
}