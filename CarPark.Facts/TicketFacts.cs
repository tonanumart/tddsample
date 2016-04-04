using CarPark.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace CarPark.Facts
{
    public class TicketFacts
    {
        public class General
        {

            [Fact]
            public void BasicUsage()
            {
                Ticket ticket = new Ticket();
                ticket.PlateNo = "12345";
                ticket.DateIn = new DateTime(2016, 4, 4, 9, 0, 0);
                ticket.DateOut = DateTime.Parse("13:00");
                Assert.Equal("12345", ticket.PlateNo);
                Assert.Equal(9, ticket.DateIn.Hour);
                Assert.Equal(13, ticket.DateOut.Value.Hour);
            }

            [Fact]
            public void NewTicket_HasNoDataInitail()
            {
                Ticket ticket = new Ticket();
                Assert.Null(ticket.DateOut);
            }
        }


        public class ParkingFeeProperty
        {
            [Fact]
            public void First15Min_Free()
            {
                Ticket ticket = new Ticket();
                ticket.DateIn = DateTime.Parse("9:00");
                ticket.DateOut = DateTime.Parse("9:15");

                decimal? fee = ticket.ParkingFee;
                Assert.Equal(0m, fee);
            }

            [Fact]
            public void First15Min_1Second_50Bath()
            {
                Ticket ticket = new Ticket();
                ticket.DateIn = DateTime.Parse("9:00");
                ticket.DateOut = DateTime.Parse("9:15:01");
                decimal? fee = ticket.ParkingFee;
                Assert.Equal(50m, fee);
            }

            [Fact]
            public void Morethan3Hour_50Bath()
            {
                Ticket ticket = new Ticket();
                ticket.DateIn = DateTime.Parse("9:00");
                ticket.DateOut = DateTime.Parse("9:15:01");

                decimal? fee = ticket.ParkingFee;
                Assert.Equal(50m, fee);

            }

            [Fact]
            public void Time3Hour11Min_50Bath()
            {
                Ticket ticket = new Ticket();
                ticket.DateIn = DateTime.Parse("9:00");
                ticket.DateOut = DateTime.Parse("12:11");

                decimal? fee = ticket.ParkingFee;
                Assert.Equal(50m, fee);

            }

            [Fact]
            public void Time4Hour0Min_80Bath()
            {
                Ticket ticket = new Ticket();
                ticket.DateIn = DateTime.Parse("9:00");
                ticket.DateOut = DateTime.Parse("13:00");

                decimal? fee = ticket.ParkingFee;
                Assert.Equal(80m, fee);

            }

            [Fact]
            public void Time6Hour15Min1Second_170Bath()
            {
                Ticket ticket = new Ticket();
                ticket.DateIn = DateTime.Parse("9:00");
                ticket.DateOut = DateTime.Parse("15:15:01");

                decimal? fee = ticket.ParkingFee;
                Assert.Equal(170m, fee);

            }

            [Fact]
            public void UnknownTimeOutTicketFee()
            {
                Ticket ticket = new Ticket();
                ticket.DateIn = DateTime.Parse("9:00");
                Assert.Null(ticket.ParkingFee);
            }

            [Fact]
            public void ThrownIfInMoreThanOut()
            {
                Ticket ticket = new Ticket();
                ticket.DateOut = DateTime.Parse("7:00");
                ticket.DateIn = DateTime.Parse("9:00");
                Exception exception = Assert.Throws<Exception>(() =>
                {
                    decimal? fee = ticket.ParkingFee;
                });
                Assert.Equal("DateTime out must more than Dateime In", exception.Message);
            }

        }

        public class DataDriventest {

            //[Fact]
            [Theory]
            [InlineData("9:00","17:00",200)]
            [InlineData("9:00", "18:00", 230)]
            [InlineData("9:00", "19:00", 260)]
            public void TestData(string dateIn,string dateOut,decimal expectedValue)
            {
                Ticket ticket = new Ticket();
                ticket.DateIn = DateTime.Parse(dateIn);
                ticket.DateOut = DateTime.Parse(dateOut);

                decimal? fee = ticket.ParkingFee;
                Assert.Equal(expectedValue, fee);
            }

        }
    }
}
