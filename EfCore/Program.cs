// See https://aka.ms/new-console-template for more information
using EfCore.DataAccess;
using EfCore.Entities;

using Microsoft.EntityFrameworkCore;

Console.WriteLine("Entity Frameworks All Methods");

EfContext context = new();
///  AddedAsync
//await AddedAsync(context, "Selim", "BAYINDIR");
static async Task AddedAsync(EfContext context, string name, string lastname, string city)
{
    Person person = new(name, lastname, city);
    await context.AddAsync(person);
    context.SaveChanges();
    Console.WriteLine("Success");
}
///  AddRangeAsync
///await AddRangeAsync(context);
static async Task AddRangeAsync(EfContext context)
{
    List<Person> _people;
    _people = new List<Person>
{
    new Person("Selim","BAYINDIR","ISTANBUL"),
    ///  new Urun() { ProductName = "Fanta" },
    new Person("Gülce","BAYINDIR","ISTANBUL"),
    new Person("Yiğit Can","İÇ","ISTANBUL"),
    new Person("Ayşe","BIRICIK","ANKARA"),
    new Person("AK","SUNGUR","EDİRNE"),
    new Person("Tom","HAWK","Sivas"),
    new Person("Tom","Vercetti","Sivas")
};
    await context.AddRangeAsync(_people);
    context.SaveChanges();
    Console.WriteLine("Success");
}

//UptateFalse(context);
static void UptateFalse(EfContext context)
{
    Person person = context.People.FirstOrDefault(p => p.Id == 8);
    person.Name = "Yahya";
    person.LastName = "Uzak";
    context.SaveChanges();
    Console.WriteLine("Success");
}
//
///Change Tracker :Context üzerinden gelen verilerin takibinden sorumlu bir mekanizmadır.
//UpdateMethod(context);
static void UpdateMethod(EfContext context)
{
    Person person = new();
    person.Id = 10;
    person.Name = "Yuri";
    person.LastName = "BOYKA";

    context.People.Update(person);
    context.SaveChanges();
    Console.WriteLine("Success");
}

//EntityState

//await ToListAsync(context);
static async Task ToListAsync(EfContext context)
{
    Person person = new();
    var personList = await context.People.ToListAsync();
    //foreach (var item in personList)
    //{
    //    Console.WriteLine(item.Name);
    //}
    ///EfCore ForEach
    personList.ForEach(x =>
    {
        Console.WriteLine(x.Name);
    });
}

//await EntityState(context);
static async Task EntityState(EfContext context)
{
    Person person = new();
    Console.WriteLine(context.Entry(person).State);
    person.Name = "Can";
    person.LastName = "Kızıl";
    await context.AddAsync(person);
    context.SaveChanges(true);
    Console.WriteLine(context.Entry(person).State);
}

//await Remove(context);
static async Task Remove(EfContext context)
{
    var deletepeople = await context.People.FirstOrDefaultAsync(p => p.Id == 14);
    Console.WriteLine(deletepeople.Name + " " + deletepeople.LastName + " Silindi ..");
    context.Remove(deletepeople);
    context.SaveChanges();
}

//await RemoveRange(context);
static async Task RemoveRange(EfContext context)
{
    List<Person> peopleList = await context.People.Where(p => p.Id >= 1 && p.Id <= 26).ToListAsync();
    context.RemoveRange(peopleList);
    await context.SaveChangesAsync();
    Console.WriteLine("Success");
}
//SqlRaw(context);
static void SqlRaw(EfContext context)
{
    var person = context.People
          .FromSqlRaw("SELECT * FROM People WHERE Name='Selim'")
          .ToList();
    person.ForEach(p =>
    {
        Console.WriteLine(p.Name);
    });
}

//FromSqlDeferedExecution(context);
static void FromSqlDeferedExecution(EfContext context)
{
    int urunid = 3;

    var urunler2 = from item in context.People
                   where item.Id > urunid //isteğe bağlı
                   select item;

    urunid = 10; //DEFERRED EXECUTİON SONRADAN ERTELNMİŞ ÇALIŞMA
    foreach (var item in urunler2)
    {
        Console.WriteLine(item.Id + " " + item.Name);
    }
}

///IQUERYABLE Ve Ienumarable Nedir?
///IQUERYABLE :Sorgu ya Karşılık Gelir :Ef Core üzerinden yapılmış sorgunun Execute edilmemiş halini ifade eder.
///IEnumarable:Sorgunun Çalııştırılıp/Exercute edilmiş Verilerin InMemorye yüklenmiş halini ifade eder.

