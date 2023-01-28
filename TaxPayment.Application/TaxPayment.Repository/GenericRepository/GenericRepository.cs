using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxPayment.Common.SystemResponse;
using TaxPayment.Repository.DapperDao;

namespace TaxPayment.Repository.GenericRepository
{
    public class GenericRepository : IGenericRepository
    {
        private readonly IDapperDao _dapperDao;
        private readonly ILogger _log = Log.ForContext<GenericRepository>();

        public GenericRepository(IDapperDao dapperDao)
        {
            _dapperDao = dapperDao;
        }

        public SystemResponse ManageData<T>(string spName, T input)
        {
            string procedureName = spName;
            _log.Information("sp called with query {0} {1}", "EXEC " + procedureName, JsonConvert.SerializeObject(input));
            var response = _dapperDao.ExecuteQuery<SystemResponse>(procedureName, input);
            _log.Information("response returned as {0}", JsonConvert.SerializeObject(response));
            return response.FirstOrDefault();
        }



        public T ManageDataWithSingleObject<T>(string spName, object obj)
        {
            string procedureName = spName;
            _log.Information("sp called with query {0} {1}", "EXEC " + procedureName, JsonConvert.SerializeObject(obj));
            var response = _dapperDao.ExecuteQuery<T>(procedureName, obj);
            _log.Information("response returned as {0}", JsonConvert.SerializeObject(response));
            return response.FirstOrDefault();
        }

        public List<T> ManageDataWithListObject<T>(string spName, object obj)
        {
            string procedureName = spName;
            _log.Information("sp called with query {0} {1}", "EXEC " + procedureName, JsonConvert.SerializeObject(obj));
            var response = _dapperDao.ExecuteQuery<T>(procedureName, obj);
            _log.Information("response returned as {0}", JsonConvert.SerializeObject(response));
            return response;
        }


        public List<object> ManageDataWithListObjectMultiple<T0, T1>(string spName, object obj)
        {
            string procedureName = spName;
            _log.Information("sp called with query {0} {1}", "EXEC " + procedureName, JsonConvert.SerializeObject(obj));
            var response = _dapperDao.ExecuteQuery<T0, T1>(procedureName, obj);
            _log.Information("response returned as {0}", JsonConvert.SerializeObject(response));
            return response;
        }
        public List<object> ManageDataWithObjectMultiple<T0, T1, T2, T3, T4, T5, T6>(string spName, object obj)
        {
            string procedureName = spName;
            _log.Information("sp called with query {0} {1}", "EXEC " + procedureName, JsonConvert.SerializeObject(obj));
            var response = _dapperDao.ExecuteQuery<T0, T1, T2, T3, T4, T5, T6>(procedureName, obj);
            _log.Information("response returned as {0}", JsonConvert.SerializeObject(response));
            return response;
        }
    }
}
