using ReferenceFile;
using TimetableFile;

namespace ExampleApplication
{
  class Program
  {
    static void Main(string[] args)
    {
      //Timetable myTimetable = new Timetable(@"H:\Network Rail Data\20160228020751_v8.xml\20160228020751_v8.xml");
      //var myJourneys = myTimetable.GetAllJourneys();
      //var myJourney = myTimetable.GetJourneysFiltered(j => j.TrainUid == "C10572");

      Reference myReference = new Reference(@"D:\Data\National Rail\Network Rail Data\20160228020751_ref_v3.xml\20160228020751_ref_v3.xml");

      //var myLocationRefs = myReference.GetAllLocations();
      //var myLocation = myReference.GetLocationsFiltered(lr => lr.CrsCode.Equals("NCL"));

      //var myTrainOperators = myReference.GetAllOperators();
      //var myTrainOperator = myReference.GetOperatorsFiltered(toc => toc.TrainOperatorCode.Equals("XC"));

      //var myLateRunningReasons = myReference.GetAllLateReasons();
      //var myLateRunningReason = myReference.GetLateReasonsFiltered(r => r.CisCode.Equals(100));

      //var myCancelationReasons = myReference.GetAllCancelationReasons();
      //var myCancelationReason = myReference.GetCancelationReasonsFiltered(r => r.CisCode.Equals(100));

      //var myViaRecords = myReference.GetAllViaRecords();
      //var myfilteredViaRecords = myReference.GetViaRecordsFiltered(v => v.DestinationStation.Equals("CAMBDGE"));

      var myCisSourceReferences = myReference.GetAllCisSourceReferences();
      var myCisSourceReferencesFiltered = myReference.GetCisSourceReferencesFiltered(c => c.CisCode.Equals("AM01"));

    }

  }
}
