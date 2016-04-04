using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPark.Model
{
    public class Ticket
    {
        public DateTime DateIn { get; set; }
        public DateTime? DateOut { get; set; }
        public decimal? ParkingFee
        {
            get
            {


                if (!this.DateOut.HasValue)
                    return null;

                if (this.DateOut < this.DateIn)
                {
                    throw new Exception("DateTime out must more than Dateime In");
                }

                TimeSpan minInterval = this.DateOut.Value - this.DateIn;
                if (minInterval.TotalMinutes < 180)
                {
                    if (minInterval.TotalMinutes <= 15)
                    {
                        return 0;
                    }
                    return Cost.Minimum;
                }
                else
                {
                    var afterhour = minInterval.Hours - 3;
                    if (minInterval.TotalHours - minInterval.Hours > 0.25)//more than 1 a quarter
                        afterhour++;
                    return Cost.Minimum + (Cost.UnitPerHour * afterhour);
                }
            }
        }
        public string PlateNo { get; set; }
    }

    public class Cost
    {
        public static decimal Minimum = 50M;
        public static decimal UnitPerHour = 30M;
    }

}
