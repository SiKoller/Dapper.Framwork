
using Kastner.Dapper.Framework.Database.Model.ModelConfig;
using System.Collections.Generic;
using System.Threading.Tasks;


#nullable enable
namespace Kastner.Dapper.Framework.Database.Services
{
  public interface ICRUDServiceAsync<T> where T : IModelBaseClass
  {
    Task<IEnumerable<T>> FindAllAsync();

    Task<int> InsertAsync(T modelClass);

    Task<int> UpdateAsync(T modelClass);

    Task<int> DeleteAsync(T modelClass);

    T Find(T modelClass);

    IEnumerable<T> FindAll();

    int Insert(T modelClass);

    int Update(T modelClass);

    int Delete(T modelClass);
  }
}
