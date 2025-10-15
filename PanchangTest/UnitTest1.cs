using NUnit.Framework;
using org.transliteral.panchang;

namespace PanchangTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            GlobalOptions globalOptions = new GlobalOptions();
            GlobalOptions.Instance = globalOptions;
            globalOptions.HOptions.EphemerisPath = "D:\\KHAPRE_DATA\\Code\\CalendarCode\\SwissEphemeris\\eph";

            PanchangOptions computeOptions = new PanchangOptions()
            {
                NumberOfDays = 3,
            };

            HoroscopeOptions horaOptions = new HoroscopeOptions()
            {
                Ayanamsa = AyanamsaType.Lahiri,
                SunrisePosition = SunrisePositionType.ApparentDiscCenter,
                EphemerisPath = globalOptions.HOptions.EphemerisPath

            };
            StrengthOptions strengthOptions = new StrengthOptions();

            /* 
             *   Dallas
                 HMSInfo mLat = new HMSInfo(33, 0, 18, Direction.NS);
                 HMSInfo mLon = new HMSInfo(-97, 13, 35, Direction.EW);
                 HMSInfo mTz = new HMSInfo(-5, 0, 0, Direction.EW);
            */
            /*   
            *   Seattle
                mLat = new HMSInfo(47, 40, 27, dir_type.NS);
                mLon = new HMSInfo(-122, 7, 13, dir_type.EW);
                mTz = new HMSInfo(-7, 0, 0, dir_type.EW);
           */



            HoraInfo horaInfo = new HoraInfo(
                                        atob: new Moment(),
                                        alat: new HMSInfo(33, 0, 18, Direction.NorthSouth),
                                        alon: new HMSInfo(-97, 13, 35, Direction.EastWest),
                                        atz: new HMSInfo(-5, 0, 0, Direction.EastWest)
                                        );

            HinduPanchang  hinduPanchang = new HinduPanchang(
                                    horaInfo, 
                                    horaOptions, 
                                    strengthOptions, 
                                    globalOptions, 
                                    computeOptions
                              );
            hinduPanchang.Compute();

            Assert.Pass();
        }
    }
}