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

    private void ResetFlags()
    {
      _invalidFile = false;
      _fileNotLoaded = false;
      _fileIsLoaded = false;
    }

  }

}
