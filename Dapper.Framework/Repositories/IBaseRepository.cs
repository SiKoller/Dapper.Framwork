

using Kastner.Dapper.Framework.Database.Model.ModelConfig;
using System.Collections.Generic;
using System.Threading.Tasks;


#nullable enable
namespace Kastner.Dapper.Framework.Database.Repositories
{
  public interface IBaseRepository<T> where T : IModelBaseClass
  {
    Task<IEnumerable<T>> FindAllAsync();

    Task<int> InsertAsync(T modelClass);

    Task<int> UpdateAsync(T modelClass);

    Task<int> DeleteAsync(T modelClass);

    IEnumerable<T> FindAll();

    T Find(T modelClass);

    int Insert(T modelClass);

    int Update(T modelClass);

    int Delete(T modelClass);

    void Rollback();
  }
}
