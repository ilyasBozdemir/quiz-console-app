namespace QuizAppConsole.Constants;

public class AppConstants
{
    #region URLs

    public const string JSON_API_URL = "https://quiz-app-data-api.vercel.app/api/get-questions";

    #endregion

    #region Directories

    public const string BASE_DIRECTORY = @"D:\QuizConsoleApp";

    #endregion

    #region FileNames

    public const string XML_FORMAT_NAME = "XML";
    public const string JSON_FORMAT_NAME = "JSON";

    #endregion

    #region QuestionPrompts

    public const string EXPORT_BOOKLET_QUESTION = "Kaç kitapçık dışa aktarılacak?: ";
    public const string CHOOSE_SELECTION_PROMPT = "Seçiminizi yapın";

    #endregion

    #region Success Messages

    public const string QUIZ_STARTED_SUCCESS_MESSAGE = "Quiz başarıyla başlatıldı.";
    public const string QUIZ_COMPLETED_MESSAGE = "Quiz tamamlandı.";
    public const string INVALID_ANSWER_ENTRY_MESSAGE = "Geçersiz bir cevap girişi yaptınız. Lütfen sadece doğru bir cevap belirtin.";
    public const string INVALID_OR_UNDEFINED_QUESTION_MESSAGE = "Soru geçersiz veya tanımsız.";
    public const string ANSWER_KEYS_LOADED_SUCCESS_MESSAGE = "Cevap anahtarları doğru bir şekilde yüklendi.";
    public const string QUIZ_COMPLETED_SUCCESS_MESSAGE = "Quiz başarıyla sonlandırıldı.";
    public const string QUIZ_ERROR_OCCURRED_MESSAGE = "Quiz sırasında bir hata oluştu. Lütfen daha sonra tekrar deneyin.";
    public const string QUIZ_TERMINATED_MESSAGE = "Quiz sonlandırıldı. Sonuçlarınızı görmek için ana menüye dönün.";
    public const string QUESTIONS_LOADED_SUCCESS_MESSAGE = "Sorular başarıyla yüklendi.";
    public const string QUIZ_TIME_EXPIRED_MESSAGE = "Quiz süresi doldu. Quiz sonlandırılıyor.";
    public const string QUIZ_TIME_NOT_EXPIRED_MESSAGE = "Quiz süresi dolmadı, ancak sonlandırmak istiyor musunuz? (E/H)";
    public const string BOOKLET_EXPORT_SUCCESS_MESSAGE_TEMPLATE = "'{0}' kitapçığı başarıyla JSON olarak dışa aktarıldı. Dosya yolu: {1}";
    public const string ANSWER_KEYS_EXPORT_SUCCESS_MESSAGE_TEMPLATE = "'{0}' kitapçığının cevap anahtarları başarıyla JSON olarak dışa aktarıldı. Dosya yolu: {1}";

    #endregion

    #region Warning Messages

    public const string ANSWER_KEYS_NOT_FOUND_MESSAGE = "Cevap anahtarları bulunamadı.";
    public const string INVALID_QUESTION_NUMBER_MESSAGE = "Geçerli bir soru numarası giriniz.";

    #endregion

    #region Error Messages

    public const string FILE_LOAD_ERROR_MESSAGE_TEMPLATE = "{0} dosyası yüklenirken bir hata oluştu.";
    public const string FILE_FORMAT_ERROR_MESSAGE_TEMPLATE = "{0} biçiminde bir hata oluştu: {1}";
    public const string OPTION_RANGE_ERROR_MESSAGE_TEMPLATE = "Seçenek Id {0} ile {1} arasında olmalıdır";
    public const string INPUT_MUST_BE_LETTER_ERROR_MESSAGE = "Giriş bir harf olmalıdır.";
    public const string INVALID_EXPORT_TYPE_ERROR_MESSAGE = "Geçersiz dışa aktarma türü.";
    public const string EXPORT_ERROR_MESSAGE_TEMPLATE = "Dışa aktarma sırasında bir hata oluştu: {0}";
    public const string INVALID_SELECTION_ERROR_MESSAGE = "Geçersiz seçim. Lütfen geçerli bir seçenek girin.";
    public const string INVALID_SELECTION_ERROR_MESSAGE_TEMPLATE = "Invalid selection. Please enter {0}.";
    public const string INVALID_SELECTION_MESSAGE = "Geçersiz seçim. Lütfen listedeki bir seçeneği seçin.";
    public const string INVALID_INPUT_MESSAGE = "Geçersiz Giriş. Lütfen bir numara giriniz.";
    public const string GENERAL_FILE_NOT_SPECIFIED = "{0} dosya yolu veya içeriği belirtilmedi.";
    public const string INVALID_CORRECT_OPTION_COUNT_TEMPLATE = "{0}. Soru için doğru seçenek sayısı geçersiz. Yalnızca bir doğru seçenek sunulmalıdır.";
    public const string INVALID_INCORRECT_OPTION_COUNT_TEMPLATE = "{0}. Soru için yanlış seçenek sayısı geçersiz. Yanlış seçenek olmamalıdır.";
    public const string UNEXPECTED_FILE_CONTENT_MESSAGE_TEMPLATE = "Alınan medya türü dosya içeriği beklenmiyor. Alınan medya türü: {0}";
    public const string DATA_FETCH_ERROR_MESSAGE_TEMPLATE = "Veriler API'den alınamadı. Hata kodu: {0}";
    public const string ERROR_OCCURRED_MESSAGE = "Bir hata oluştu: {0}";
    public const string BOOKLET_ID_NOT_FOUND_ERROR_MESSAGE_TEMPLATE = "Hata: Beklenen kitapçık ID bulunamadı. Beklenen ID: {0}";
    public const string QUESTIONS_LOAD_FAILED_MESSAGE = "Sorular yüklenirken bir hata oluştu. Kaynakta sorun olabilir veya boş bir yanıt döndü.";
    public const string FILE_DESERIALIZE_ERROR_MESSAGE = "Dosya ayrıştırılırken bir hata oluştu. Dosya formatını kontrol edin ve tekrar deneyin.";

