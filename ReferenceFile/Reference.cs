using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ReferenceFile.Entities;
using XmlExtensionMethods;

namespace ReferenceFile
{
  public class Reference
  {
    private List<LocationReference> _locations = null;
    private List<TocReference> _trainOperators = null;
    private List<ReasonReference> _lateRunningReasons = null;
    private List<ReasonReference> _cancelationReasons = null;

    private bool _invalidFile;
    private bool _fileNotLoaded;
    private bool _fileIsLoaded;

    private readonly XNamespace _timetableRefNs = "http://www.thalesgroup.com/rtti/XmlRefData/v3";

    public bool InvalidFile { get { return _invalidFile; } }
    public bool FileNotLoaded { get { return _fileNotLoaded; } }
    public bool FileIsLoaded { get { return _fileNotLoaded; } }
    public string TimetableId { get; private set; }

    public Reference()
    {
      ResetFlags();
    }

    public Reference(string referenceFile)
    {
      LoadReferenceFile(referenceFile);
    }

    public void LoadReferenceFile(string referenceFile)
    {
      ResetFlags();

      XDocument baseDocument = XDocument.Load(referenceFile);

      if (baseDocument.Root != null && baseDocument.Root.Name.LocalName != "PportTimetableRef")
      {
        _invalidFile = true;
        _fileNotLoaded = true;
        return;
      }

      _fileIsLoaded = true;

      TimetableId = baseDocument.Root.TryGetAttributeString("timetableId");

      LoadLocations(baseDocument);
      LoadTrainOperators(baseDocument);
      LoadLateRunningReasons(baseDocument);
      LoadCancelationReasons(baseDocument);

    }

    public List<LocationReference> GetAllLocations()
    {
      return _locations ?? new List<LocationReference>();
    }

    public List<LocationReference> GetLocationsFiltered(Func<LocationReference, bool> lambda)
    {
      List<LocationReference> results = new List<LocationReference>();

      if (_locations == null) return results;

      results.AddRange(_locations.Where(lambda));
      return results;

    }

    public List<TocReference> GetAllOperators()
    {
      return _trainOperators ?? new List<TocReference>();
    }

    public List<TocReference> GetOperatorsFiltered(Func<TocReference, bool> lambda)
    {
      List<TocReference> results = new List<TocReference>();

      if (_trainOperators == null) return results;

      results.AddRange(_trainOperators.Where(lambda));
      return results;

    }

    public List<ReasonReference> GetAllLateReasons()
    {
      return _lateRunningReasons ?? new List<ReasonReference>();
    }

    public List<ReasonReference> GetLateReasonsFiltered(Func<ReasonReference, bool> lambda)
    {
      List<ReasonReference> results = new List<ReasonReference>();

      if (_lateRunningReasons == null) return results;

      results.AddRange(_lateRunningReasons.Where(lambda));
      return results;

    }

    public List<ReasonReference> GetAllCancelationReasons()
    {
      return _cancelationReasons ?? new List<ReasonReference>();
    }

    public List<ReasonReference> GetCancelationReasonsFiltered(Func<ReasonReference, bool> lambda)
    {
      List<ReasonReference> results = new List<ReasonReference>();

      if (_cancelationReasons == null) return results;

      results.AddRange(_cancelationReasons.Where(lambda));
      return results;

    }

    private void LoadLocations(XDocument baseDocument)
    {
      if (baseDocument.Root == null) return;

      _locations = new List<LocationReference>();
      var locationRefElements = baseDocument.Root.Elements(_timetableRefNs + "LocationRef");
      foreach (XElement locationRefElement in locationRefElements)
      {
        _locations.Add(new LocationReference()
        {
          Tpl = locationRefElement.TryGetAttributeString("tpl"),
          Crs = locationRefElement.TryGetAttributeString("crs"),
          Toc = locationRefElement.TryGetAttributeString("toc"),
          LocName = locationRefElement.TryGetAttributeString("locname")

        });
      }

    }

    private void LoadTrainOperators(XDocument baseDocument)
    {
      if (baseDocument.Root == null) return;

      _trainOperators = new List<TocReference>();
      var locationRefElements = baseDocument.Root.Elements(_timetableRefNs + "TocRef");
      foreach (XElement locationRefElement in locationRefElements)
      {
        _trainOperators.Add(new TocReference()
        {
          Toc = locationRefElement.TryGetAttributeString("toc"),
          TocName = locationRefElement.TryGetAttributeString("tocname"),
          Url = locationRefElement.TryGetAttributeString("url")
        });
      }

    }

    private void LoadLateRunningReasons(XDocument baseDocument)
    {
      if (baseDocument.Root == null) return;

      _lateRunningReasons = new List<ReasonReference>();

      var lateReasonsElement = baseDocument.Root.Element(_timetableRefNs + "LateRunningReasons");
      if (lateReasonsElement == null) return;

      var lateReasons = lateReasonsElement.Elements(_timetableRefNs + "Reason");
      foreach (XElement lateReason in lateReasons)
      {
        _lateRunningReasons.Add(new ReasonReference()
        {
          Code = lateReason.TryGetAttributeInteger("code"),
          ReasonText = lateReason.TryGetAttributeString("reasontext")
        });
      }

    }

    private void LoadCancelationReasons(XDocument baseDocument)
    {
      if (baseDocument.Root == null) return;

      _cancelationReasons = new List<ReasonReference>();

      var canelationReasonsElement = baseDocument.Root.Element(_timetableRefNs + "CancellationReasons");
      if (canelationReasonsElement == null) return;

      var cancelReasons = canelationReasonsElement.Elements(_timetableRefNs + "Reason");
      foreach (XElement cancelReason in cancelReasons)
      {
        _cancelationReasons.Add(new ReasonReference()
        {
          Code = cancelReason.TryGetAttributeInteger("code"),
          ReasonText = cancelReason.TryGetAttributeString("reasontext")
        });
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
