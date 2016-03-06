using System.Collections.Generic;
using System.IO;
using AtocDecoder.Entities;

namespace AtocDecoder
{
  public class ManualTrainsFile
  {
    private List<TiplocInsertionRecord> _tiplocInsertRecords = new List<TiplocInsertionRecord>();
    private List<TiplocAmendRecord> _tiplocAmendRecords = new List<TiplocAmendRecord>();
    private List<TiplocDeleteRecord> _tiplocDeleteRecords = new List<TiplocDeleteRecord>();

    private TextReader _inputFile = null;

    public List<TiplocInsertionRecord> TiplocInsertionRecords { get { return _tiplocInsertRecords; } }
    public List<TiplocDeleteRecord> TiplocDeleteRecords { get { return _tiplocDeleteRecords; } }
    public List<TiplocAmendRecord> TiplocAmendRecords { get { return _tiplocAmendRecords; } }

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

          case "TA":
            DecodeTiplocAmendRecord(line);
            break;

          case "TD":
            DecodeTiplocDeleteRecord(line);
            break;

        }

      }
      CloseFile();
    }

    private void DecodeTiplocInsertionRecord(string recordLine)
    {
      List<string> recordStrings = recordLine.ToClipList(new[] {2, 7, 2, 6, 1, 26, 5, 4, 3, 16, 8});

      _tiplocInsertRecords.Add(new TiplocInsertionRecord
      {
        TiplocCode = recordStrings[1].Trim(),
        CapitalsIdentification = recordStrings[2].Trim().ToInt(),
        NationalLocationCode = recordStrings[3].Trim().ToInt(),
        NlcCheckCharacter = recordStrings[4].Trim(),
        TpsDescription = recordStrings[5].Trim(),
        TopsLocationCode = recordStrings[6].Trim().ToInt(),
        PostOfficeLocationCode = recordStrings[7].Trim().ToInt(),
        CrsCode = recordStrings[8].Trim(),
        CapriDescription = recordStrings[9].Trim()
      });
    }

    private void DecodeTiplocAmendRecord(string recordLine)
    {
      List<string> recordStrings = recordLine.ToClipList(new[] {2, 7, 2, 6, 1, 26, 5, 4, 3, 16, 7, 1});

      _tiplocAmendRecords.Add(new TiplocAmendRecord
      {
        TiplocCode = recordStrings[1].Trim(),
        CapitalsIdentification = recordStrings[2].Trim().ToInt(),
        NationalLocationCode = recordStrings[3].Trim().ToInt(),
        NlcCheckCharacter = recordStrings[4].Trim(),
        TpsDescription = recordStrings[5].Trim(),
        TopsLocationCode = recordStrings[6].Trim().ToInt(),
        PostOfficeLocationCode = recordStrings[7].Trim().ToInt(),
        CrsCode = recordStrings[8].Trim(),
        CapriDescription = recordStrings[9].Trim(),
        NewTiplocCode = recordStrings[10].Trim()
      });
    }

    private void DecodeTiplocDeleteRecord(string recordLine)
    {
      List<string> recordStrings = recordLine.ToClipList(new[] { 2, 7, 71 });

      _tiplocDeleteRecords.Add(new TiplocDeleteRecord
      {
        TiplocCode = recordStrings[1].Trim()
      });
    }

  }
}