//ContainsMethod(context);
static void ContainsMethod(EfContext context)
{
    var EfContains = from person in context.People
                     where person.Name.Contains("Selim")
                     select person;
    foreach (var item in EfContains)
    {

        Console.WriteLine("Personel Id {0} Personel Adı :{1} Personel Soyadi :{2}", item.Id, item.Name, item.LastName);

    }
}

//WherMethod(context);
static void WherMethod(EfContext context)
{
    var person = from per in context.People
                 where per.Id > 5 && per.Name.EndsWith("m")
                 select per;
    foreach (var item in person)
    {
        Console.WriteLine(item.Name + " " + item.LastName);
    }
}

//OrderByMethod(context);

static void OrderByMethod(EfContext context)
{
    var orderbyfunc = context.People.OrderBy(p => p.Name).ToList();///OrderByDescending
    orderbyfunc.ForEach(p =>
    {
        Console.WriteLine(p.Name);
    });
}

//orderbyQurable(context);

static void orderbyQurable(EfContext context)
{
    var orderbyquarable = from p in context.People
                          where p.Id > 5
                          orderby p.Id
                          select p;
    foreach (var item in orderbyquarable)
    {
        Console.WriteLine(item.Id + " " + item.Name + " " + item.LastName);
    }
}

///Then By
///Order By üzerinden yapılan sıralama işlemini birden fazla kolonda sağlar

//OrderByThenBy(context);
static void OrderByThenBy(EfContext context)
{
    var orderbyquarable = context.People
        .OrderBy(p => p.Name)
        .ThenBy(p => p.Id)
        .ToList();
    foreach (var item in orderbyquarable)
    {
        Console.WriteLine(item.Id + " " + item.Name);
    }
}

//OrderByDescending
//ThenByDescending

///Tekil Veri Getiren Sorgular
///SingleAsync
///Eğer Sorgu Sonucunda Birden Fazla Geliorsa exception fırlatır
//await SingleAsync(context);
static async Task SingleAsync(EfContext context)
{
    var p = await context.People.SingleAsync(u => u.Id == 2); ///>=
    Console.WriteLine(p.Name);
}

//SingleOrDefault
///Eğer Sorgu Sonucunda Birden Fazla Geliyorsa exception fırlatır hiç veri gelmez ise Null Değer Fırlatır.

//await SingleOrDefault(context);
static async Task SingleOrDefault(EfContext context)
{
    var p = await context.People.SingleOrDefaultAsync(u => u.Id >= 9); ///>=
    Console.WriteLine(p.Name);
}

///FirstAsync
///Sorgu Sonucunda Elde edilen Verilerden İlki gelir Eğer veri gelmiyorsa Hata Fırlatır..
//await FirstAsync(context);
static async Task FirstAsync(EfContext context)
{
    var p = await context.People.FirstAsync(u => u.Id == 8);
    Console.WriteLine(p.Name + " " + p.LastName);
}
//////////
///
//FirstOrDefault
///Sorgu Sonucunda Elde edilen Verilerden İlki gelir Eğer veri gelmiyorsa Null döner..
//await FirstOrDefault(context);
static async Task FirstOrDefault(EfContext context)
{
    var p = context.People.FirstOrDefault(u => u.Id == 5);
    Console.WriteLine(p.Name);
}


///FindAsync
///Urunun Benzersiz Olan Kolonunu çağırıyoruz. Primary Key Kolonuna hızlı bir şekilde sorgulama yapmayı sağlar.

//await FindAsync(context);

static async Task FindAsync(EfContext context)
{
    var p = await context.People.FindAsync(5);
    Console.WriteLine(p.Name + " " + p.LastName);
}


///Composite Primary Key Durumu

///LastAsync
///Order By Kullanılmalı 

//await LastAsync(context);
///Şartı sağlayaan en son değer gelir hic veri gelmiyorsa Exception Fırlatır.
static async Task LastAsync(EfContext context)
{
    var p = await context.People.OrderBy(u => u.Name).LastAsync(u => u.Id > 5);
    Console.WriteLine(p.Name);
}
///LastOrDefault
//await lastOrDefault(context);
static async Task lastOrDefault(EfContext context)
{
    //Şartı sağlayaan en son değer gelir hic veri gelmiyorsa Null Fırlatır.

    var p = await context.People.OrderBy(u => u.Name).LastAsync(u => u.Id > 5);
    Console.WriteLine(p.Name);
}

///CountAsync
//await CountAsync(context);
static async Task CountAsync(EfContext context)
{
    var p = (await context.People.ToListAsync()).Count();
    var p2 = await context.People.CountAsync(); ///integer döner
    Console.WriteLine(p);
    Console.WriteLine(p2);
}

