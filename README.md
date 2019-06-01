
# Spis treści
* Wprowadzenie
* Instalacja
* Istniejąca baza danych
* DbContext
* Pierwsza aplikacja
* Migracje
* Zapytania
* Zapisywanie danych
* Konwencje
* Konfiguracje
* Relacje FluentApi
* Praca z odłączonymi encjami
* Śledzenie zmian
* Surowe zapytania SQL
* Procedury składowane
* Filtry globalne
* Konwertery


# NET Core

## Przydatne komendy CLI
- ``` dotnet new {template} ``` - utworzenie nowego projektu na podstawie wybranego szablonu
- ``` dotnet new {template} -o {output} ``` - utworzenie nowego projektu w podanym katalogu
- ``` dotnet restore ``` - pobranie bibliotek nuget na podstawie pliku projektu
- ``` dotnet build ``` - kompilacja projektu
- ``` dotnet run ``` - uruchomienie projektu
- ``` dotnet run {app.dll}``` - uruchomienie aplikacji
- ``` dotnet test ``` - uruchomienie testów jednostkowych
- ``` dotnet run watch``` - uruchomienie projektu w trybie śledzenia zmian
- ``` dotnet test ``` - uruchomienie testów jednostkowych w trybie śledzenia zmian
- ``` dotnet add {project.csproj} referencje {library.csproj} ``` - dodanie odwołania do biblioteki
- ``` dotnet remove {project.csproj} referencje {library.csproj} ``` - usunięcie odwołania do biblioteki
- ``` dotnet new sln ``` - utworzenie nowego rozwiązania
- ``` dotnet sln {solution.sln} add {project.csproj}``` - dodanie projektu do rozwiązania
- ``` dotnet sln {solution.sln} remove {project.csproj}``` - usunięcie projektu z rozwiązania
- ``` dotnet publish -c Release -r {platform}``` - publikacja aplikacji
- ``` dotnet publish -c Release -r win10-x64``` - publikacja aplikacji dla Windows
- ``` dotnet publish -c Release -r linux-x64``` - publikacja aplikacji dla Linux
- ``` dotnet publish -c Release -r osx-x64``` - publikacja aplikacji dla MacOS



# Entity Framework Core

## Instalacja SQL Server jako obraz Docker

~~~ bash
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=yourStrong(!)Password' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest
~~~

## Uruchomienie w trybie interaktywnym
~~~
docker exec -it <container_id|container_name> /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P <your_password>
~~~


~~~ csharp
private static void CreateDbTest()
{
    string connectionString = "Server=127.0.0.1,1433;Database=mydb;User Id=sa;Password=P@ssw0rd";

    // string connectionString = Configuration.GetConnectionString("MyConnectionString");

    var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
    optionsBuilder.UseSqlServer(connectionString);

    using (var context = new MyContext(optionsBuilder.Options))
    {
        bool created = context.Database.EnsureCreated(); 

        System.Console.WriteLine(created);

    }
}
~~~


- Azure Data Studio

https://docs.microsoft.com/en-us/sql/azure-data-studio/download?view=sql-server-2017



## Instalacja EF Core

~~~ bash
dotnet add package Microsoft.EntityFrameworkCore
~~~

### Instalacja dostawcy bazy danych SQL Server
~~~ bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
~~~

### Dostawcy baz danych

| Database | NuGet Package  |
|---|---|
| SQL Server | Microsoft.EntityFrameworkCore.SqlServer |
| SQLite | Microsoft.EntityFrameworkCore.SQLite |
| MySQL | MySql.Data.EntityFrameworkCore |
| PostgreSQL | Npgsql.EntityFrameworkCore.PostgreSQL |
| SQL Compact | EntityFrameworkCore.SqlServerCompact40 |
| In-memory | 	Microsoft.EntityFrameworkCore.InMemory |


## Przydatne komendy CLI
- ``` dotnet ef ``` - weryfikacja instalacji
- ``` dotnet ef migrations add {migration} ``` - utworzenie migracji
- ``` dotnet ef migrations remove ``` - usunięcie ostatniej migracji
- ``` dotnet ef migrations list ``` - wyświetlenie listy wszystkich migracji
- ``` dotnet ef migrations script ``` - wygenerowanie skryptu do aktualizacji bazy danych do najnowszej wersji
- ``` dotnet ef database update ``` - aktualizacja bazy danych do najnowszej wersji
- ``` dotnet ef database update -verbose ``` - aktualizacja bazy danych do najnowszej wersji + wyświetlanie logu
- ``` dotnet ef database update {migration} ``` - aktualizacja bazy danych do podanej migracji
- ``` dotnet ef database drop ``` - usunięcie bazy danych
- ``` dotnet ef dbcontext info ``` - wyświetlenie informacji o DbContext (provider, nazwa bazy danych, źródło)
- ``` dotnet ef dbcontext list ``` - wyświetlenie listy DbContextów
- ``` dotnet ef dbcontext scaffold {connectionstring} Microsoft.EntityFrameworkCore.SqlServer -o Models ``` - wygenerowanie modelu na podstawie bazy danych

