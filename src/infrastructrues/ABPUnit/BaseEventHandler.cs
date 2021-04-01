using System;
using System.Collections.Generic;
using System.Text;

namespace ABPUnit
{
    public class BaseEventHandler
    {
        public BaseEventHandler(long id,string content)
        {
            this.Id = id;
            this.Content = content;
        }
        public long Id { get;private set; }
        public string Content { get;private set; }
    }
}
