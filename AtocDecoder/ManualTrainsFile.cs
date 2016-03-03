using System.Collections.Generic;
using System.IO;
using AtocDecoder.Entities;

namespace AtocDecoder
{
  public class ManualTrainsFile
  {
    private List<TiplocInsertionRecord> _tiplocInsertRecords = new List<TiplocInsertionRecord>();

    private TextReader _inputFile = null;

    public List<TiplocInsertionRecord> TiplocInsertionRecords { get { return _tiplocInsertRecords; } }

    public ManualTrainsFile() { }

    public void OpenFile(string fileName)
    {
      _inputFile = new StreamReader(new FileStream(fileName, FileMode.Open));
    }

    public void CloseFile()
    {
      _inputFile.Close();
    }

    public void LoadEntireFile(string fileName)
    {
      OpenFile(fileName);
      string line;
      while ((line = _inputFile.ReadLine()) != null)
      {
        string recordType = line.Substring(0, 2);

        switch (recordType)
        {
          case "TI":
            DecodeTiplocInsertionRecord(line);
            break;
        }

      }
      CloseFile();
    }

    private void DecodeTiplocInsertionRecord(string recordLine)
    {
      string tiploc = recordLine.Substring(2, 7);
      string capsId = recordLine.Substring(9, 2);
      string nalco = recordLine.Substring(11, 6);
      string nlcCheck = recordLine.Substring(17, 1);
      string tpsDesc = recordLine.Substring(18, 26);
      string stanox = recordLine.Substring(44, 5);
      string poMcp = recordLine.Substring(49, 4);
      string crs = recordLine.Substring(53, 3);
      string capriDesc = recordLine.Substring(56, 16);

      _tiplocInsertRecords.Add(new TiplocInsertionRecord
      {
        TiplocCode = tiploc.Trim(),
        CapitalsIdentification = capsId.Trim().ToInt(),
        NationalLocationCode = nalco.Trim().ToInt(),
        NlcCheckCharacter = nlcCheck.Trim(),
        TpsDescription = tpsDesc.Trim(),
        TopsLocationCode = stanox.Trim().ToInt(),
        PostOfficeLocationCode = poMcp.Trim().ToInt(),
        CrsCode = crs.Trim(),
        CapriDescription = capriDesc.Trim()
      });
    }

  }
}
