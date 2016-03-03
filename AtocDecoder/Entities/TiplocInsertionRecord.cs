namespace AtocDecoder.Entities
{
  public class TiplocInsertionRecord
  {
    public string TiplocCode { get; set; }
    public int CapitalsIdentification { get; set; }
    public int NationalLocationCode { get; set; }
    public string NlcCheckCharacter { get; set; }
    public string TpsDescription { get; set; }
    public int TopsLocationCode { get; set; }
    public int PostOfficeLocationCode { get; set; }
    public string CrsCode { get; set; }
    public string CapriDescription { get; set; }
  }
}
