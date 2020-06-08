using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Model.Interfaces
{
    public interface ITrackable
    {
        DateTimeOffset CreatedAt { get; set; }
        string CreatedBy { get; set; }

        DateTimeOffset UpdatedAt { get; set; }
        string UpdatedBy { get; set; }
    }
}
