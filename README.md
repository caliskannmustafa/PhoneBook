# PhoneBook

![Phonebook](https://user-images.githubusercontent.com/22472702/117715473-fd931e80-b1e0-11eb-8fa7-3d5b1b7d6952.png)

Mikro Servis altyapısı ile hazırlanmış olan Phonebook sistemi içerisindeki projeler ve özellikleri kısaca şu şekildedir:

**Phonebook.Presentation**
Son kullanıcının etkileşimini sağlamak için hazırlanmış önyüz projesidir. People ve Reports olmak üzere 2 menü seçeneği sunulur.\
People bölümü rehberde saklanacak kişilerin ve onlara ait iletişim bilgilerinin yönetildiği kısımdır.\
Report bölümü ise rehberde bulunan kişilere ait sistemsel raporların kullanıcıya sunulduğu bölümdür.\
Her 2 bölüm de arkaplan sorgulamalarını Phonebook.Gateway uygulaması aracılığı ile sağlamaktadır. Projede yer alan appsettings.json dosyası içerisinde Phonebook.Gateway uygulamasının base adresi(ApiBaseAddress) bulunmaktadır. Phonebook.Gateway adresi değişirse bu parametre üzerinde de değişiklik yapılması gerekmektedir.

**Phonebook.Gateway**
Phonebook.Presentation uygulamasından gelen istekleri Phonebook.Main,Phonebook.Report ve Phonebook.DataGenerator mikro uygulamalarına yönlendiren ara birimdir. Ocelot kütüphanesini kullanmaktadır. Yönlendirme ile ilgili konfigurasyonlar ocelot.json dökümanı içerisinde yer almaktadır. People bölümünden gelen istekler Phonebook.Main uygulamasına, Report bölümünden gelen istekler de Phonebook.Report uygulamasına yönderilecek şekilde düzenleme yapılmıştır. People Listeleme sayfasında bulunan Generate Dummy Data fonksiyonu ise Phonebook.DataGenerator uygulamasına yönlendirilmiştir.

**Phonebook.Main**
Rehber sistemi ile ilgili ana verilerin işlendiği mikroservistir. Kullanıcı ve İletişim bilgilerini PhonebookDatabase isimli PostgreSql database'i üzerinde saklamaktadır. Uygulama lokal PostgreSql sunucusuna bakacak şekilde konfigure edilmiştir. Uygulamanın çalışabilmesi için appsettings.json altında bulunan ConnectionStrings alanından çalışılmak istenen PostgrSQL sunucusu bağlantı bilgilerinin girilmesi gerekmektedir. Uygulamada db modeli olarak CodeFirst kullanılmıştır ve uygulama ayağa kaldırıldığında database şablonu otomatik olarak oluşturulacaktır. {ip:port}/swagger/index.html ile servisler detaylı olarak görülebilir.

Ayrıca, sisteme girilen rapor taleplerinin sonuçları bu mikroservis tarafından hazırlanmaktadır. Bu işlem için Phonebook.Main altında çalışmakta olan bir backgroundservice "report_create" isimli queue için consumer pozisyonundadır. Bu queue üzerinden bir mesaj yayınlandığı zaman backgroundservice devreye girerek ilgili raporun hazırlanmasını sağlar ve rapor sonucunu "repor_result" isimli queue'ya publish eder. 

Aynı zamanda "data_generate" isimli queue için consumer pozisyonundadır.Bu queue üzerinden bir mesaj yayınlandığı zaman backgroundservice devreye girerek fake dataların oluşturularak database'e kayıt edilmesini sağlar.
RabbitMq serverı olarak bir cloudserver kullanılmaktadır.

**Phonebook.Report**
Rapor ile ilgili ana verilerin işlendiği mikroservistir. Rapor ve rapor detay bilgilerini PhonebookReportDatabase isimli PostgreSql database'i üzerinde saklamaktadır. Uygulama lokal PostgreSql sunucusuna bakacak şekilde konfigure edilmiştir. Uygulamanın çalışabilmesi için appsettings.json altında bulunan ConnectionStrings alanından çalışılmak istenen PostgrSQL sunucusu bağlantı bilgilerinin girilmesi gerekmektedir. Uygulamada db modeli olarak CodeFirst kullanılmıştır ve uygulama ayağa kaldırıldığında database şablonu otomatik olarak oluşturulacaktır. {ip:port}/swagger/index.html ile servisler detaylı olarak görülebilir.

Ayrıca, sisteme girilen rapor taleplerinin istekleri bu mikroservis tarafından hazırlanmaktadır. Kullanıcıdan gelen yeni rapor talebi "report_create" isimli queue'ya publish edilir.Phonebook.Main tarafından oluşturulan sonuçların alınabilmesi için Phonebook.Report altında çalışmakta olan bir backgroundservice "report_result" isimli queue için consumer pozisyonundadır. Bu queue üzerinden bir mesaj yayınlandığı zaman backgroundservice devreye girerek ilgili raporun sonucunu database'e kaydetmektedir.

**Phonebook.DataGenerator**
Dummy data üretim işleminin yönetildiği mikroservistir. Kullanıcı tarafından girilen Dummy Data oluşturma taleplerinin "data_generate" isimli queue'ya publish edilmesini sağlamaktadır. RabbitMq serverı olarak bir cloudserver kullanılmaktadır.

**Phonebook.EventBus**
Phonebook.Main,Phonebook.Report ve Phonebook.DataGenerator uygulamaları arasında veri alışverişinin yapıldığı katmandır. RabbitMq kütüphanesi ve uygulaması üzerinden mesaj alışverişi sağlanmaktadır.

**Eklenecekler**
Projede şu anda bulunmayan ancak eklenmesi gereken bazı ana fonksiyonlar ile proje zenginleştirilebilir:
- Authentication
- Authorization
- Logging
- Dashboard


