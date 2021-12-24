using Microsoft.EntityFrameworkCore.Storage;
using PlaneSpotters.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotters.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private PlaneSpotterDBContext _context { get; set; }

        public UnitOfWork(PlaneSpotterDBContext context)
        {
            this._context = context;
        }

        public async Task Commit()
        {
            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(true);
            }
            catch
            {

            }
        }
        public IDbContextTransaction BeginTransaction()
        {
            var transaction = _context.Database.BeginTransaction();
            return transaction;
        }
    }
}
