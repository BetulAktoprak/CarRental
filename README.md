# Araç Kiralama Sistemi

*Bu proje, araç kiralama firması için araçların aktif çalışma sürelerini, bakım sürelerini ve boşta bekleme sürelerini hesaplayarak görsel ve tablolu raporlar sunmak amacıyla geliştirilmiştir. Proje, modern yazılım geliştirme prensiplerine uygun olarak tasarlanmış olup ASP.NET Core MVC platformunda inşa edilmiştir.*

![Proje Tanıtımı](./AracIslemleri.gif)

## Projede Kullanılan Teknolojiler

- Programlama Dili: C#
- Framework: ASP.NET Core MVC
- Veritabanı: Microsoft SQL Server
- Frontend Teknolojileri: HTML5, CSS3, Chart.js, Bootstrap
- Araçlar: Visual Studio
- Tasarım Prensipleri: Katmanlı Mimari, Responsive Tasarım

## Proje İçeriği
### Görevler
- Araç Kayıt Modülü: Araçların adı ve plakalarının kaydedilmesi.
- Kullanıcı Süre Girişi: Kullanıcı Aktif çalışma süresini ve bakım süresini kaydetmesi.
- Çalışma Süresi Hesaplama: 7 gün ve 24 saat esasına göre araçların aktif çalışma süresinin ve boşta kalma süresinin hesaplanması.
- Grafik ve Listeleme: Araçların aktif çalışma sürelerinin listelenmesi ve görsel olarak sunulması.
- Rollere Dayalı İşlevsellik: Admin ve kullanıcı rolleriyle farklı işlem yetkilendirmeleri.
### Hesaplama Yöntemleri
- Aktif Çalışma Yüzdesi: ((Aktif Çalışma Süresi) / (7x24)) * 100
- Boşta Bekleme Yüzdesi: (((7x24) - (Bakım Süresi + Aktif Çalışma Süresi)) / (7x24)) * 100
- Boşta Bekleme Süresi: ((7x24) - (Bakım Süresi + Aktif Çalışma Süresi)) 

## Geliştirme Süreci

### Backend: Oluşturulan Katmanlar

- **Business**
- **Core**
- **DataAccess**
- **Presentation**

---

### Katmanlardaki Klasörler ve İçerikleri

#### Core Katmanı

- **Abstractions**:  
  Ortak kullanılan property'lerin bulunduğu Entity sınıfı yer alır.

- **Entities**:  
  Sınıfların olduğu klasör. (Car, Roles, User, WorkTime)

- **Repositories**:  
  Metotların imzalarının bulunduğu interface’ler yer alır.  
  (ICarRepository, IUserRepository, IWorkTimeRepository, IGenericRepository)  
  - **CRUD işlemleri.**
  - **Araçlara ait kullanıcıları çekmek için yazılan metodun imzası.**
  - `AuthenticateAsync`: Login işlemi sırasında kullanıcının kayıtlı olup olmadığına bakan metodun imzası.
  - `RegisterAsync`: Kullanıcı kayıt işlemi için yazılan metodun imzası.
  - `GetUserCarsAsync`: Giriş yapan kullanıcıya ait araç bilgileri alınması için yazılan metodun imzası.
  - `GetUserWithCarsAndWorkTimesAsync`: Kullanıcıya ait araç bilgileri üzerinden araçlara ait süreleri girebilmesi adına yazılan metodun imzası.
  - `GetAllWithFilterAsync`: Raporlama için kullanılacak olan WorkTime ve Car sınıfları üzerinde işlem yapabilmek adına yazılmış metodun imzası.

- **Services**:  
  Servis interface'leri yer alır.  
  (ICarService, IUserService, IWorkService, IService)

- **UnitOfWorks**:  
  SaveChanges metodunun interface’i yer alır.

---

#### DataAccess Katmanı

- **Configurations**:  
  Entity Framework yapılandırmalarını içerir.  
  (CarConfiguration, UserConfiguration, WorkTimeConfiguration)

- **Context**:  
  AppDbContext sınıfı yer alır. Veritabanı ile bağlantıyı sağlayan DbContext sınıfını içerir.

- **Repositories**:  
  Veritabanı işlemleri için repository implementasyonlarının yapıldığı sınıflar yer alır.  
  (CarRepository, UserRepository, WorkTimeRepository, GenericRepository)

- **UnitOfWorks**:  
  IUnitOfWork sınıfının implementasyonunun yapıldığı sınıf yer alır.

---

#### Business Katmanı

- **Services**:  
  Core Katmanı'nda tanımlanan Service Interface'lerinin implementasyonlarını içerir.  
  Repository sınıfları kullanılarak veritabanı işlemleri gerçekleştirilir.  
  (CarService, UserService, WorkTimeService, Service)

- **Validations**:  
  Validasyon kurallarının yazıldığı sınıflar yer alır.  
  (CarValidator, UserValidator, WorkTimeValidator, LoginValidator)

---

#### Presentation Katmanı

- **Controllers**:
  - **CarController**:  
    Admin kullanıcısının araç adları ve plakaları üzerine işlemler yaptığı action metotları yer alır.  
    (Listeleme, Ekleme, Düzenleme, Silme)

  - **HomeController**:  
    Kullanıcı kendisine ait olan araçlar üzerinde işlemler yapabildiği action metotlar yer alır.  
    - `ListUserCar`: Kullanıcıya atanan araç listesi gelir.
    - `UserCar`: Kullanıcıya atanan araçlarının ayrıntılı listesi yer alır.  
      Burada aktif çalışma süresi ve bakım süresi için düzenleme ve ekleme sayfasına geçiş yapabilir.
    - `CreateUserCar`: Kullanıcı kendisine atanan araçlara aktif çalışma süresi ve bakım süresi ekler.
    - `EditUserCar`: Kullanıcı girmiş olduğu aktif çalışma süresi ve bakım süresini düzenleyebilir.

  - **LoginController**:  
    Giriş ve Kayıt action metotları yer alır.  
    (Login, Register)  
    > Not: Kayıt yapıldığı zaman kullanıcı "user" rolüne atanır.

  - **ReportController**:  
    Admin kullanıcısının görüntüleyebileceği tablo ve grafiklerin action metotları yer alır.  
    Tüm hesaplamalar `GetWeeklyWorkTimeAsync()` metodu ile yapılmıştır.  
    Grafikler için **chart.js** kullanılmıştır.  
    - **Index**:  
      Araçların haftalık toplam aktif çalışma süresi, bakım süresi ve boşta bekleme süresi tablosu yer alır.
    - **ActiveWorkChart**:  
      Araçların haftalık aktif çalışma yüzdesi tablo ve sütun grafiği ile gösterilmiştir.
    - **IdleTimeChart**:  
      Araçların haftalık boşta kalma yüzdesi tablo ve pasta dilimi grafiği ile gösterilmiştir.

- **Models**:  
  View ile çalışacak modellerin tanımlandığı ViewModel sınıfları yer alır.

- **appsettings.json**:  
  Sql Connection string dizesi bu kısıma yazılmıştır.

- **program.cs**:  
  Admin kullanıcısının sistem kayıt bilgileri burada yer almaktadır.

---

## Genel Mimari Açıklaması

- **Katmanlar Arası İletişim**:  
  - Presentation katmanı, Business katmanı ile iletişim kurar.  
  - Business katmanı, gerekli işlemleri yapmak için DataAccess ve Core katmanlarını kullanır.  
  - Core katmanı, diğer katmanlara altyapı ve genel işlevsellik sağlar.
