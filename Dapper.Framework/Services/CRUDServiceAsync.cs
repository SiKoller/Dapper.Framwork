

using Dapper;
using Kastner.Dapper.Framework.Database.Model.ModelConfig;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

#nullable enable
namespace Kastner.Dapper.Framework.Database.Services
{
  internal class CRUDServiceAsync<T> : ICRUDServiceAsync<T> where T : IModelBaseClass
  {
    private IDbConnection connection;

    private string TableName => typeof (T).Name;

    public T Find(T modelClass)
    {
      try
      {
        return SqlMapper.QuerySingle<T>(this.connection, this.CreateQuery("select * from " + this.TableName + " ", modelClass), (object) modelClass, (IDbTransaction) null, new int?(), new CommandType?());
      }
      catch (SqlException ex)
      {
        if (((ExternalException) ex).ErrorCode == -2146232060)
          throw new Exception("Die angegebene Tabelle: " + typeof (T).Name + " ist leider falsch geschrieben oder nicht vorhanden");
        throw new Exception("Fehler:" + ((Exception) ex).Message);
      }
    }

    public IEnumerable<T> FindAll()
    {
      try
      {
        return SqlMapper.Query<T>(this.connection, "select * from " + this.TableName, (object) null, (IDbTransaction) null, true, new int?(), new CommandType?());
      }
      catch (SqlException ex)
      {
        if (((ExternalException) ex).ErrorCode == -2146232060)
          throw new Exception("Die angegebene Tabelle: " + typeof (T).Name + " ist leider falsch geschrieben oder nicht vorhanden");
        throw new Exception("Fehler:" + ((Exception) ex).Message);
      }
    }

    public int Insert(T va)
    {
      try
      {
        PropertyInfo[] allProperties = va.GetAllProperties();
        string str1 = string.Join(",", ((IEnumerable<PropertyInfo>) allProperties).Select<PropertyInfo, string>((Func<PropertyInfo, string>) (pi => pi.Name)));
        string str2 = string.Join(", @", ((IEnumerable<PropertyInfo>) allProperties).Select<PropertyInfo, string>((Func<PropertyInfo, string>) (pi => pi.Name)));
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 3);
        interpolatedStringHandler.AppendLiteral("Insert Into ");
        interpolatedStringHandler.AppendFormatted(this.TableName);
        interpolatedStringHandler.AppendLiteral("(");
        interpolatedStringHandler.AppendFormatted(str1);
        interpolatedStringHandler.AppendLiteral(") Values (@");
        interpolatedStringHandler.AppendFormatted(str2);
        interpolatedStringHandler.AppendLiteral(")");
        return SqlMapper.Execute(this.connection, interpolatedStringHandler.ToStringAndClear(), (object) va, (IDbTransaction) null, new int?(), new CommandType?());
      }
      catch (SqlException ex)
      {
        if (((ExternalException) ex).ErrorCode == -2146232060)
          throw new Exception("Die angegebene Tabelle: " + typeof (T).Name + " ist leider falsch geschrieben oder nicht vorhanden");
        throw new Exception("Fehler:" + ((Exception) ex).Message);
      }
    }

    public int Update(T modelClass)
    {
      try
      {
        return SqlMapper.Execute(this.connection, this.CreateQuery("Update " + this.TableName + " Set " + string.Join(",", (IEnumerable<string>) ((IEnumerable<PropertyInfo>) modelClass.GetAllProperties()).Select<PropertyInfo, string>((Func<PropertyInfo, string>) (p => p.Name)).ToList<string>().Select<string, string>((Func<string, string>) (a => this.CreateAsignment(a))).ToList<string>()) + " ", modelClass), (object) modelClass, (IDbTransaction) null, new int?(), new CommandType?());
      }
      catch (SqlException ex)
      {
        if (((ExternalException) ex).ErrorCode == -2146232060)
          throw new Exception("Die angegebene Tabelle: " + typeof (T).Name + " ist leider falsch geschrieben oder nicht vorhanden");
        throw new Exception("Fehler:" + ((Exception) ex).Message);
      }
    }

    public int Delete(T modelClass)
    {
      try
      {
        return SqlMapper.Execute(this.connection, this.CreateQuery("Delete from " + this.TableName + " ", modelClass), (object) modelClass, (IDbTransaction) null, new int?(), new CommandType?());
      }
      catch (SqlException ex)
      {
        if (((ExternalException) ex).ErrorCode == -2146232060)
          throw new Exception("Die angegebene Tabelle: " + typeof (T).Name + " ist leider falsch geschrieben oder nicht vorhanden");
        throw new Exception("Fehler:" + ((Exception) ex).Message);
      }
    }

