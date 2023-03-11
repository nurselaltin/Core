namespace Core.CommonModel
{
  public enum UserTypeEnum
  {
    Admin = 0,
    Worker = 1,
    Customer = 2
  }
  public enum ResultCode  //Burayı özelleştirebiliriz
  {
    //Success Codes
    Success = 200,
    Created = 201, //Yeni bir kayıt eklenme durumu
    SuccesWithNoContent = 204, //Örneğin silme işlemi başarılı olup, body kısmında bir data dönmediği durumlar için kullanılır


    //Error Codes
    BadRequest = 400, //Gönderilen request modeli uygun olmadığı validasyon hatalarında kullanılabilir
    Unauthorized = 401, // Doğrulama yapılamadığı durumlarda kullanılır  
    Forbidden = 403, // Yetki olmayan işlemlerde kullanılır
    NotFound = 404, // İstenilen veri bulanamadığı durumlar 


    GeneralServiceError = 900, //Sistemsel hatalarda bu kodu kullanalım

    MissingArgument = 400,
    UnavailableService = 503,

  }
}
