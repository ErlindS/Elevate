using CommunityToolkit.Mvvm.ComponentModel;
using Elevate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Elevate.Services
{
    public partial class ElevateTimeService
    {
        private WeekModel _mappedWeek = new();

        public ElevateTimeService()
        {

        }

        public DayOfWeek GetDayOfTheWeek() { 
            DayOfWeek CurrentDayOfWeek = DateTime.Now.DayOfWeek;
            return CurrentDayOfWeek;
        }

        public WeekModel GetWeek() { 
            return _mappedWeek; 
        }

        public void SetWeek(WeekModel week)
        {
            _mappedWeek = week;
        }
    }
}