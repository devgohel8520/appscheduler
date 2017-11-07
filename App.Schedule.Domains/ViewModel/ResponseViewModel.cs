using System.Collections.Generic;

namespace App.Schedule.Domains.ViewModel
{
    public class ResponseViewModel<T>
    {
        public bool Status { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }

    public class ModelState
    {
        public List<string> Message { get; set; }
    }

    public class AspIdentityModelStateViewModel
    {
        public string Message { get; set; }
        public ModelState ModelState { get; set; }
    }
}
