

using Kastner.Dapper.Framework.Database.Model.ModelConfig;
using Kastner.Dapper.Framework.Database.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;


#nullable enable
namespace Kastner.Dapper.Framework.Database.Repositories
{
  public class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : IModelBaseClass
  {
    protected IDbConnection connection;
    private TransactionScope transactionScope;
    private ICRUDServiceAsync<T> crudService;

    public Task<IEnumerable<T>> FindAllAsync() => this.crudService.FindAllAsync();

    public Task<int> InsertAsync(T modelClass) => this.crudService.InsertAsync(modelClass);

    public Task<int> UpdateAsync(T modelClass) => this.crudService.UpdateAsync(modelClass);

    public Task<int> DeleteAsync(T modelClass) => this.crudService.DeleteAsync(modelClass);

    public T Find(T modelClass) => this.crudService.Find(modelClass);

    public IEnumerable<T> FindAll() => this.crudService.FindAll();

    public int Insert(T modelClass) => this.crudService.Insert(modelClass);

    public int Update(T modelClass) => this.crudService.Update(modelClass);

    public int Delete(T modelClass) => this.crudService.Delete(modelClass);

    public void Dispose()
    {
      this.connection.Close();
      this.connection.Dispose();
      if (!(Transaction.Current != (Transaction) null))
        return;
      this.transactionScope.Complete();
      this.transactionScope.Dispose();
    }

    public void Rollback()
    {
      this.transactionScope.Dispose();
      this.transactionScope = new TransactionScope();
    }

    public BaseRepository(string connectionString)
    {
      this.connection = new SqlConnection(connectionString) as IDbConnection;
      this.transactionScope = new TransactionScope();
      this.connection.Open();
      this.crudService = (ICRUDServiceAsync<T>) new CRUDServiceAsync<T>(this.connection);
    }
  }
}
