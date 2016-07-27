using System.Web.Http;
using MongoDb.DAL.Context;

namespace MongoDb.WebUI.Controllers
{
    public class DiagnoseController : ApiController
    {
        private readonly RestaurantContext _context;

        public DiagnoseController()
        {
            _context = new RestaurantContext();
        }

        /// <summary>
        /// This action is used to test if the database server is online
        /// </summary>
        /// <returns>IHttpActionResult with boolean</returns>
        [HttpGet]
        public IHttpActionResult GetDatabaseConnectionStatus()
        {
            bool isServerOnline = _context.IsServerOnline();
            return Ok(isServerOnline);
        }
    }
}
