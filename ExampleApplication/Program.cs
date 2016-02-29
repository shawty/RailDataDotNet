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

      Reference myReference = new Reference(@"H:\Network Rail Data\20160228020751_ref_v3.xml\20160228020751_ref_v3.xml");

      //var myLocationRefs = myReference.GetAllLocations();

      var myLocation = myReference.GetLocationsFiltered(lr => lr.Crs.Equals("NCL"));

    }

  }
}
