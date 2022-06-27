using System;
using System.Collections.Generic;

namespace api.Helper
{
    
    public class singleResponse<TModel>
    {
        public string message { get; set; }
        public TModel model { get; set; }
    }

    //public class ArrayResponse<TModel>
    //{
    //    public int? pageNumber { get; set; }
    //    public int totalRecords { get; set; }
    //    public int currentRecords { get; set; }
    //    public int? pageSize { get; set; }
    //    public string message { get; set; }
    //    public string errorMessage { get; set; }
    //    public bool didError { get; set; }
    //    public IEnumerable<TModel> model { get; set; }
    //}

    public class ArrayResponse<TModel>
    {
        public string message { get; set; }
        public IEnumerable<TModel> model { get; set; }
    }

    public class ReportResponse<TModel>:ArrayResponse<TModel>
    {
       
        public Decimal? crAmount { get; set; }
        public Decimal? drAmount { get; set; }
        public string balType { get; set; }
        public Decimal? balance { get; set; }
    }

    
}