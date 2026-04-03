using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using System.Threading.Tasks;

namespace LabLinkBackend.Repositories;

public interface IClientRepository
{
    Task<ClientListResult> GetAllAsync();
}
