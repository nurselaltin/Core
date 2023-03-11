using Core.Models.Users;
using Nest;

namespace Core.ElastichSearch
{
  public class ElastichSearchService<T> : IElastichSearchService<T> where T : class
  {
    ElasticClientProvider _provider;
    ElasticClient _client;

    public ElastichSearchService(ElasticClientProvider provider)
    {
      _provider= provider;
      _client = _provider._ElasticClient;
    }

    public void CheckExistsAndInsertLog(T logModel, string indexName)
    {
      if (!_client.Indices.Exists(indexName).Exists)
      {
        var newIndexName = indexName + System.DateTime.Now.Ticks;

        var indexSettings = new IndexSettings(); // ???

        #region indexSettingsNote
        //3 sharding denilen, 3 makinada performans amaçlı çalışan ve herbir makinanın 1 yedeği olacak şekilde(Replica) güvenlik amaçlı toplam 6 makina üzerinde,
        //Elasticsearch yapılandırılır.6 makina 2şerli gruplar halinde toplam 3 grupa ayrılır.
        //Herbir gruba Node denir. Herbir makina yedeğinin, farklı bir Nodeda olması elzemdir
        indexSettings.NumberOfReplicas = 1;
        indexSettings.NumberOfShards = 3;
        #endregion

        var response = _client.Indices.Create(newIndexName, index =>
        index.Map<T>(m => m.AutoMap())
        .InitializeUsing(new IndexState() { Settings = indexSettings })
        //Elasticsearch’e aliases vermeniz çok önemlidir. İlerde index üzerinde değişiklik yapılırken,
        //önceden kaydedilen dökümanların kaybedilmeden yenisinin kolaylıkla yaratılabilmesini sağlar.
        .Aliases(a => a.Alias(indexName))
        );
      }
      //Var olana atama yap
      IndexResponse responseIndex = _client.Index<T>(logModel, idx => idx.Index(indexName));
      int a = 0;
    }

    public IReadOnlyCollection<LoginLogModel> SearchLoginLog(string userID, DateTime? BeginDate, DateTime? EndDate, string controller = "", string action = "", int? page = 0, int? rowCount = 10, string? indexName = "login_log")
    {
      throw new NotImplementedException();
    }
  }


}