///LongCountAsync
///Oluşturulan Sorgunun Sonucunda  execute edilmesi neticesinde kaç ,adet astırım elde edileceğini sayısal olarak (long) bizlere bildiren türdür..
//await LongCountAsync(context);
static async Task LongCountAsync(EfContext context)
{
    var p = await context.People.LongCountAsync();
    Console.WriteLine(p);
}

///AnyAsync 
///Sorgulanan veri geliyor mu bool turunde vAR MI Yok mu    
//await AnyAsync(context);
static async Task AnyAsync(EfContext context)
{
    var p = await context.People.AnyAsync(p => p.Id == 14);  //fundemental
    var p2 = await context.People.Where(u => u.Name.Contains("a")).AnyAsync(); //where kullanabilirsin
    var p3 = await context.People.AnyAsync(u => u.Name.Contains("a"));//Any nin içerisinde de yazabilirsin 
    Console.WriteLine(p + " " + p2 + " " + p3);
}
///MaxAsync
///Oluşturulan Sorguda verileN kOLONDA EN YÜKSEK DEĞER
//await MaxAsync(context);
static async Task MaxAsync(EfContext context)
{
    var personNumber = await context.People.MaxAsync(p => p.Id);
    var personEntity = context.People.SingleOrDefault(u => u.Id == personNumber);
    Console.WriteLine(personNumber + " " + personEntity.Name + " " + personEntity.LastName);

}
///MinAsync
//await MinAsync(context);
static async Task MinAsync(EfContext context)
{
    var p = await context.People.MinAsync(p => p.Id);
    Console.WriteLine(p);
}
///Distinct
///Sorguda tekrar eden kayıtlar varsa tekil kayıt getirir

//await Distinct(context); //!!
static async Task Distinct(EfContext context)
{
    var p = await context.People.Distinct().ToListAsync();
    p.ForEach(x =>
    {
        Console.WriteLine(x.Name);
    }
    );
}

///AllAsync
///Bir Sorgu neticesinde gelen verilerin,verilen şarta uyup uymadığını kontrol eder. true,false döner
//await AllAsync(context);


static async Task AllAsync(EfContext context)
{
    var person = await context.People.AllAsync(p => p.Id > 10);
    var person2 = await context.People.AllAsync(u => u.Name.Contains("a"));

    Console.WriteLine(person);
}

//Sum Fonksiyonu  :Toplam
///await SumAsync(context);

static async Task SumAsync(EfContext context)
{
    var fiyatToplam = await context.People.SumAsync(u => u.Id);
    Console.WriteLine(fiyatToplam);
}
///AverageAsync:Aritmetik Ortalama 
///await AverageAsync(context);
static async Task AverageAsync(EfContext context)
{
    var fiyatToplam = await context.People.AverageAsync(u => u.Id);
    Console.WriteLine(fiyatToplam);
}
//Contains

//await Contains(context);

static async Task Contains(EfContext context)
{
    var person = await context.People.Where(p => p.Name.Contains("can")).ToListAsync();
    person.ForEach(p =>
    {
        Console.WriteLine(p.Name);
    });
}
//StartsWith
///await StartsWith(context);

static async Task StartsWith(EfContext context)
{
    var person = await context.People.Where(p => p.Name.StartsWith("c")).ToListAsync();
    person.ForEach(p =>
    {
        Console.WriteLine(p.Name);
    });
}
///await EndsWith(context);
static async Task EndsWith(EfContext context)
{
    var person = await context.People.Where(p => p.LastName.EndsWith("l")).ToListAsync();
    person.ForEach(p =>
    {
        Console.WriteLine(p.Name + " " + p.LastName);
    });
}

//------------------------------------------------------------------------------------------------------------------------------------------------------//
//----------------------------------------------------------SORGU SONUCU DONUSUM FONKSİYONLARI----------------------------------------------------------//
//------------------------------------------------------------------------------------------------------------------------------------------------------//

//bU fONKSİYONLAR İLE SORGU SONUCUNDA ELDE EDİLEN VERİLERİ İSTEĞİMİZ DOIĞRULTUSUNDA FARKLI TÜRLERDE GÖRÜNTELEME SAĞLATABİLİRİZ
//ToDictionary :Pek kullanılmaz vt deki verileri dictionary şeklinde getirir

//await ToDictionaryAsync(context);
static async Task ToDictionaryAsync(EfContext context)
{
    var urunler = await context.People.ToDictionaryAsync(context => context.Id, context => context.Name);
    Console.WriteLine(urunler);
}

/*
 *ToList ile aynı amaca hizmet etmektedir.Yani,Oluşturulan Sorguyu execute edip neticesini alırlar.
 *ToList      :Gelen Sorgu neticesini entity türünde bir koleksiyona(List<TEntity>) dönüştürmekteyken,
 *ToDictionary:Gelen Sorgu neticesini Dictionary türnden bir koleksiyona dönüştürecektir.
 */



