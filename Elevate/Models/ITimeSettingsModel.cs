using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevate.Models
{
    public interface ITimeSettingsModel
    {
        TimeOnly StartTime { get; }
        TimeOnly EndTime { get; }
        TimeOnly GetStartTime();
        TimeOnly GetEndTime();
        void SetStartTime(TimeOnly timeOnly);
        void SetEndTime(TimeOnly timeOnly);

    }
}
