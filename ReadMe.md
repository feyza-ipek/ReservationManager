# Welcome to ReservationApplication!

Bu uygulama bir işletmedeki rezervasyon süreçlerinin yönetilmesi için geliştirilmiştir.
.Net7 teknolojisi ile geliştirilmiş olup Onion Design Pattern kullanılmıştır. EntityFramework ORM ile çalışılıp veritabanı süreci InMemory ile yürütülmüştür. Projenin test süreçlerinde unit ve integration testler xUnit ve MsUnit ile sağlanmıştır. Proje geliştirme sürecinde DependencyInjection, CleanCode prensiplerine dikkat edilmiştir.


# Katmanlar

## Domain Katmanı

Entities, Request Response Models, Configuration Models gibi çeşitli POCO nesnelerinin bulunduğu katmandır. Onion patern presibi ile bu katmanda hiç bir iş mantığı içeren metot geliştirilmez.

## Persistence Katmanı

Veritabanı migration operasyonlarının döndüğü katman olup aynı zamanda DbContext’i de içinde barındırmaktadır. Up_Migration ve Down_Migration bu katmanda oluşturulur ve yönetilir.

## Repository Katmanı
Repository Katmanı Sql Operation Metotlarının Bulunduğu Katmandır. Persistence Katmanı ile Bağımlıdır. 

## Service Katmanı
Servis Katmanında iş mantık süreçlerinin döndüğü ancak database işlemlerinin kesinlikle olmadığı işlemdir. Repository Katmanı ile ilişkilidir. Repositoryden Entity dönüşleri response modellere maplenir. Servisler Geri dönüşlerde entity kullanmaması gerekir.

## Presentation Katmanı
Uygulamada **Sunum** katmanı olarak görev yapan bu katman UserInterfaces(UI) olarakta bilinmektedir. Kullanıcının iş mantıklarına veri gönderme işlemlerinde bu katman sayesinde veri internet sisteminden anlamlandırılıp object oriented kurallarınca request modellere maplenir. Bu işlemler ModelBinderlar tarafından gerçekleştirilir. Bu Katman Servis Katmanına Yani **Business Logic**(İş Mantığı) Katmanı ile ilişkilidir.

##  Test Katmanı
Test Katmanı developer testlerinin yapıldığı katman olup bazı katmanlarla friendlyassembly yaklaşımı sergilemektedir. Bundan dolayı Service veya Presentation Katmanları ile bu kalıtım modeli için konuşabiliriz. Örnek Projede Bu Yaklaşım Api Katmanında Gözlenmektedir.

## Infrastructure Katmanı
Bu Katman öncelikle service katmanına hizmet vermektedir (Referans Olmaktadır). İş mantıklarında Sms gönderme, e-posta gönderme veya dış servis ilişkisi var ise dış servis entegrasyon süreçleri bu katmanda yer alır ancak birbirlerine sıkı sıkıya bağımlı değildir. Örnek Projede Dependency Injection ile bağımlılık sorunu giderilmiştir.

## Teknoloji ve Yaklaşımlar

* [ASP.NET Core 7](https://learn.microsoft.com/tr-tr/dotnet/whats-new/dotnet-7-docs)

* [Microsoft DI](https://learn.microsoft.com/tr-tr/dotnet/core/extensions/dependency-injection)
* [OnionArchitecture](https://www.gencayyildiz.com/blog/nedir-bu-onion-architecture-tam-teferruatli-inceleyelim/)
* [xUnit](https://medium.com/@e.karabudak7/net-core-ile-unit-test-xunit-eaf0d94edd71#:~:text=xUnit%2C%20.,y%C3%B6ntem%20sa%C4%9Flayan%20bir%20test%20framework%C3%BCd%C3%BCr.)
* [Moq](https://ismailkasan.medium.com/asp-nette-moq-k%C3%BCt%C3%BCphanesi-ile-unit-test-yaz%C4%B1m%C4%B1-74a1b108b41d)
* [MSTest](https://medium.com/@e.karabudak7/net-core-ile-unit-test-xunit-eaf0d94edd71#:~:text=xUnit%2C%20.,y%C3%B6ntem%20sa%C4%9Flayan%20bir%20test%20framework%C3%BCd%C3%BCr.)
* [Entity Framework Core 7](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/whatsnew)
* [EntityFramework InMemoryDatabase](https://learn.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=dotnet-core-cli#supported-database-engines)
* [Microsoft.Asp.Net.Mvc.Test](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/unit-testing/creating-unit-tests-for-asp-net-mvc-applications-cs)

* ## Destek

Eğer sorunuz yada hata durumları oluşursa  [Yeni İş Tanımlaması](https://github.com/feyza-ipek/ReservationManager/issues/new) yapınız..
