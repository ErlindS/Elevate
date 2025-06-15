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
    public partial class ElevateTimeService : ObservableObject
    {
        [ObservableProperty]
        private WeekModel mappedWeek;

        public ElevateTimeService()
        {

        }

        public DayOfWeek GetDayOfTheWeek() { 
            DayOfWeek CurrentDayOfWeek = DateTime.Now.DayOfWeek;
            return CurrentDayOfWeek;
        }
    }
}