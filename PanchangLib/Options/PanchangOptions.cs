using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace org.transliteral.panchang
{
    [Serializable]
    public class PanchangOptions : SerializableOptions, ICloneable, ISerializable
    {

        private int mNumberOfDays = 3;
        private bool bCalculateLagnaCusps = false;
        private bool bCalculateTithiCusps = true;
        private bool bCalculateKaranaCusps = true;
        private bool bCalculateNakshatraCusps = true;
        private bool bCalculateHoraCusps = true;
        private bool bCalculateSMYogaCusps = true;
        private bool bCalculateKalaCusps = true;
        private bool bShowSpecialKalas = true;
        private bool bShowSunriset = true;
        private bool bLargeHours = false;
        private bool bShowUpdates = true;
        private bool bOneEntryPerLine = false;


        private readonly int[] rahu_kalas = new int[] { 7, 1, 6, 4, 5, 3, 2 };
        private readonly int[] gulika_kalas = new int[] { 6, 5, 4, 3, 2, 1, 0 };
        private readonly int[] yama_kalas = new int[] { 4, 3, 2, 1, 0, 6, 5 };

        public PanchangOptions()
        {
            NumberOfDays = 3;
        }
        

        [Description("Rahu Kala Index")]
        public int[] RahuKalas => rahu_kalas;

        [Description("Gulika Kala Index")]
        public int[] GulikaKalas => gulika_kalas;

        [Description("Yama Kala Index")]
        public int[] YamaKalas => yama_kalas;

        [Description("Number of days to compute information for")]
        public int NumberOfDays
        {
            get { return mNumberOfDays; }
            set { mNumberOfDays = value; }
        }
        [Description("Include sunriset / sunset in the output?")]
        public bool ShowSunriset
        {
            get { return bShowSunriset; }
            set { bShowSunriset = value; }
        }
        [Description("Calculate and include Lagna cusp changes?")]
        public bool CalculateLagnaCusps
        {
            get { return bCalculateLagnaCusps; }
            set { bCalculateLagnaCusps = value; }
        }
        [Description("Calculate and include Tithi cusp information?")]
        public bool CalculateTithiCusps
        {
            get { return bCalculateTithiCusps; }
            set { bCalculateTithiCusps = value; }
        }
        [Description("Calculate and include Karana cusp information?")]
        public bool CalculateKaranaCusps
        {
            get { return bCalculateKaranaCusps; }
            set { bCalculateKaranaCusps = value; }
        }
        [Description("Calculate and include Sun-Moon yoga cusp information?")]
        public bool CalculateSunMoonYogaCusps
        {
            get { return bCalculateSMYogaCusps; }
            set { bCalculateSMYogaCusps = value; }
        }
        [Description("Calculate and include Nakshatra cusp information?")]
        public bool CalculateNakshatraCusps
        {
            get { return bCalculateNakshatraCusps; }
            set { bCalculateNakshatraCusps = value; }
        }
        [Description("Calculate and include Hora cusp information?")]
        public bool CalculateHoraCusps
        {
            get { return bCalculateHoraCusps; }
            set { bCalculateHoraCusps = value; }
        }
        [Description("Calculate and include special Kalas?")]
        public bool CalculateSpecialKalas
        {
            get { return bShowSpecialKalas; }
            set { bShowSpecialKalas = value; }
        }
        [Description("Calculate and include Kala cusp information?")]
        public bool CalculateKalaCusps
        {
            get { return bCalculateKalaCusps; }
            set { bCalculateKalaCusps = value; }
        }
        [Description("Display 02:00 after midnight as 26:00 or *02:00?")]
        public bool LargeHours
        {
            get { return bLargeHours; }
            set { bLargeHours = value; }
        }
        [Description("Display incremental updates?")]
        public bool ShowUpdates
        {
            get { return bShowUpdates; }
            set { bShowUpdates = value; }
        }
        [Description("Display only one entry / line?")]
        public bool OneEntryPerLine
        {
            get { return bOneEntryPerLine; }
            set { bOneEntryPerLine = value; }
        }

       

        public object Clone()
        {
            PanchangOptions uo = new PanchangOptions
            {
                NumberOfDays = NumberOfDays,
                CalculateLagnaCusps = CalculateLagnaCusps,
                CalculateNakshatraCusps = CalculateNakshatraCusps,
                CalculateTithiCusps = CalculateTithiCusps,
                CalculateKaranaCusps = CalculateKaranaCusps,
                CalculateHoraCusps = CalculateHoraCusps,
                CalculateKalaCusps = CalculateKalaCusps,
                CalculateSpecialKalas = CalculateSpecialKalas,
                LargeHours = LargeHours,
                ShowUpdates = ShowUpdates,
                ShowSunriset = ShowSunriset,
                OneEntryPerLine = OneEntryPerLine,
                CalculateSunMoonYogaCusps = CalculateSunMoonYogaCusps
            };
            return uo;
        }
        public object CopyFrom(object _uo)
        {
            PanchangOptions uo = (PanchangOptions)_uo;
            NumberOfDays = uo.NumberOfDays;
            CalculateLagnaCusps = uo.CalculateLagnaCusps;
            CalculateNakshatraCusps = uo.CalculateNakshatraCusps;
            CalculateTithiCusps = uo.CalculateTithiCusps;
            CalculateKaranaCusps = uo.CalculateKaranaCusps;
            CalculateHoraCusps = uo.CalculateHoraCusps;
            CalculateKalaCusps = uo.CalculateKalaCusps;
            CalculateSpecialKalas = uo.CalculateSpecialKalas;
            LargeHours = uo.LargeHours;
            ShowUpdates = uo.ShowUpdates;
            ShowSunriset = uo.ShowSunriset;
            CalculateSunMoonYogaCusps = uo.CalculateSunMoonYogaCusps;
            OneEntryPerLine = uo.OneEntryPerLine;
            return Clone();
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }

}
