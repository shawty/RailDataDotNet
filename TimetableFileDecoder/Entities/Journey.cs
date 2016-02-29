using System;
using System.Collections.Generic;

namespace TimetableFile.Entities
{
  public class Journey
  {
    public string RttiTrainId { get; set; }

    public string TrainUid { get; set; }

    public string HeadCode { get; set; }

    public DateTime ScheduledStartDate { get; set; }

    public string AtocCode { get; set; }

    public string TypeOfService { get; set; }

    public string CategoryOfService { get; set; }

    public bool IsPassengerSvc { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsCharter { get; set; }

    public bool QTrain { get; set; }

    public bool IsCanceled { get; set; }

    public List<JourneyLocation> JourneyLocations { get; set; }

    public CancelationReason CancelationReason { get; set; }

  }
}
