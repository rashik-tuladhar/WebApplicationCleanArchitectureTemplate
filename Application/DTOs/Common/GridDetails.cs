using System.Collections.Generic;

namespace Domain.Settings
{
    public class GridDetails : JqueryDataTableHelper
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

    public class HtmlGrid<T>
    {
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public List<T> aaData { get; set; }
    }

    public abstract class JqueryDataTableHelper
    {
        // properties are not capital due to json mapping
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }
    }

    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
    }

    public class Search
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

    public class GridParam
    {
        public int DisplayLength { get; set; }
        public int DisplayStart { get; set; }
        public string SortDir { get; set; }
        public int SortCol { get; set; }
        public string Flag { get; set; }
        public string Search { get; set; }
        public string UserName { get; set; }
    }


    public class GridExtension
    {
        public string FilterCount { get; set; }
        public string Action { get; set; }
    }
}
