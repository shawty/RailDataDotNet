using System.Collections.Generic;

namespace AtocDecoder
{
  public static class ExtensionMethods
  {
    public static int ToInt(this string source)
    {
      int temp;
      return int.TryParse(source, out temp) ? temp : 0;
    }

    public static List<string> ToClipList(this string source, int[] clipSizes)
    {
      List<string> result = new List<string>();

      int stringIndex = 0;
      foreach (int clipSize in clipSizes)
      {
        result.Add(source.Substring(stringIndex, clipSize));
        stringIndex += clipSize;
        if (stringIndex > source.Length)
        {
          break;
        }
      }
      
      return result;
    }

  }
}
