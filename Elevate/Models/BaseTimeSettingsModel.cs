using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elevate.Models;


namespace Elevate.Models
{
    /// <summary>
    /// Basic time Setting Model with some implemented functions
    /// </summary>
    public abstract class BaseTimeSettingsModel : ITimeSettingsModel
    {
        public virtual TimeOnly StartTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);

        public virtual TimeOnly EndTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);

        public virtual TimeOnly GetStartTime() => StartTime;
        public virtual TimeOnly GetEndTime() => EndTime;

        public virtual void SetStartTime(TimeOnly timeOnly) { 
            StartTime = timeOnly;
        }

        public virtual void SetEndTime(TimeOnly timeOnly)
        {
            EndTime = timeOnly;
        }
    }
}
