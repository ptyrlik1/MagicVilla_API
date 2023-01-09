using MagicVilla_WebApi.Data;
using MagicVilla_WebApi.Models;
using MagicVilla_WebApi.Repository.InRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Linq.Expressions;

namespace MagicVilla_WebApi.Repository
{
    public class VillaNumberRepository :  Repository<VillaNumber>,IVillaNumberRepository
    {
        private readonly ApplicationDbContext _db;

        public VillaNumberRepository(ApplicationDbContext db): base(db) 
        {
            _db = db;
        }

        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.VillaNumbers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
