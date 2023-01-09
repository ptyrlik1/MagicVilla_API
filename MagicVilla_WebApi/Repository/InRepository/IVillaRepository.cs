using MagicVilla_WebApi.Models;
using System.Linq.Expressions;

namespace MagicVilla_WebApi.Repository.InRepository
{
    public interface IVillaRepository : IRepository<Villa>
    {

        Task<Villa> UpdateAsync(Villa entity);
    }
}
