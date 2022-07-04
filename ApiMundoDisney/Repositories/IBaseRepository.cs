using ApiMundoDisney.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMundoDisney.Repositories
{
    public interface IBaseRepository<TCreation, TEntity, TDto> where TEntity : class, IHaveId
    {
        public Task<List<TDto>> GetAll();
        public Task<TDto> GetByID(int id);
        public Task<TDto> Insert(TCreation creation);
        public Task<TDto> Update(int id, TCreation creation);
        public Task<bool> Delete(int id);

    }
}