## Przydatne polecenia PMC
- ``` Add-Migration {migration} ``` - utworzenie migracji
- ``` Remove-Migration ``` - usunięcie ostatniej migracji
- ``` Update-Database -script ``` - wygenerowanie skryptu do aktualizacji bazy danych do najnowszej wersji
- ``` Update-Database ``` - aktualizacja bazy danych do najnowszej wersji
- ``` Update-Database -verbose ``` - aktualizacja bazy danych do najnowszej wersji + wyświetlanie logu
- ``` Update-Database {migration} ``` - aktualizacja bazy danych do podanej 
- ``` Scaffold-DbContext {connectionstring} Microsoft. Models ``` - wygenerowanie modelu na podstawie bazy danych

# DbContext
Klasa DbContext jest główną częścią Entity Framework. Instacja DbContext reprezentuje sesję z bazą danych.


Models.cs

~~~ csharp

public class Order
{
    public int OrderId { get; set; }   
    public string OrderNumber { get; set; }  
    public DateTime OrderDate { get; set; }
    public DateTime DeliveryDate? { get; set; }
    public Customer Customer { get; set; }
}

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsDeleted { get; set; }
}

~~~


MyContext.cs

~~~ csharp
public class MyContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    public CustomersContext(DbContextOptions options) 
        : base(options)
    {
    }
}
~~~

~~~ csharp
var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
optionsBuilder.UseSqlite("Data Source=blog.db");

using (var context = new MyContext(optionsBuilder.Options))
{
  // do stuff
}
~~~

DbContext umożliwia następujące zadania:
 1. Zarządzanie połączeniem z bazą danych
 2. Konfiguracja modelu i relacji
 3. Odpytywanie bazy danych
 4. Zapisywanie danych do bazy danych
 5. Śledzenie zmian
 6. Cache'owanie
 7. Zarządzanie transakcjami

## Właściwości DbContext
| Metoda | Użycie |
|---|---|
| ChangeTracker | Dostarcza informacje i operacje do śledzenie obiektów  |
| Database | Dostarcza informacje i operacje bazy danych |
| Model | Zwraca metadane o encjach, ich relacjach i w jaki sposób mapowane są do bazy danych |



# Konwencja relacji Jeden-do-wielu

## Konwencja 1
Encja zawiera navigation property.

``` csharp
public class Order
{
    public int OrderId { get; set; }   
    public string OrderNumber { get; set; }  

    public Customer Customer { get; set; } // Navigation property
}

public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

```
Zamówienie zawiera referencje do navigation property typu klient. EF utworzy shadow property CustomerId w modelu koncepcyjnym, które będzie mapowane do kolumny CustomerId w tabeli Orders.

## Konwencja 2
Encja zawiera kolekcję.

``` csharp
public class Order
{
    public int OrderId { get; set; }       
    public string OrderNumber { get; set; } 
}

public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public List<Order> Orders { get; set; }
}

```

W bazie danych będzie taki sam rezultat jak w przypadku konwencji 1.

## Konwencja 3

Relacja zawiera navigation property po obu stronach. W rezultacie otrzymujemy połączenie konwencji 1 i 2.


``` csharp
public class Order
{
    public int OrderId { get; set; }       
    public string OrderNumber { get; set; } 

    public Customer Customer { get; set; } // Navigation property
}

public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public List<Order> Orders { get; set; }
}

```

## Konwencja 4
Konwencja z uzyciem wlasciwosci foreign key


``` csharp
public class Order
{
    public int OrderId { get; set; }       
    public string OrderNumber { get; set; } 

    public int CustomerId { get; set; }  // Foreign key property
    public Customer Customer { get; set; } // Navigation property
}

public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public List<Order> Orders { get; set; }
}

```


# Konwencja relacji Jeden-do-jeden

``` csharp
public class Order
{
    public int OrderId { get; set; }       
    public string OrderNumber { get; set; } 

    public Payment Payment { get; set; } // Navigation property
}

public class Payment
{
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }
}
```

# Konwencja relacji wiele-do-wielu

Obecnie w EF Core nie ma domyslnej konwencji, która konfiguruje relację wiele-do-wielu. Trzeba uzyc Fluent Api.


# Konfiguracja relacji Jeden-do-wielu z uzyciem Fluent API


``` csharp
   protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne<Customer>()
                .WithMany(c=>c.Orders)
                .HasForeignKey(p=>p.CustomerId);
``` 


