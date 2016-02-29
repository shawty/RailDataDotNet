using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TimetableFile.Entities;
using XmlExtensionMethods;

namespace TimetableFile
{
  public class Timetable
  {
    private List<Journey> _journeys = null;
    private bool _invalidFile;
    private bool _fileNotLoaded;
    private bool _fileIsLoaded;

    private readonly XNamespace _timetableNs = "http://www.thalesgroup.com/rtti/XmlTimetable/v8";

    public bool InvalidFile { get { return _invalidFile; } }
    public bool FileNotLoaded { get { return _fileNotLoaded; } }
    public bool FileIsLoaded { get { return _fileNotLoaded; } }
    public string TimetableId { get; private set; }

    public Timetable()
    {
      ResetFlags();
    }

    public Timetable(string timetableFile)
    {
      LoadTimetableFile(timetableFile);
    }

    public void LoadTimetableFile(string timetableFile)
    {
      ResetFlags();

      XDocument baseDocument = XDocument.Load(timetableFile);

      if (baseDocument.Root != null && baseDocument.Root.Name.LocalName != "PportTimetable")
      {
        _invalidFile = true;
        _fileNotLoaded = true;
        return;
      }

      _fileIsLoaded = true;

      TimetableId = baseDocument.Root.TryGetAttributeString("timetableID");

      LoadJourneys(baseDocument);

    }

    public List<Journey> GetAllJourneys()
    {
      return _journeys ?? new List<Journey>();
    }

    public List<Journey> GetJourneysFiltered(Func<Journey, bool> lambda)
    {
      List<Journey> results = new List<Journey>();

      if (_journeys == null) return results;

      results.AddRange(_journeys.Where(lambda));
      return results;

    }

    private void LoadJourneys(XDocument baseDocument)
    {
      if (baseDocument.Root == null) return;

      var journeyElements = baseDocument.Root.Elements(_timetableNs + "Journey");

      _journeys = new List<Journey>();

      foreach (XElement journeyElement in journeyElements)
      {
        Journey newJourney = new Journey()
        {
          RttiTrainId = journeyElement.TryGetAttributeString("rid"),
          TrainUid = journeyElement.TryGetAttributeString("uid"),
          HeadCode = journeyElement.TryGetAttributeString("trainId"),
          ScheduledStartDate = journeyElement.TryGetAttributeDateTime("ssd"),
          AtocCode = journeyElement.TryGetAttributeString("toc"),
          TypeOfService = journeyElement.TryGetAttributeString("status"),
          CategoryOfService = journeyElement.TryGetAttributeString("trainCat"),
          IsPassengerSvc = journeyElement.TryGetAttributeBoolean("isPassengerSvc"),
          IsDeleted = journeyElement.TryGetAttributeBoolean("deleted"),
          IsCharter = journeyElement.TryGetAttributeBoolean("isCharter"),
          QTrain = journeyElement.TryGetAttributeBoolean("qtrain"),
          IsCanceled = journeyElement.TryGetAttributeBoolean("can"),
          JourneyLocations = new List<JourneyLocation>()
        };

        LoadJourneyLocations(newJourney, journeyElement);

        var cancelationElement = journeyElement.Element(_timetableNs + "cancelReason");
        if(cancelationElement != null)
        {
          newJourney.CancelationReason = new CancelationReason()
          {
            Tiploc = cancelationElement.TryGetAttributeString("tiploc"),
            ClassifyAsNearTiploc = cancelationElement.TryGetAttributeBoolean("near"),
            CancelationReasonCode = cancelationElement.Value
          };
        }

        _journeys.Add(newJourney);

      }
    }

    private void LoadJourneyLocations(Journey journeyRecord, XElement sourceElement)
    {
      var childElements = sourceElement.Elements();
      int order = 1;
      foreach (XElement childElement in childElements)
      {
        // We don't want cancel reasons in this list, we'll handle them separately
        if (childElement.Name.LocalName.Equals("cancelReason")) continue;

        JourneyLocation thisLocation = new JourneyLocation()
        {
          LocationType = childElement.Name.LocalName,
          Order = order,
          Tiploc = childElement.TryGetAttributeString("tpl"),
          ActivityCode = childElement.TryGetAttributeString("act"),
          PlannedActivityCode = childElement.TryGetAttributeString("planAct"),
          IsCanceled = childElement.TryGetAttributeBoolean("can"),
          PlatformNumber = childElement.TryGetAttributeString("plat"),
          PublicScheduledTimeOfArrival = childElement.TryGetAttributeString("pta"),
          PublicScheduledTimeOfDeparture = childElement.TryGetAttributeString("ptd"),
          WorkingScheduledTimeOfArrival = childElement.TryGetAttributeString("wta"),
          WorkingScheduledTimeOfDeparture = childElement.TryGetAttributeString("wtd"),
          RouteDelay = childElement.TryGetAttributeShort("rdelay"),
          FalseDestination = childElement.TryGetAttributeString("fd")

        };
        journeyRecord.JourneyLocations.Add(thisLocation);
        order++;
      }
    }

    private void ResetFlags()
    {
      _invalidFile = false;
      _fileNotLoaded = false;
      _fileIsLoaded = false;
    }


  }
}
