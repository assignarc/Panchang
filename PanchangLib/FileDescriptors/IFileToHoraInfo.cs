namespace org.transliteral.panchang
{
    /// <summary>
    /// Interface will return a HoraInfo object specifying all birth
    /// time information required for a single chart.
    /// </summary>
    public interface IFileToHoraInfo
    {
        HoraInfo toHoraInfo();
        void ToFile(HoraInfo hi);
    }
}
