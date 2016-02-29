using System;
using System.Xml.Linq;

namespace XmlExtensionMethods
{
  public static class ExtensionMethods
  {
    public static string TryGetAttributeString(this XElement sourceElement, string attributeName)
    {
      return sourceElement.Attribute(attributeName) != null ? sourceElement.Attribute(attributeName).Value : String.Empty;
    }

    public static int TryGetAttributeInteger(this XElement sourceElement, string attributeName)
    {
      string attributeData = sourceElement.Attribute(attributeName) != null ? sourceElement.Attribute(attributeName).Value : String.Empty;
      if(String.IsNullOrEmpty(attributeData))
      {
        return 0;
      }

      int tempValue;
      if(!int.TryParse(attributeData, out tempValue))
      {
        return 0;
      }

      return tempValue;
    }

    public static short TryGetAttributeShort(this XElement sourceElement, string attributeName)
    {
      string attributeData = sourceElement.Attribute(attributeName) != null ? sourceElement.Attribute(attributeName).Value : String.Empty;
      if (String.IsNullOrEmpty(attributeData))
      {
        return 0;
      }

      short tempValue;
      if (!short.TryParse(attributeData, out tempValue))
      {
        return 0;
      }

      return tempValue;
    }

    public static bool TryGetAttributeBoolean(this XElement sourceElement, string attributeName)
    {
      string attributeData = sourceElement.Attribute(attributeName) != null ? sourceElement.Attribute(attributeName).Value : String.Empty;
      if (String.IsNullOrEmpty(attributeData))
      {
        return false;
      }

      bool tempValue;
      if (!bool.TryParse(attributeData, out tempValue))
      {
        return false;
      }

      return tempValue;
    }

    public static DateTime TryGetAttributeDateTime(this XElement sourceElement, string attributeName)
    {
      string attributeData = sourceElement.Attribute(attributeName) != null ? sourceElement.Attribute(attributeName).Value : String.Empty;
      if (String.IsNullOrEmpty(attributeData))
      {
        return new DateTime();
      }

      DateTime tempValue;
      if (!DateTime.TryParse(attributeData, out tempValue))
      {
        return new DateTime();
      }

      return tempValue;
    }

  }
}