Alternatywnie mozna wyjsc od drugiej strony
``` csharp
            modelBuilder.Entity<Customer>()
                .HasMany(c=>c.Orders)
                .WithOne(o=>o.Customer)
                .HasForeignKey(o=>o.CustomerId);


        }
```

## Konfiguracja kaskadowego usuwania z Fluent API

``` csharp
modelBuilder.Entity<Customer>()
                .HasMany(c=>c.Orders)
                .WithOne(o=>o.Customer)
                .HasForeignKey(o=>o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
```

Rodzaje:
- Cascade - usuwa wszystkie encje wraz z encją nadrzędną
- ClientSetNull - klucze obce w encjach zaleznych będą ustawione na null
- Restrict - blokuje kaskadowe usuwanie
- SetNull - klucze obce w encjach zaleznych będą ustawione na null


## Konfiguracja jeden-do-jeden z Fluent API


``` csharp
 modelBuilder.Entity<Order>()
                .HasOne<Payment>()
                .WithOne(p=>p.Order)
                .HasForeignKey<Payment>(p=>p.PaymentId);
```

# Migracje

## Instalacja narzędzi

PMC
```
Microsoft.EntityFrameworkCore.Tools
```




# SQLite

- DB Browser for SQLite
http://sqlitebrowser.org





# Praca z odłączonymi encjami


## Attach()
Metoda *Attach()* przyłącza odłączony graf encji i zaczyna go śledzić.

Metoda *Attach()* ustawia główną encję na stan Added niezależnie od tego, czy posiada wartość klucza. Jeśli encje dzieci posiadają wartość klucza wówczas zaznaczane są jako *Unchanged*, a w przeciwnym razie jako *Added*.


``` csharp
context.Attach(entityGraph).State = state;
```

| Attach()  | Root entity with Key value  | Root Entity with Empty or CLR default value  | Child Entity with Key value  |  Child Entity with empty or CLR default value |
|---|---|---|---|---|
| EntityState.Added  | Added  | Added  | Unchanged  | Added  |
| EntityState.Modified  | Modified  | Exception  | Unchanged  | Added  |
| EntityState.Deleted  | Deleted  | Exception  | Unchanged  | Added  |



## Entry

``` csharp
context.Entry(order).State = EntityState.Modified
```

Wyrażenie przyłącza encję do kontekstu i ustawia stan na **Modified**. Ignoruje wszystkie pozostałe encje.

## Add()
Metody *DbContext.Add()* i *DbSet.Add()* przyłączają graf encji do kontekstu i ustawiają stan encji na **Added** niezależnie od tego czy posiadają wartość klucza czy też nie.

| Method | Root entity with/out Key value  | 	Root entity with/out Key |
|---|---|---|
| DbContext.Add  | Added  | Added  | 


## Update()

Metoda *Update()* przyłącza graf encji do kontekstu i ustawia stan poszczególnych encji zależnie od tego czy jest ustawiona wartość klucza.

| Update()  | Root entity with Key value  | Root Entity with Empty or CLR default value  | Child Entity with Key value  |  Child Entity with empty or CLR default value |
|---|---|---|---|---|
| DbContext.Update  | Modified  | Added  | Modified  | Added  |



## Delete()

Metoda *Delete()* ustawia stan głównej encji na **Deleted**.

| Delete()  | Root entity with Key value  | Root Entity with Empty or CLR default value  | Child Entity with Key value  |  Child Entity with empty or CLR default value |
|---|---|---|---|---|
| DbContext.Delete  | Deleted  | Exception  | Unchanged  | Added  |


# Filtry globalne

## Konfiguracja filtra globalnego
~~~ csharp
 internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
 {
     public void Configure(EntityTypeBuilder<Customer> builder)
     {
        builder.HasQueryFilter(p => !p.IsDeleted);
      }
   }
~~~

### Lokalne wyłączenie filtra globalnego

~~~ csharp
using(var context = new MyContext())
            {
                var customers = context.Customers.IgnoreQueryFilters().ToList();

                Display(customers);
            }
~~~
       



# Konwertery

### Wstęp
Czasami sposób zapisu wartości jakiejś właściwości w bazie danych różni się od tej zdefiniowanej w klasie. 
Na przykład w modelu posiadamy np. płeć jako enum a w bazie danych chcemy zapisać jako "M" lub "F". 
Albo bardziej złożony przykład - po stronie modelu posiadamy obiekt z adresem lub parametrami urządzenia, a w bazie danych chcemy zapisać go w jednej kolumnie jako xml lub json. 

