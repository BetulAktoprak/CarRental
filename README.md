# CarRental

## Giriş

Bu proje, araç kiralama firması için araçların aktif çalışma sürelerini, bakım sürelerini ve boşta bekleme sürelerini hesaplayarak görsel ve tablolu raporlar sunmak amacıyla geliştirilmiştir. Proje, modern yazılım geliştirme prensiplerine uygun olarak tasarlanmış olup ASP.NET Core MVC platformunda inşa edilmiştir.

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
