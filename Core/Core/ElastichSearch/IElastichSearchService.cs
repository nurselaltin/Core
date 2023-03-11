
using Core.Models.Users;

namespace Core.ElastichSearch
{
  public interface IElastichSearchService<T> where T : class
  {
    //Yok ise ilgili Index’in oluşturulması ve istenen document’ın yani row’un atılması sağlanır.
    public void CheckExistsAndInsertLog(T logModel, string indexName);

    // Login olan clientların ve çağırdıkları işaretli tüm methodların logu, bu method ile filtrelenerek listelenebilir.
    public IReadOnlyCollection<LoginLogModel> SearchLoginLog(string userID, DateTime? BeginDate, DateTime? EndDate, string controller = "", string action = "", int? page = 0, int? rowCount = 10, string? indexName = "login_log");
  }


}


