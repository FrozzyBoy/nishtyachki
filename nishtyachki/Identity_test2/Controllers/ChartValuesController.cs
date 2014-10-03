using AdminApp.QueueChannel;
using System.Web.Http;

namespace AdminApp.Controllers
{    
    [RoutePrefix("api/chartValues")]
    public class ChartValuesController : ApiController
    {
        private readonly IQueueChannel _channel;

        public ChartValuesController(IQueueChannel channel)
        {
            _channel = channel;
        }

        // GET api/<controller>
        [Route("{state}")]
        public object GetGeneral(int state)
        { 
            return _channel.GetStatisticsGeneralWasMoreThenAthoresInState(state);
        }

        [Route("user/{userID}")]
        public object GetPersonal(string userID)
        {            
            return _channel.GetStatisticsPersonal(userID);
        }

        [Route("nishtiak/{id}")]
        public object GetNishtiakStatistic(string id)
        {
            return _channel.GetStatisticsForNishtiak(id);
        }
    }
}