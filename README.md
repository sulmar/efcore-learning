
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


# Wprowadzenie

## Dostawcy baz danych

| Database | NuGet Package  |
|---|---|
| SQL Server | Microsoft.EntityFrameworkCore.SqlServer |
| SQLite | Microsoft.EntityFrameworkCore.SQLite |
| MySQL | MySql.Data.EntityFrameworkCore |
| PostgreSQL | Npgsql.EntityFrameworkCore.PostgreSQL |
| SQL Compact | EntityFrameworkCore.SqlServerCompact40 |
| In-memory | 	Microsoft.EntityFrameworkCore.InMemory |


# Instalacja

# Istniejąca baza danych

Instalacja narzędzi
``` powershell
Install-Package Microsoft.EntityFrameworkCore.Tools
```

Wygenerowanie klas na podstawie bazy danych
``` powershell
PM> Scaffold-DbContext "Server=.\SQLExpress;Database=Northwind;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
```

# DbContext
Klasa DbContext jest główną częścią Entity Framework. Instacja DbContext reprezentuje sesję z bazą danych.

DbContext umożliwia następujące zadania:
 1. Zarządzanie połączeniem z bazą danych
 2. Konfiguracja modelu i relacji
 3. Odpytywanie bazy danych
 4. Zapisywanie danych do bazy danych
 5. Śledzenie zmian
 6. Cacheowanie
 7. Zarządzanie transakcjami

## Właściwości DbContext
| Metoda | Użycie |
|---|---|
| ChangeTracker | Dostarcza informacje i operacje do śledzenie obiektów  |
| Database | Dostarcza informacje i operacje bazy danych |
| Model | Zwraca metadane o encjach, ich relacjach i w jaki sposób mapowane są do bazy danych |


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


# Praca z odłączonymi encjami