    public Task<IEnumerable<T>> FindAllAsync()
    {
      try
      {
        return SqlMapper.QueryAsync<T>(this.connection, "select * from " + this.TableName, (object) null, (IDbTransaction) null, new int?(), new CommandType?());
      }
      catch (SqlException ex)
      {
        if (((ExternalException) ex).ErrorCode == -2146232060)
          throw new Exception("Die angegebene Tabelle: " + typeof (T).Name + " ist leider falsch geschrieben oder nicht vorhanden");
        throw new Exception("Fehler:" + ((Exception) ex).Message);
      }
    }

    public Task<int> InsertAsync(T modelClass)
    {
      try
      {
        PropertyInfo[] allProperties = modelClass.GetAllProperties();
        string str1 = string.Join(",", ((IEnumerable<PropertyInfo>) allProperties).Select<PropertyInfo, string>((Func<PropertyInfo, string>) (pi => pi.Name)));
        string str2 = string.Join(", @", ((IEnumerable<PropertyInfo>) allProperties).Select<PropertyInfo, string>((Func<PropertyInfo, string>) (pi => pi.Name)));
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 3);
        interpolatedStringHandler.AppendLiteral("Insert Into ");
        interpolatedStringHandler.AppendFormatted(this.TableName);
        interpolatedStringHandler.AppendLiteral("(");
        interpolatedStringHandler.AppendFormatted(str1);
        interpolatedStringHandler.AppendLiteral(") Values (@");
        interpolatedStringHandler.AppendFormatted(str2);
        interpolatedStringHandler.AppendLiteral(")");
        return SqlMapper.ExecuteAsync(this.connection, interpolatedStringHandler.ToStringAndClear(), (object) modelClass, (IDbTransaction) null, new int?(), new CommandType?());
      }
      catch (SqlException ex)
      {
        if (((ExternalException) ex).ErrorCode == -2146232060)
          throw new Exception("Die angegebene Tabelle: " + typeof (T).Name + " ist leider falsch geschrieben oder nicht vorhanden");
        throw new Exception("Fehler:" + ((Exception) ex).Message);
      }
    }

    public Task<int> UpdateAsync(T modelClass)
    {
      try
      {
        return SqlMapper.ExecuteAsync(this.connection, this.CreateQuery("Update " + this.TableName + " Set " + string.Join(",", (IEnumerable<string>) ((IEnumerable<PropertyInfo>) modelClass.GetAllProperties()).Select<PropertyInfo, string>((Func<PropertyInfo, string>) (p => p.Name)).ToList<string>().Select<string, string>((Func<string, string>) (a => this.CreateAsignment(a))).ToList<string>()) + " ", modelClass), (object) modelClass, (IDbTransaction) null, new int?(), new CommandType?());
      }
      catch (SqlException ex)
      {
        if (((ExternalException) ex).ErrorCode == -2146232060)
          throw new Exception("Die angegebene Tabelle: " + typeof (T).Name + " ist leider falsch geschrieben oder nicht vorhanden");
        throw new Exception("Fehler:" + ((Exception) ex).Message);
      }
    }

    public Task<int> DeleteAsync(T modelClass)
    {
      try
      {
        return SqlMapper.ExecuteAsync(this.connection, this.CreateQuery("Delete from " + this.TableName + " ", modelClass), (object) modelClass, (IDbTransaction) null, new int?(), new CommandType?());
      }
      catch (SqlException ex)
      {
        if (((ExternalException) ex).ErrorCode == -2146232060)
          throw new Exception("Die angegebene Tabelle: " + typeof (T).Name + " ist leider falsch geschrieben oder nicht vorhanden");
        throw new Exception("Fehler:" + ((Exception) ex).Message);
      }
    }

    public CRUDServiceAsync(IDbConnection connection) => this.connection = connection;

    public CRUDServiceAsync(string connectionString) => this.connection = (IDbConnection) new SqlConnection(connectionString);

    private string getPrimaryWhere(T modelClass)
    {
      IEnumerable<string> source = ((IEnumerable<PropertyInfo>) modelClass.GetPrimaryKeys()).Select<PropertyInfo, string>((Func<PropertyInfo, string>) (pk => pk.Name));
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine(" Where ");
      stringBuilder.AppendLine(string.Join(" and ", source.Select<string, string>((Func<string, string>) (kn => this.CreateAsignment(kn)))));
      return stringBuilder.ToString();
    }

    private string CreateQuery(string startQuery, T modelClass)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine(startQuery);
      stringBuilder.AppendLine(this.getPrimaryWhere(modelClass));
      return stringBuilder.ToString();
    }

    private string CreateAsignment(string valueName) => valueName + " = @" + valueName;
  }
}
