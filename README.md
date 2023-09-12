
# LibraryCore

Bir kütüphanede (kitap ödünç alma - iade etme - Kitapları durumlarına göre listeleme ...) ihtiyaç duyulabilecek kullanıcı ve admin taraflı gereksinimleri içeren web projesi.


## Ortam Değişkenleri

Bu projeyi çalıştırmak için aşağıdaki ortam değişkenlerini (Nuget Paketlerini) Projenize eklemeniz gerekecek

-Microsoft.EntityFrameworkCore ->>https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/6.0.0?_src=template

    
-Microsoft.EntityFrameworkCore.Design ->>https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/6.0.0?_src=template
+Oluşturulan sınıfların Migration İşlemleri Ve veritabanına doğru sınıfların doğru şekilde oluşturulabilmesi işine yarar.


-Microsoft.AspNetCore.Authentication.Cookies ->>https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.Cookies/2.1.0?_src=template
+Kullanıcı bilgilerini tutup Bağlantıyı belirtilen süre açık tutar. Tekrar giriş yapmaya gerek kalmaz.


-Microsoft.EntityFrameworkCore.SqlServer ->>https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/6.0.0?_src=template
+Veri tabanı sorgularını oluşturmak, veri tabanına sorguları göndermek ve sonuçları almak için SQL Server ile iletişim kurar.


-Microsoft.EntityFrameworkCore.Tools ->>https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/6.0.0?_src=template
+Package Manager üzerinden (Add-Migration - Remove-Migration - Update-database) Komutlarını kullanabilmek için gerekli nuget paketi.


-FluentValidation.AspNetCore ->>https://www.nuget.org/packages/FluentValidation.AspNetCore/11.3.0?_src=template
+Kullanıcı girdilerinin Sınıflara göre belirlenen kurallara uymadığı durumlarda hata fırlatır. Ör:Kullanıcı adı minimun 4 karakter olmalıdır , Geçerli Bir Mail adresi Girin.

-RoverCore.ToastNotification ->>https://www.nuget.org/packages/RoverCore.ToastNotification/1.2.3?_src=template
+Crud işlemlerinde kullanıcıya bildirim vermek için kullanılan Nuget paketi.


-Serilog.AspNetCore ->>https://www.nuget.org/packages/Serilog.AspNetCore/7.0.0?_src=template
+Hataları Try - Catch blokları içerisinde yakalamak  ve günlük Log kayıtları tutmak için Nuget paketi.


-Serilog.Sinks.File ->>https://www.nuget.org/packages/Serilog.Sinks.File/5.0.0?_src=template
+Serilog ile yakalanan hataları ve logları Dosyaya yazmak için Nuget paketi.

  
## Kullanılan Teknolojiler

**IDE:** Visual Studio 22


**Frameworks:** .Net CORE 6 MVC - EntityFramework 6


**DataBase:** MSSql 19

  
**Frontend:** Html - Css - Js - Boostrap


**DesignPattern:** Repository Design Pattern

**Loglama:** Loglar Proje çözümü içerisinde "logs" klasörü altında txtlerde tutulmaktadır.

## Özellikler

    Kullanıcı
- Kitap adına göre arama.
- Alfabetik kitap listeleme.
- Kitap ödünç alma.
- Ödünç alınan Kitabı iade etme.
- Profil bilgilerini güncelleme.
- Başkası tarafında ödünç alınmış-Ödünç alınamayan kitapları listeleme.

## Özellikler

    Yönetici

- Kitapları alfabetik listeleme

- Yeni kitap ekleme - silme - güncelleme