//ToArrayAsync 
//Oluşturulan Sorguyu dizi olarak elde eder
//ToList ile muadil amaca hizmet eder.Yani Sorguyu execute eder lakin gelen sonucu entity dizisiolarak elde eder 

///await ToArrayAsync(context);

static async Task ToArrayAsync(EfContext context)
{
    var urunler = await context.People.ToArrayAsync();
    Console.WriteLine(urunler);
}

//SELECT
//1: Select Fonksiyonu Generate edilecek Sorgunun Çekileceği Kolonlarını Ayarlamak için Kullanılır
///await Select(context);
static async Task Select(EfContext context)
{
    var people = await context.People
        .Select(u => new Person
        {
            Name = u.Name,
            //LastName= u.LastName,

        }).ToListAsync();
    people.ForEach(p =>
    {
        Console.WriteLine(p.Name + " " + p.LastName);
    });
}

//2: Select Fonksiyonu Gelen Verileri Farklı Türlerde karşılamamızı sağlar
//----------------------------------------------------------------------------------------------------------ac b adcjkszjvkğpdsövçkn<srıvxcö h vı
//var people = await context.People.Include(u=>u.Department)
//    .SelectMany(u => new Person
//    {
//        Name = u.Name,
//        //LastName= u.LastName,

//    }).ToListAsync();

//Group BY:Gruplama Yapmamızı sağlayan Fonksiyondur


//Default Convertion
/*Her iki entity de Navi Property ile birbiri ile  tekil referans ederek fiziksel bir ilişki olacağı tarif dilir
One To One ilişki türünde,dependent entity 'nin hangisi olduğunu belrleyebilmek pek kolay değildir.
Bu durumda fiziksel olarak bir foreign key'e karşılık  propoerty/kolon tanımlayarak çözebiliyoruz. 
Böylece foreign key'e karşılık property tanımlayarak lüzümsuz bir kolon oluşturmuş oluruz.

One To One
///Person
       public int Id { get; set; }
        
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? City { get; set; }

        public Address Address { get; set; }
////Address
      public int Id { get; set; }
        public String Adres { get; set; }
        public int PersonId { get; set; }//bunu ayırt ettirmek için yazıyoruz default Convertion
        public Person Person { get; set; }
One To Many :Bire çok default Convention işleminde fk tanımlama zorunluluğu yoktur
///Person
     public int Id { get; set; }
        
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? City { get; set; }
        public Company Company { get; set; }
        public Department Department { get; set; }
///Department
        public int Id { get; set; }
        public string DepartmentName { get; set; }

        public ICollection<Person> People { get; set; }

Many To Many

*/

// 2 Data Annotations
/*
 One To One
///Person
       public int Id { get; set; }
        
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? City { get; set; }

        public Address Address { get; set; }
////Address
    [Key,ForeignKey(nameof(Person))] //Data ANNOTATION
      public int Id { get; set; }
        public String Adres { get; set; }
        public Person Person { get; set; }
One To Many

    ///Person
       public int Id { get; set; }
 
    [ForeignKey(nameof(Department))]
       public int DId { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string? City { get; set; }

        public Address Address { get; set; }
////Address
    [Key,ForeignKey(nameof(Person))] //Data ANNOTATION
      public int Id { get; set; }
        public String Adres { get; set; }
        public Person Person { get; set; }

Many To Many
 */

///3 FluntAPI
///HasOne  :İlgili entity nin ilişkisel entity'ye birebir yada bire çok olacak şekilde ilişkisini yapılandırmaya başlayan metottur..
///HasMany :İlgili entity nin ilişkisel entity'ye çoka bir ya da çoka çok olacak şekilde ilişkisini yapılandırmaya başlayan metottur..
///WitOne  :HasOne ya da HasMany den Sonra bire bir ya da çoka bir olacak şekilde ilişkisini yapılandırmaya başlayan metottur.. 
///WitMany :HasOne ya da HasMany den Sonra bire çok ya da çoka çok olacak şekilde ilişkisini yapılandırmaya başlayan metottur.. 
/*
 * One To One 
Context nesnesi içerisine oluşturulan model builder içerisinde bu işlemler yürütülür
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().HasKey(c => c.Id);
            modelBuilder.Entity<Person>().HasOne(c => c.Address).WithOne(c => c.Person).HasForeignKey<Address>(c => c.Id); //birebir 
        }


*/
/*
 * One To Many
               modelBuilder.Entity<Person>()
                .HasOne(c => c.Department)
                .WithMany(d => d.People);
                //.HasForeignKey(c => c.DId); //Department Id DEĞİLDE DId benim fk m olacak dersen budur
 
 */

