

using System.Reflection;


#nullable enable
namespace Kastner.Dapper.Framework.Database.Model.ModelConfig
{
  public interface IModelBaseClass
  {
    string Key { get; }

    PropertyInfo[] GetPrimaryKeys();

    PropertyInfo[] GetAllProperties();
  }
}
