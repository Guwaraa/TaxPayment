﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxPayment.Repository.DapperDao
{
    public interface IDapperDao
    {
        #region Synchronous Calls
        List<T0> ExecuteQuery<T0>(string sqlQuery, object sqlParam, CommandType queryType = CommandType.StoredProcedure);
        List<object> ExecuteQuery<T0, T1>(string sqlQuery, object sqlParam, CommandType queryType = CommandType.StoredProcedure);
        List<object> ExecuteQuery<T0, T1, T2>(string sqlQuery, object sqlParam, CommandType queryType = CommandType.StoredProcedure);
        List<object> ExecuteQuery<T0, T1, T2, T3>(string sqlQuery, object sqlParam, CommandType queryType = CommandType.StoredProcedure);
        List<object> ExecuteQuery<T0, T1, T2, T3, T4>(string sqlQuery, object sqlParam, CommandType queryType = CommandType.StoredProcedure);
        List<object> ExecuteQuery<T0, T1, T2, T3, T4, T5>(string sqlQuery, object sqlParam, CommandType queryType = CommandType.StoredProcedure);
        List<object> ExecuteQuery<T0, T1, T2, T3, T4, T5, T6>(string sqlQuery, object sqlParam, CommandType queryType = CommandType.StoredProcedure);
        List<object> ExecuteQuery<T0, T1, T2, T3, T4, T5, T6, T7>(string sqlQuery, object sqlParam, CommandType queryType = CommandType.StoredProcedure);
        List<object> ExecuteQuery<T0, T1, T2, T3, T4, T5, T6, T7, T8>(string sqlQuery, object sqlParam, CommandType queryType = CommandType.StoredProcedure);
        List<object> ExecuteQuery<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string sqlQuery, object sqlParam, CommandType queryType = CommandType.StoredProcedure);
        List<object> ExecuteQuery<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string sqlQuery, object sqlParam, CommandType queryType = CommandType.StoredProcedure);
        List<object> ExecuteQuery<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string sqlQuery, object sqlParam, CommandType queryType = CommandType.StoredProcedure);
        List<object> ExecuteQuery<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string sqlQuery, object sqlParam, CommandType queryType = CommandType.StoredProcedure);
        List<object> ExecuteQuery<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string sqlQuery, object sqlParam, CommandType queryType = CommandType.StoredProcedure);

        #endregion
    }
}
