using ApiMundoDisney.Models;
using ApiMundoDisney.Utilities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMundoDisney.Repositories.Implements
{
    public class BaseRepository<TCreation, TEntity, TDto> : IBaseRepository<TCreation, TEntity, TDto>
        where TEntity : class, IHaveId
    {
        private readonly DisneyDbContext _disneyDb;
        private readonly IMapper _mapper;
        public BaseRepository(DisneyDbContext disneyDbContext, IMapper mapper)
        {
            _disneyDb = disneyDbContext;
            _mapper = mapper;
        }
        public virtual async Task<bool> Delete(int id)
        {
            try
            {
                var entidad = await _disneyDb.Set<TEntity>().FirstOrDefaultAsync(c => c.Id == id);
                _disneyDb.Entry(entidad).State = EntityState.Deleted;
                await _disneyDb.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task<List<TDto>> GetAll()
        {
            try
            {
                var entidad = await _disneyDb.Set<TEntity>().ToListAsync();
                return _mapper.Map<List<TDto>>(entidad);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public virtual async Task<TDto> GetByID(int id)
        {
            try
            {
                var entidad = await _disneyDb.Set<TEntity>().FindAsync(id);
                return _mapper.Map<TDto>(entidad);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task<TDto> Insert(TCreation creation)
        {
            try
            {
                var entidad = _mapper.Map<TEntity>(creation);
                await _disneyDb.Set<TEntity>().AddAsync(entidad);
                await _disneyDb.SaveChangesAsync();
                var dto = _mapper.Map<TDto>(entidad);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task<TDto> Update(int id, TCreation creation)
        {
            try
            {
                var entidad = await _disneyDb.Set<TEntity>().FirstOrDefaultAsync(c => c.Id == id);
                _mapper.Map(creation, entidad);
                _disneyDb.Entry(entidad).State = EntityState.Modified;
                await _disneyDb.SaveChangesAsync();
                var dto = _mapper.Map<TDto>(entidad);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
