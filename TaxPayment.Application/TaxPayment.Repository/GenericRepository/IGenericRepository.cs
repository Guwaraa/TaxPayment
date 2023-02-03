using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxPayment.Common.SystemResponse;

namespace TaxPayment.Repository.GenericRepository
{
    public interface IGenericRepository
    {
        SystemResponse ManageData<T>(string spName, T input);
        T ManageDataWithSingleObject<T>(string spName, object obj);
        List<T> ManageDataWithListObject<T>(string spName, object obj);
        List<object> ManageDataWithListObjectMultiple<T0, T1>(string spName, object obj);
        List<object> ManageDataWithObjectMultiple<T0, T1, T2, T3, T4, T5, T6>(string spName, object obj);
        List<List<SelectListItem>> ManageDataWithMultipleSelectListItem(string spName, string flagName);
        List<List<SelectListItem>> ManageDataWithMultipleSelectListItemOBJ(string spName,object param);

    }
}

