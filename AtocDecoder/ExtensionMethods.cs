namespace AtocDecoder
{
  public static class ExtensionMethods
  {
    public static int ToInt(this string source)
    {
      int temp;
      return int.TryParse(source, out temp) ? temp : 0;
    }

  }
}
