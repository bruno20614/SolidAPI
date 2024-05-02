namespace Manager.Infra.Interfaces;
using Manager.Domain.Entities
using System.Threading.Tasks;
using System.Collection.Generic;

public interface IBaseReposiroy<T> where T : Base
{
    Task<T> Create(T obj);
    Task<T> Update(T obj);
    Task<T> Remove(long id);
    Task<T> Get(long id);
    Task<List<T>> Get();
}