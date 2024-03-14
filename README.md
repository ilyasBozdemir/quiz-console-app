# Quiz Uygulaması

Bu uygulama, kullanıcıların çeşitli sınav kitapçıklarından soruları çözebilecekleri ve cevap anahtarlarını görüntüleyebilecekleri bir konsol uygulamasıdır.

## Uygulamanın Akışı
1. Kullanıcılar, JSON formatında örnek veri girerek soru verilerini uygulamaya aktarır.

2. Uygulama, girilen verileri kullanarak farklı zorluk seviyelerinde kitapçıklar oluşturur.
3. Oluşturulan kitapçıklar, içerdikleri soruları ve seçenekleri karışık bir şekilde düzenler.
4.Kullanıcılar, oluşturulan kitapçıkları dışa aktarabilir veya quiz moduna geçerek soruları çözebilirler.

## Nasıl Kullanılır

1. Uygulamayı çalıştırın (`quiz-console-app.exe` veya Visual Studio'da F5 tuşuna basarak).
2. Uygulama başlatıldığında, mevcut kitapçıklar listelenecek ve kullanıcı bir kitapçık seçmek için talimatlar alacak.
3. Kitapçık seçildikten sonra, kullanıcı soruları çözebilir.
4. Soruları çözdükten sonra, kullanıcı cevap anahtarlarını görüntüleyebilir.

## Özellikler

- Kullanıcı dostu arayüz
- Farklı zorluk seviyelerine sahip sorular
- Soruların ve cevap anahtarlarının XML ve JSON olarak dışa aktarılması
- XML verilerinin XSLT ile biçimlendirilerek tarayıcıda görüntülenmesi
- Hata işleme ve güvenilirlik

## Kullanılan Teknolojiler

- C#
- .NET Framework
- JSON.NET kütüphanesi

## Kurulum

1. Bu depoyu klonlayın: `https://github.com/ilyasBozdemir/quiz-console-app`
2. Visual Studio'da çözümü açın: `quiz-console-app.sln`
3. Uygulamayı derleyin ve çalıştırın.

## Katkıda Bulunma

1. Bu depoyu çatallayın (fork).
2. Yeni bir dal (branch) oluşturun: `git checkout -b new-feature`
3. Değişikliklerinizi yapın ve bunları işleyin (commit): `git commit -am 'Yeni özellik ekle'`
4. Dalınızı ana depoya gönderin (push): `git push origin yeni-ozellik`
5. Bir birleştirme isteği (pull request) gönderin.

## Lisans

Bu proje MIT lisansı altında lisanslanmıştır. Daha fazla bilgi için `LICENSE` dosyasına bakın.
