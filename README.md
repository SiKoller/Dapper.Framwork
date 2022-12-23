# Dapper.Framwork
This Framwork provides Basic CRUD Functions.

# Why I created this Framework
I wanted to learn more about Dapper, git and C# and also to prevent to write this CRUD Scripts all the times and simplify Rollbacks and Tansaction-handling.
I think it is not that good yet and will hopefully grow.

# Geeting Started:
## ModelBaseClass

First Create a Model Base Class.

### PrimaryKey
This class will override Equals Hascode with the "PrimaryKey" Attribute.

```C#
public class Customer :ModelBaseClass
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string Name { get; set; }
        public Customer() { }
    }
```
### Ignore Columns
To ignore Columns add the "Ignore" Attribute.

```C#
public class Customer :ModelBaseClass
    {
        [PrimaryKey]
        public int ID { get; set; }
        
        [Ignore]
        public string Name { get; set; }
        public Customer() { }
    }
```
## Repositories

The repository design pattern is a software design pattern that separates the logical representation of data from the physical storage of data.
It provides a layer of abstraction between the application and the database, 
allowing the application to access and manipulate data without having to know how it is stored or how it is retrieved.

### Interface
```C#
 public interface ICustomerRepository<T> :IBaseRepository<T> where T : IModelBaseClass
    {

    }
```

### Concrete Repository
```C#
public class CustomerRepository<T> : BaseRepository<T>, ICustomerRepository<T> where T : IModelBaseClass
    {
        public CustomerRepository(string connectionString) : base(connectionString)
        {
        }
    }
```
This Repository already provides Basic functionallity and can be modified with additional Methods.

# How it Works

```C#
using (CustomerRepository<Customer> rep = new CustomerRepository<Customer>(connectionString))
            {
                rep.Insert(new Customer() { ID = 2, Name="Testkunde"});
            }
```
In the Dispose Method, the Transactions will be commited or make a rollback if there is an Error.


