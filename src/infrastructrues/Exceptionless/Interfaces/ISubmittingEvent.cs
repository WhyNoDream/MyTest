using Exceptionless;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionlessUnit.Interfaces
{
    public interface ISubmittingEvent
    {
        void OnSubmittingEvent(object sender, EventSubmittingEventArgs e);
    }
}
