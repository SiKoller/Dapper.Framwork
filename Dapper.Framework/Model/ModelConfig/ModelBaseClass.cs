

using Kastner.Dapper.Framework.Database.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


#nullable enable
namespace Kastner.Dapper.Framework.Database.Model.ModelConfig
{
  public class ModelBaseClass : IModelBaseClass
  {
    public string Key => this.GetKeyByPrimaryAttributes();

    private string GetKeyByPrimaryAttributes() => string.Join("|", ((IEnumerable<PropertyInfo>) this.GetPrimaryKeys()).Select<PropertyInfo, string>((Func<PropertyInfo, string>) (pk => pk.GetValue((object) this).ToString())));

    public PropertyInfo[] GetPrimaryKeys()
    {
      List<PropertyInfo> propertyInfoList = new List<PropertyInfo>();
      foreach (PropertyInfo property in this.GetType().GetProperties())
      {
        foreach (object customAttribute in property.GetCustomAttributes(true))
        {
          if (customAttribute is PrimaryKeyAttribute)
            propertyInfoList.Add(property);
        }
      }
      return propertyInfoList.ToArray();
    }

    public override bool Equals(object obj) => obj is ModelBaseClass modelBaseClass && this.Key == modelBaseClass.Key;

    public override int GetHashCode() => 990326508 + EqualityComparer<string>.Default.GetHashCode(this.Key);

    public PropertyInfo[] GetAllProperties() => ((IEnumerable<PropertyInfo>) this.GetType().GetProperties()).Where<PropertyInfo>((Func<PropertyInfo, bool>) (p => p.Name != "Key")).ToArray<PropertyInfo>();
  }
}
