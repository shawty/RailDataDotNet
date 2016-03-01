using System;
using System.IO;
using Newtonsoft.Json;
using ReferenceFile;
using TimetableFile;

namespace ConvertToJson
{
  class Program
  {
    private static Timetable myTimetable;
    private static Reference myReference;

    private static string outputFolder = @"D:\Data\National Rail\Network Rail Data\Json";

    private static void WriteJourneysFile()
    {
      Console.WriteLine("Writing journeys to JSON format");
      var myJourneys = myTimetable.GetAllJourneys();

      using (FileStream fs = File.Open(Path.Combine(outputFolder, "journeys.json"), FileMode.CreateNew))
      using (StreamWriter sw = new StreamWriter(fs))
      using (JsonWriter jw = new JsonTextWriter(sw))
      {
        jw.Formatting = Formatting.Indented;

        JsonSerializer serializer = new JsonSerializer();
        serializer.Serialize(jw, myJourneys);
      }
    }

    private static void WriteLocationsFile()
    {
      Console.WriteLine("Writing Location Refs to JSON format");
      var myLocations = myReference.GetAllLocations();

      using (FileStream fs = File.Open(Path.Combine(outputFolder, "locationrefs.json"), FileMode.CreateNew))
      using (StreamWriter sw = new StreamWriter(fs))
      using (JsonWriter jw = new JsonTextWriter(sw))
      {
        jw.Formatting = Formatting.Indented;

        JsonSerializer serializer = new JsonSerializer();
        serializer.Serialize(jw, myLocations);
      }
    }

    private static void WriteOperatorsFile()
    {
      Console.WriteLine("Writing Operator Refs to JSON format");
      var myOperators = myReference.GetAllOperators();

      using (FileStream fs = File.Open(Path.Combine(outputFolder, "operatorrefs.json"), FileMode.CreateNew))
      using (StreamWriter sw = new StreamWriter(fs))
      using (JsonWriter jw = new JsonTextWriter(sw))
      {
        jw.Formatting = Formatting.Indented;

        JsonSerializer serializer = new JsonSerializer();
        serializer.Serialize(jw, myOperators);
      }
    }

    private static void WriteLateReasonsFile()
    {
      Console.WriteLine("Writing Late Reasons to JSON format");
      var myReasons = myReference.GetAllLateReasons();

      using (FileStream fs = File.Open(Path.Combine(outputFolder, "latereasons.json"), FileMode.CreateNew))
      using (StreamWriter sw = new StreamWriter(fs))
      using (JsonWriter jw = new JsonTextWriter(sw))
      {
        jw.Formatting = Formatting.Indented;

        JsonSerializer serializer = new JsonSerializer();
        serializer.Serialize(jw, myReasons);
      }
    }

    private static void WriteCancelReasonsFile()
    {
      Console.WriteLine("Writing Cancelation Reasons to JSON format");
      var myReasons = myReference.GetAllCancelationReasons();

      using (FileStream fs = File.Open(Path.Combine(outputFolder, "cancelreasons.json"), FileMode.CreateNew))
      using (StreamWriter sw = new StreamWriter(fs))
      using (JsonWriter jw = new JsonTextWriter(sw))
      {
        jw.Formatting = Formatting.Indented;

        JsonSerializer serializer = new JsonSerializer();
        serializer.Serialize(jw, myReasons);
      }
    }

    private static void WriteViaRecordsFile()
    {
      Console.WriteLine("Writing Via Records to JSON format");
      var myViaRecords = myReference.GetAllViaRecords();

      using (FileStream fs = File.Open(Path.Combine(outputFolder, "viarecords.json"), FileMode.CreateNew))
      using (StreamWriter sw = new StreamWriter(fs))
      using (JsonWriter jw = new JsonTextWriter(sw))
      {
        jw.Formatting = Formatting.Indented;

        JsonSerializer serializer = new JsonSerializer();
        serializer.Serialize(jw, myViaRecords);
      }
    }

    private static void WriteCisSourceRecordsFile()
    {
      Console.WriteLine("Writing Cis Source records to JSON format");
      var myCisSources = myReference.GetAllCisSourceReferences();

      using (FileStream fs = File.Open(Path.Combine(outputFolder, "cissourcerecords.json"), FileMode.CreateNew))
      using (StreamWriter sw = new StreamWriter(fs))
      using (JsonWriter jw = new JsonTextWriter(sw))
      {
        jw.Formatting = Formatting.Indented;

        JsonSerializer serializer = new JsonSerializer();
        serializer.Serialize(jw, myCisSources);
      }
    }

    static void Main()
    {
      Console.WriteLine("Loading timetable and reference files");
      myTimetable = new Timetable(@"D:\Data\National Rail\Network Rail Data\20160228020751_v8.xml\20160228020751_v8.xml");
      myReference = new Reference(@"D:\Data\National Rail\Network Rail Data\20160228020751_ref_v3.xml\20160228020751_ref_v3.xml");

      if(!Directory.Exists(outputFolder))
      {
        Directory.CreateDirectory(outputFolder);
      }

      WriteJourneysFile();
      WriteLocationsFile();
      WriteOperatorsFile();
      WriteLateReasonsFile();
      WriteCancelReasonsFile();
      WriteViaRecordsFile();
      WriteCisSourceRecordsFile();

    }

  }
}