    #endregion

    #region ContentTypes

    public const string APPLICATION_JSON = "application/json";
    public const string APPLICATION_XML = "application/xml";
    public const string TEXT_XML = "text/xml";
    public const string TEXT_HTML = "text/html";
    public const string TEXT_PLAIN = "text/plain";
    public const string APPLICATION_JAVASCRIPT = "application/javascript";
    public const string TEXT_JAVASCRIPT = "text/javascript";
    public const string TEXT_CSS = "text/css";
    public const string IMAGE_JPEG = "image/jpeg";
    public const string IMAGE_PNG = "image/png";
    public const string IMAGE_GIF = "image/gif";
    public const string APPLICATION_PDF = "application/pdf";
    public const string APPLICATION_ZIP = "application/zip";

    #endregion

    #region Labels

    public const string BLANK_ANSWER = "  - Blank";
    public const string CORRECT_ANSWER = "  - Correct";
    public const string INCORRECT_ANSWER = "  - Incorrect";
    public const string BOOKLET_ID_LABEL = "Kitapçık Id: ";
    public const string BOOKLET_NAME_LABEL = "Kitapçık Adı: ";
    public const string QUESTION_LABEL = "Soru: ";
    public const string ANSWER_KEYS_TITLE = "Cevap Anahtarları:";
    public const string QUESTION_ID_LABEL = "Soru  Id: ";
    public const string CORRECT_OPTION_LABEL = "Doğru Seçenek";
    public const string DEFAULT_BOOKLET_NAME = "BOOKLET";
    public const string ANSWERS_LABEL = "Cevaplar:";

    #endregion

    #region Messages

    public const string QUESTIONS_ARE_BEING_FETCHED_MESSAGE = "Sorular Sunucudan Alınıyor.";
    public const string QUESTIONS_FETCHED_SUCCESSFULLY_MESSAGE = "Sorular Sunucudan Başarıyla Alındı";
    public const string NETWORK_LOAD_FAILED_MESSAGE = "Kaynak sorular yüklenemedi. Lütfen bağlantınızı kontrol edin veya daha sonra tekrar deneyin.";

    #endregion

    #region Instructions

    public const string EXIT_INSTRUCTION = "Çıkmak için Enter'a basın.";

    #endregion

    #region ModeTitles

    public const string QUIZ_MODE_TITLE = "Quiz Çözme Modu";
    public const string DATA_MANIPULATION_MODE_TITLE = "Verileri Oluşturma ve Dışa Aktarma Modu";
    public const string CREATE_AND_EXPORT_FILE_TITLE_TEMPLATE = "Kitapçık Oluştur ve {0} Olarak Dışa Aktar";
    public const string RETURN_TO_MAIN_MENU = "Ana Menüye Dön";
    #endregion

    #region ResultMessages

    public const string TOTAL_QUESTION_COUNT_MESSAGE = "Toplam Soru Sayısı:";
    public const string CORRECT_COUNT_MESSAGE = "Doğru Sayısı:";
    public const string INCORRECT_COUNT_MESSAGE = "Yanlış Sayısı:";
    public const string INCORRECT_ANSWER_PENALTY_MESSAGE = "Incorrect Answer Penalty:";
    public const string BLANK_COUNT_MESSAGE = "Boş Sayısı:";
    public const string NET_COUNT_MESSAGE = "Net: ";
    public const string SUCCESS_RATE_MESSAGE = "Başarı Yüzdesi:";

    #endregion

    #region UserInfoTitles

    public const string QUIZ_INFO_TITLE = "Quiz Bilgileri:";
    public const string QUIZ_CREATOR_LABEL = "Oluşturan: ";
    public const string QUIZ_TITLE_LABEL = "Başlık: ";
    public const string QUIZ_DESCRIPTION_LABEL = "Açıklama: ";
    public const string USER_INFO_TITLE = "Kullanıcı Bilgileri: ";
    public const string USER_ID_LABEL = "Kullanıcı ID: ";
    public const string USER_FULLNAME_LABEL = "Ad Soyad: ";
    public const string USER_USERNAME_LABEL = "Kullanıcı Adı: ";
    public const string USER_NAME_LABEL = "Name: ";

    #endregion
}