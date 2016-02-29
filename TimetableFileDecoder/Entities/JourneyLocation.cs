using System;
using System.Linq;

namespace TimetableFile.Entities
{
  public class JourneyLocation
  {
    private string _locationType = String.Empty;

    public string LocationType
    {
      get
      {
        return _locationType;
      }
      set
      {
        string[] acceptableValues = {"OR", "OPOR", "IP", "OPIP", "PP", "DT", "OPDT"};
        if (!acceptableValues.Contains(value.ToUpper()))
        {
          throw new ApplicationException("Illegal value specified for LocationType, value MUST be one of: " + String.Join(", ", acceptableValues));
        }
        _locationType = value.ToUpper();
      }
    }

    public int Order { get; set; }

    public string Tiploc { get; set; }

    public string ActivityCode { get; set; }

    public string PlannedActivityCode { get; set; }

    public bool IsCanceled { get; set; }

    public string PlatformNumber { get; set; }

    public string PublicScheduledTimeOfArrival { get; set; }

    public string PublicScheduledTimeOfDeparture { get; set; }

    public string WorkingScheduledTimeOfArrival { get; set; }

    public string WorkingScheduledTimeOfDeparture { get; set; }

    public short RouteDelay { get; set; }

    public string FalseDestination { get; set; }

  }
}