Niestety w poprzedniej wersji EF 6 nie było gotowego mechanizmu i trzeba było stosować obejścia. 
Najczęściej obejście polegało na tym, że trzeba było utworzyć w klasie dodatkowe ukryte prywatne pole (tzw. backfield) odpowiadające typowi w bazie danych, a docelowa właściwość była oznaczona jako ignorowana przez EF. Następnie w metodach get i set była realizowana konwersja. Nie było to optymalne rozwiązanie i kod nie był przenośny.

W EF Core wprowadzono nową funkcję, tzw. konwertery (ValueConverters), które rozwiązują ten problem w bardzo elegancki sposób. 
Nie trzeba już tworzyć dodatkowych pól, ale przede wszystkim można wielokrotnie używać tej samej konwersji. 
Konwerter można użyć w konfiguracji oraz w konwencji.


### Konwersja za pomocą wyrażeń lambda

~~~ csharp
builder.Property(p=>p.Gender)
  .HasConversion(
      v => v.ToString(),
      v => (Gender)Enum.Parse(typeof(Gender), v)
);
~~~

### Konwersja za pomocą obiektu konwertera
~~~ csharp            
var converter = new ValueConverter<Gender, string>(
v => v.ToString(),
v => (Gender)Enum.Parse(typeof(Gender), v));
~~~

### Użycie wbudowanego konwertera
   
~~~ csharp              
            var converter = new EnumToStringConverter<Gender>();
            builder.Property(p=>p.Gender)
              .HasConversion(converter);
~~~

Niektóre z konwerterów posiadają dodatkowe parametry:
~~~ csharp              
            builder.Property(p=>p.IsDeleted)
                .HasConversion(new BoolToStringConverter("Y", "N"));
~~~

Lista wbudowanych konwerterów [https://docs.microsoft.com/en-us/ef/core/modeling/value-conversions]

                

### Predefiniowane konwersje
W większości przypadków nie trzeba tworzyć konwerterów, bo wystarczy skorzystać z predefiniowanych konwersji:
~~~ csharp 
builder.Property(p=>p.Gender)
                .HasConversion<string>();
~~~                

            
### Własny konwerter

#### Własny konwerter za pomocą wyrażenia lambda

~~~ csharp 
  builder.Property(p => p.ShippingAddress).HasConversion(
    v => JsonConvert.SerializeObject(v),
    v => JsonConvert.DeserializeObject<Address>(v));
~~~
   
#### Utworzenie własnego konwertera

Utworzenie klasy własnego konwertera
~~~ csharp 
public class JsonValueConverter<T> : ValueConverter<T, string>
    {
        public JsonValueConverter(ConverterMappingHints mappingHints = null)
        : base(v => JsonConvert.SerializeObject(v), 
               v => JsonConvert.DeserializeObject<T>(v), 
               mappingHints)
        {
        }      
    }
~~~

Użycie własnego konwertera
~~~ csharp 
builder.Property(p => p.ShippingAddress).HasConversion(new JsonValueConverter<Address>());
~~~

W celu ułatwienia korzystania z konwertera można utworzyć metodę rozszerzającą

~~~ csharp 
public static class PropertyBuilderExtensions
    {
        public static PropertyBuilder<T> HasJsonValueConversion<T>(this PropertyBuilder<T> propertyBuilder) where T : class
        {
            propertyBuilder
              .HasConversion(new JsonValueConverter<T>());

            return propertyBuilder;
        }
    }
    
~~~

A następnie użyć jej podczas konfiguracji
~~~ csharp 
            builder.Property(p => p.ShippingAddress)
                .HasJsonValueConversion();
~~~


# Change Tracker

Odczytanie stanu encji

~~~ csharp

Trace.WriteLine(context.Entry(customer).State);
foreach (var property in context.Entry(customer).Properties)
{
    Trace.WriteLine($"{property.Metadata.Name} {property.IsModified} {property.OriginalValue} -> {property.CurrentValue}");
}

~~~


# SQL

Uruchomienie zapytania SQL i pobranie wyników

~~~ csharp
public IEnumerable<Customer> Get(string lastname)
{
    string sql = $"select * from dbo.customers where LastName = '{lastname}'";
    return context.Customers.FromSql(sql);
}
~~~

- Uruchomienie procedury składowanej
~~~ csharp
using (var context = new SampleContext())
{
    var books = context.Customers
        .FromSql("EXEC GetAllCustomers")
        .ToList();
}
~~~

- Uruchomienie sparametryzowanej procedury składowanej
~~~ csharp
using (var context = new SampleContext())
{
    var city = new SqlParameter("@City", "Warsaw");
    var customers = context.Books
        .FromSql("EXEC GetCustomersByCity @City" , city)
        .ToList();
}
~~~

# DbQuery
Typ **DbQuery** został wprowadzony w .NET Core 2.1. Umozliwia mapowanie tabel i widoków.
Przypomina typ **DbSet** ale nie posiada operacji do zapisu, np. Add(). 

