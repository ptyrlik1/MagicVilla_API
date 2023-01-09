using MagicVilla_WebApi.Models;
using System.Linq.Expressions;

namespace MagicVilla_WebApi.Repository.InRepository
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {

        Task<VillaNumber> UpdateAsync(VillaNumber entity);
    }
}