- Ödünç Durumdaki kitap ayrıntılarını görme.
- Önceden ödünç verilmiş ve iade edilmiş kitapların listesini görme.
- Kullanıcıları listeleme - düzenleme - silme.
- Yeni kullanıcı ekleme.
- Yazar ekleme - Silme - Güncelleme.
- Kitap Türü ekleme - silme - güncelleme.
- Personel - Kullanıcı pozisyonlarındaki kullanıcı sayısını görme.
- Kitap sayısı - alınan kitap sayısı - yazar sayısı - kullanıcı sayısı - kitap türü sayısı - kullanıcı pozisyon sayısı İstatistiklerini görme.
## Ekran Görüntüleri Kullanıcı
- Login
![ Login  ](https://github.com/yas1n09/LibraryCore/assets/26649664/ce83c582-3087-49b5-b53d-a8df80fb6773)

- kitaplar
![user- List-Borrow Books](https://github.com/yas1n09/LibraryCore/assets/26649664/318a0206-97e1-4209-bee2-db8647191f1d)

- Ödünç alınmış kitaplar
![Ödünç Alınmış kitaplar](https://github.com/yas1n09/LibraryCore/assets/26649664/a5b66eb4-4816-4783-a36d-e450a998edcc)

- Profil -Kitap İade
![Profil kitap iade](https://github.com/yas1n09/LibraryCore/assets/26649664/49418873-e226-425b-9e7d-424035c23066)


## Ekran Görüntüleri Admin

- Kitaplar Ve kitap işlemleri
![resim](https://github.com/yas1n09/LibraryCore/assets/26649664/ac386499-237b-45f5-8e06-12e468c3ccea)

- Yeni Kitap Ekle
![resim](https://github.com/yas1n09/LibraryCore/assets/26649664/c1d16ccd-e1cf-4276-983e-80b565621840)

- Alınan Kitaplar
![resim](https://github.com/yas1n09/LibraryCore/assets/26649664/e85a693f-aa2b-437c-b4f4-7b580ee96597)

- İade Edilmiş Kitaplar
![resim](https://github.com/yas1n09/LibraryCore/assets/26649664/46af37b6-83d4-48be-9b29-08b854a0bbbc)

- Yazarlar ve yazar işlemleri
![resim](https://github.com/yas1n09/LibraryCore/assets/26649664/2dfd345a-6d38-44e8-8d78-2180bbe88fb2)

- Yazar Ekle
![resim](https://github.com/yas1n09/LibraryCore/assets/26649664/90675f67-a1d4-47b6-bab2-6ba3c1b83230)
- Kitap Türleri - Kitap Türü ekle ve işlemleri
![resim](https://github.com/yas1n09/LibraryCore/assets/26649664/6e014127-46c7-4a2c-8035-5da0b4220e50)
- Pozisyonlar
![resim](https://github.com/yas1n09/LibraryCore/assets/26649664/fbf2729f-db64-4c0d-8b55-ac94d8c2c155)

- İstatistikler
![resim](https://github.com/yas1n09/LibraryCore/assets/26649664/90cce5a6-a972-4db9-8f48-fc6545fa50b0)

## Ekran Görüntüleri Veri Tabanı Tablo Yapıları
- Yazarlar
![resim](https://github.com/yas1n09/LibraryCore/assets/26649664/da751fe2-dea1-47d5-9ce0-806a708e345e)

- Kitaplar
![resim](https://github.com/yas1n09/LibraryCore/assets/26649664/dd6e6d44-eebb-4cf2-8249-cd44965d1dfc)

- Ödünç alınan kitaplar
![resim](https://github.com/yas1n09/LibraryCore/assets/26649664/fe8718a7-638a-4172-a1fb-f42fd5b30b78)

- Pozisyonlar
![resim](https://github.com/yas1n09/LibraryCore/assets/26649664/0a1e2df8-b5c4-4f8d-9277-577973b766d2)

- Türler
![resim](https://github.com/yas1n09/LibraryCore/assets/26649664/9b968807-9069-4c2c-906a-365ffbb50f2e)

- Kullanıcılar
![resim](https://github.com/yas1n09/LibraryCore/assets/26649664/2772ffd0-4a69-46e3-a11b-40857b46bd33)


## Database Diagram
![resim](https://github.com/yas1n09/LibraryCore/assets/26649664/c59a1036-fe53-4426-ab21-1504074cd1ea)
