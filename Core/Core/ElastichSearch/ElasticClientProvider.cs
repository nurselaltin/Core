using Microsoft.Extensions.Options;
using Nest;

namespace Core.ElastichSearch
{
  public class ElasticClientProvider
  {
    public string _ElastichSearchHost { get; set; }
    public ElasticClient _ElasticClient { get; set; }
    public ElasticClientProvider(IOptions<ElasticConnectionSettings> elasticConfig)
    {
      _ElastichSearchHost = elasticConfig.Value.ElasticSearchHost;
      _ElasticClient = CreateClient();
    }

    /// <summary>
    /// Sadece bir client üretilecek ve projenin başından sonuna kadar aynı client nesnesi kullanılacak
    /// </summary>
    /// <returns></returns>
    public ElasticClient CreateClient()
    {
      var connectionSettings = new ConnectionSettings(new Uri(_ElastichSearchHost))
        .DisablePing() //İlk request’den sonra, belirlenen standart sürenin üstünde bir sürede hata fırlatılması sağlanır.
        .DisableDirectStreaming() //Bunu elasticsearch’de hata alındığı zaman daha detaylı hatayı alabilmek adına eklenmiştir. Memoryde performans kaybına neden olabilir. Sadece ihtiyaç anında kullanılmalıdır.
        .SniffOnStartup() //İlk connection’ın çekilme anında, havuzun kontrol edilmesini engeller. Amaç performanstır.
        .SniffOnConnectionFault(); //Bağlantı havuzu yeniden beslemeyi destekliyorsa, bir arama başarısız olduğunda ilgili connection havuzundan yeniden denetlenmesini engeller. Amaç yine performanstır.
      
      return new ElasticClient(connectionSettings);
    }

    /// <summary>
    /// Default Index ile birlikte sadece bir client üretilecek ve projenin başından sonuna kadar aynı client nesnesi kullanılacak.
    /// </summary>
    /// <returns></returns>
    public ElasticClient CreateClientWithIndex()
    {
      var connectionSettings = new ConnectionSettings(new Uri(_ElastichSearchHost))
        .DisablePing() //İlk request’den sonra, belirlenen standart sürenin üstünde bir sürede hata fırlatılması sağlanır.
        .SniffOnStartup() //İlk connection’ın çekilme anında, havuzun kontrol edilmesini engeller. Amaç performanstır.
        .SniffOnConnectionFault()
        .DefaultIndex("defaultIndex"); //Bağlantı havuzu yeniden beslemeyi destekliyorsa, bir arama başarısız olduğunda ilgili connection havuzundan yeniden denetlenmesini engeller. Amaç yine performanstır.

      return new ElasticClient(connectionSettings);
    }

  }
}
