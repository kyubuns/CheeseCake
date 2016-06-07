using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Linq;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.Editor;

namespace CheeseCake
{
  class MainClass
  {
    public static void Main(string[] args)
    {
      if (args.Length != 2) {
        Console.WriteLine ("Usage: CheeseCake FileName OptionFileName");
        return;
      }

      var sourceFileName = args [0];
      var optionFileName = args [1];

      var policy = LoadFormattingProfile (optionFileName);
      var options = LoadTextEditorProfile (optionFileName);
      var settings = new CompilerSettings ();
      settings.ConditionalSymbols.Add ("UNITY_EDITOR");
      settings.ConditionalSymbols.Add ("UNITY_IOS");
      settings.ConditionalSymbols.Add ("UNITY_ANDROID");
      var formatter = new CSharpFormatter (policy, options);

      var text = File.ReadAllText(sourceFileName);
      var document = new StringBuilderDocument (text);

      var syntaxTree = SyntaxTree.Parse (document, document.FileName, settings);
      var changes = formatter.AnalyzeFormatting(document, syntaxTree);
      changes.ApplyChanges();

      File.WriteAllText (sourceFileName, document.Text, Encoding.UTF8);
    }

    public static CSharpFormattingOptions LoadFormattingProfile (string selectedFile)
    {
      using (var stream = System.IO.File.OpenRead (selectedFile)) {
        return LoadFormattingProfile (stream);
      }
    }

    public static CSharpFormattingOptions LoadFormattingProfile (System.IO.Stream input)
    {
      CSharpFormattingOptions result = FormattingOptionsFactory.CreateMono();
      result.Name = "noname";
      using (XmlTextReader reader = new XmlTextReader (input)) {
        reader.ReadToFollowing ("Options");
        reader.ReadToDescendant ("FormattingProfile");
        while (reader.Read()) {
          if (reader.NodeType == XmlNodeType.Element) {
            if (reader.LocalName == "Property") {
              var info = typeof(CSharpFormattingOptions).GetProperty (reader.GetAttribute ("name"));
              string valString = reader.GetAttribute ("value");
              object value;
              if (info.PropertyType == typeof(bool)) {
                value = Boolean.Parse (valString);
              } else if (info.PropertyType == typeof(int)) {
                value = Int32.Parse (valString);
              } else {
                value = Enum.Parse (info.PropertyType, valString);
              }
              info.SetValue (result, value, null);
            } else if (reader.LocalName == "FormattingProfile") {
              result.Name = reader.GetAttribute ("name");
            }
          } else if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == "FormattingProfile") {
            return result;
          }
        }
      }
      return result;
    }

    public static TextEditorOptions LoadTextEditorProfile (string selectedFile)
    {
      using (var stream = System.IO.File.OpenRead (selectedFile)) {
        return LoadTextEditorProfile (stream);
      }
    }

    public static TextEditorOptions LoadTextEditorProfile (System.IO.Stream input)
    {
      TextEditorOptions result = new TextEditorOptions ();
      using (XmlTextReader reader = new XmlTextReader (input)) {
        reader.ReadToFollowing ("Options");
        reader.ReadToDescendant ("TextEditorProfile");
        while (reader.Read()) {
          if (reader.NodeType == XmlNodeType.Element) {
            if (reader.LocalName == "Property") {
              var info = typeof(TextEditorOptions).GetProperty (reader.GetAttribute ("name"));
              string valString = reader.GetAttribute ("value");
              object value;
              if (info.PropertyType == typeof(bool)) {
                value = Boolean.Parse (valString);
              } else if (info.PropertyType == typeof(int)) {
                value = Int32.Parse (valString);
              } else if (info.PropertyType == typeof(string)) {
                value = valString;
              } else {
                value = Enum.Parse (info.PropertyType, valString);
              }
              info.SetValue (result, value, null);
            }
          } else if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == "TextEditorProfile") {
            return result;
          }
        }
      }
      return result;
    }
  }
}
