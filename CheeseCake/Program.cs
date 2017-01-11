using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Options;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Newtonsoft.Json;

namespace CheeseCake
{
  public static class Program
  {
    public static void Main (string [] args)
    {
      if (args.Length != 2)
      {
        Console.WriteLine ("Usage: CheeseCake2 FileName OptionFileName");
        return;
      }

      var sourceFileName = args [0];
      var optionFileName = args [1];

      var text = File.ReadAllText (sourceFileName);

      var csharpParseOptions = new CSharpParseOptions (LanguageVersion.CSharp6, DocumentationMode.Parse, SourceCodeKind.Regular, new List<string> { "UNITY_EDITOR", "UNITY_IOS", "UNITY_ANDROID" });

      var workspace = new AdhocWorkspace ();
      var tree = CSharpSyntaxTree.ParseText (text, csharpParseOptions);

      var userOptions = JsonConvert.DeserializeObject<UserOptions> (File.ReadAllText (optionFileName));
      var options = workspace.Options;

      // FormattingOptions
      options = options.WithChangedOption (FormattingOptions.IndentationSize, LanguageNames.CSharp, userOptions.FormattingOptions.IndentationSize);
      options = options.WithChangedOption (FormattingOptions.TabSize, LanguageNames.CSharp, userOptions.FormattingOptions.TabSize);
      options = options.WithChangedOption (FormattingOptions.UseTabs, LanguageNames.CSharp, userOptions.FormattingOptions.UseTabs);
      options = options.WithChangedOption (FormattingOptions.NewLine, LanguageNames.CSharp, Environment.NewLine);
      options = options.WithChangedOption (FormattingOptions.SmartIndent, LanguageNames.CSharp, FormattingOptions.IndentStyle.Smart);

      // CSharpFormattingOptions
      options = options.WithChangedOption (CSharpFormattingOptions.SpacingAfterMethodDeclarationName, userOptions.CSharpFormattingOptions.SpacingAfterMethodDeclarationName);
      options = options.WithChangedOption (CSharpFormattingOptions.SpaceWithinMethodDeclarationParenthesis, userOptions.CSharpFormattingOptions.SpaceWithinMethodDeclarationParenthesis);
      options = options.WithChangedOption (CSharpFormattingOptions.SpaceBetweenEmptyMethodDeclarationParentheses, userOptions.CSharpFormattingOptions.SpaceBetweenEmptyMethodDeclarationParentheses);
      options = options.WithChangedOption (CSharpFormattingOptions.SpaceAfterMethodCallName, userOptions.CSharpFormattingOptions.SpaceAfterMethodCallName);
      options = options.WithChangedOption (CSharpFormattingOptions.SpaceWithinMethodCallParentheses, userOptions.CSharpFormattingOptions.SpaceWithinMethodCallParentheses);
      options = options.WithChangedOption (CSharpFormattingOptions.SpaceBetweenEmptyMethodCallParentheses, userOptions.CSharpFormattingOptions.SpaceBetweenEmptyMethodCallParentheses);
      options = options.WithChangedOption (CSharpFormattingOptions.SpaceAfterControlFlowStatementKeyword, userOptions.CSharpFormattingOptions.SpaceAfterControlFlowStatementKeyword);
      options = options.WithChangedOption (CSharpFormattingOptions.SpaceWithinExpressionParentheses, userOptions.CSharpFormattingOptions.SpaceWithinExpressionParentheses);
      options = options.WithChangedOption (CSharpFormattingOptions.SpaceWithinCastParentheses, userOptions.CSharpFormattingOptions.SpaceWithinCastParentheses);
      options = options.WithChangedOption (CSharpFormattingOptions.SpaceWithinOtherParentheses, userOptions.CSharpFormattingOptions.SpaceWithinOtherParentheses);
      options = options.WithChangedOption (CSharpFormattingOptions.SpaceAfterCast, userOptions.CSharpFormattingOptions.SpaceAfterCast);
      options = options.WithChangedOption (CSharpFormattingOptions.SpacesIgnoreAroundVariableDeclaration, userOptions.CSharpFormattingOptions.SpacesIgnoreAroundVariableDeclaration);
      options = options.WithChangedOption (CSharpFormattingOptions.SpaceBeforeOpenSquareBracket, userOptions.CSharpFormattingOptions.SpaceBeforeOpenSquareBracket);
      options = options.WithChangedOption (CSharpFormattingOptions.SpaceBetweenEmptySquareBrackets, userOptions.CSharpFormattingOptions.SpaceBetweenEmptySquareBrackets);
      options = options.WithChangedOption (CSharpFormattingOptions.SpaceWithinSquareBrackets, userOptions.CSharpFormattingOptions.SpaceWithinSquareBrackets);
      options = options.WithChangedOption (CSharpFormattingOptions.SpaceAfterColonInBaseTypeDeclaration, userOptions.CSharpFormattingOptions.SpaceAfterColonInBaseTypeDeclaration);
      options = options.WithChangedOption (CSharpFormattingOptions.SpaceAfterComma, userOptions.CSharpFormattingOptions.SpaceAfterComma);
      options = options.WithChangedOption (CSharpFormattingOptions.SpaceAfterDot, userOptions.CSharpFormattingOptions.SpaceAfterDot);
      options = options.WithChangedOption (CSharpFormattingOptions.SpaceAfterSemicolonsInForStatement, userOptions.CSharpFormattingOptions.SpaceAfterSemicolonsInForStatement);
      options = options.WithChangedOption (CSharpFormattingOptions.SpaceBeforeColonInBaseTypeDeclaration, userOptions.CSharpFormattingOptions.SpaceBeforeColonInBaseTypeDeclaration);
      options = options.WithChangedOption (CSharpFormattingOptions.SpaceBeforeComma, userOptions.CSharpFormattingOptions.SpaceBeforeComma);
      options = options.WithChangedOption (CSharpFormattingOptions.SpaceBeforeDot, userOptions.CSharpFormattingOptions.SpaceBeforeDot);
      options = options.WithChangedOption (CSharpFormattingOptions.SpaceBeforeSemicolonsInForStatement, userOptions.CSharpFormattingOptions.SpaceBeforeSemicolonsInForStatement);
      options = options.WithChangedOption (CSharpFormattingOptions.SpacingAroundBinaryOperator, userOptions.CSharpFormattingOptions.SpacingAroundBinaryOperator);
      options = options.WithChangedOption (CSharpFormattingOptions.IndentBraces, userOptions.CSharpFormattingOptions.IndentBraces);
      options = options.WithChangedOption (CSharpFormattingOptions.IndentBlock, userOptions.CSharpFormattingOptions.IndentBlock);
      options = options.WithChangedOption (CSharpFormattingOptions.IndentSwitchSection, userOptions.CSharpFormattingOptions.IndentSwitchSection);
      options = options.WithChangedOption (CSharpFormattingOptions.IndentSwitchCaseSection, userOptions.CSharpFormattingOptions.IndentSwitchCaseSection);
      options = options.WithChangedOption (CSharpFormattingOptions.LabelPositioning, userOptions.CSharpFormattingOptions.LabelPositioning);
      options = options.WithChangedOption (CSharpFormattingOptions.WrappingPreserveSingleLine, userOptions.CSharpFormattingOptions.WrappingPreserveSingleLine);
      options = options.WithChangedOption (CSharpFormattingOptions.WrappingKeepStatementsOnSingleLine, userOptions.CSharpFormattingOptions.WrappingKeepStatementsOnSingleLine);
      options = options.WithChangedOption (CSharpFormattingOptions.NewLinesForBracesInTypes, userOptions.CSharpFormattingOptions.NewLinesForBracesInTypes);
      options = options.WithChangedOption (CSharpFormattingOptions.NewLinesForBracesInMethods, userOptions.CSharpFormattingOptions.NewLinesForBracesInMethods);
      options = options.WithChangedOption (CSharpFormattingOptions.NewLinesForBracesInProperties, userOptions.CSharpFormattingOptions.NewLinesForBracesInProperties);
      options = options.WithChangedOption (CSharpFormattingOptions.NewLinesForBracesInAccessors, userOptions.CSharpFormattingOptions.NewLinesForBracesInAccessors);
      options = options.WithChangedOption (CSharpFormattingOptions.NewLinesForBracesInAnonymousMethods, userOptions.CSharpFormattingOptions.NewLinesForBracesInAnonymousMethods);
      options = options.WithChangedOption (CSharpFormattingOptions.NewLinesForBracesInControlBlocks, userOptions.CSharpFormattingOptions.NewLinesForBracesInControlBlocks);
      options = options.WithChangedOption (CSharpFormattingOptions.NewLinesForBracesInAnonymousTypes, userOptions.CSharpFormattingOptions.NewLinesForBracesInAnonymousTypes);
      options = options.WithChangedOption (CSharpFormattingOptions.NewLinesForBracesInObjectCollectionArrayInitializers, userOptions.CSharpFormattingOptions.NewLinesForBracesInObjectCollectionArrayInitializers);
      options = options.WithChangedOption (CSharpFormattingOptions.NewLinesForBracesInLambdaExpressionBody, userOptions.CSharpFormattingOptions.NewLinesForBracesInLambdaExpressionBody);
      options = options.WithChangedOption (CSharpFormattingOptions.NewLineForElse, userOptions.CSharpFormattingOptions.NewLineForElse);
      options = options.WithChangedOption (CSharpFormattingOptions.NewLineForCatch, userOptions.CSharpFormattingOptions.NewLineForCatch);
      options = options.WithChangedOption (CSharpFormattingOptions.NewLineForFinally, userOptions.CSharpFormattingOptions.NewLineForFinally);
      options = options.WithChangedOption (CSharpFormattingOptions.NewLineForMembersInObjectInit, userOptions.CSharpFormattingOptions.NewLineForMembersInObjectInit);
      options = options.WithChangedOption (CSharpFormattingOptions.NewLineForMembersInAnonymousTypes, userOptions.CSharpFormattingOptions.NewLineForMembersInAnonymousTypes);
      options = options.WithChangedOption (CSharpFormattingOptions.NewLineForClausesInQuery, userOptions.CSharpFormattingOptions.NewLineForClausesInQuery);

      var formattedRoot = Formatter.Format (tree.GetRoot (), workspace, options);
      var formattedText = formattedRoot.ToFullString ();

      File.WriteAllText (sourceFileName, formattedText, Encoding.UTF8);
    }
  }

  public class UserOptions
  {
    public UserFormattingOptions FormattingOptions { get; set; }
    public UserCSharpFormattingOptions CSharpFormattingOptions { get; set; }
  }

  public class UserFormattingOptions
  {
    public int IndentationSize { get; set; }
    public bool UseTabs { get; set; }
    public int TabSize { get; set; }
  }

  public class UserCSharpFormattingOptions
  {
    public bool SpacingAfterMethodDeclarationName { get; set; }
    public bool SpaceWithinMethodDeclarationParenthesis { get; set; }
    public bool SpaceBetweenEmptyMethodDeclarationParentheses { get; set; }
    public bool SpaceAfterMethodCallName { get; set; }
    public bool SpaceWithinMethodCallParentheses { get; set; }
    public bool SpaceBetweenEmptyMethodCallParentheses { get; set; }
    public bool SpaceAfterControlFlowStatementKeyword { get; set; }
    public bool SpaceWithinExpressionParentheses { get; set; }
    public bool SpaceWithinCastParentheses { get; set; }
    public bool SpaceWithinOtherParentheses { get; set; }
    public bool SpaceAfterCast { get; set; }
    public bool SpacesIgnoreAroundVariableDeclaration { get; set; }
    public bool SpaceBeforeOpenSquareBracket { get; set; }
    public bool SpaceBetweenEmptySquareBrackets { get; set; }
    public bool SpaceWithinSquareBrackets { get; set; }
    public bool SpaceAfterColonInBaseTypeDeclaration { get; set; }
    public bool SpaceAfterComma { get; set; }
    public bool SpaceAfterDot { get; set; }
    public bool SpaceAfterSemicolonsInForStatement { get; set; }
    public bool SpaceBeforeColonInBaseTypeDeclaration { get; set; }
    public bool SpaceBeforeComma { get; set; }
    public bool SpaceBeforeDot { get; set; }
    public bool SpaceBeforeSemicolonsInForStatement { get; set; }
    public BinaryOperatorSpacingOptions SpacingAroundBinaryOperator { get; set; }
    public bool IndentBraces { get; set; }
    public bool IndentBlock { get; set; }
    public bool IndentSwitchSection { get; set; }
    public bool IndentSwitchCaseSection { get; set; }
    public LabelPositionOptions LabelPositioning { get; set; }
    public bool WrappingPreserveSingleLine { get; set; }
    public bool WrappingKeepStatementsOnSingleLine { get; set; }
    public bool NewLinesForBracesInTypes { get; set; }
    public bool NewLinesForBracesInMethods { get; set; }
    public bool NewLinesForBracesInProperties { get; set; }
    public bool NewLinesForBracesInAccessors { get; set; }
    public bool NewLinesForBracesInAnonymousMethods { get; set; }
    public bool NewLinesForBracesInControlBlocks { get; set; }
    public bool NewLinesForBracesInAnonymousTypes { get; set; }
    public bool NewLinesForBracesInObjectCollectionArrayInitializers { get; set; }
    public bool NewLinesForBracesInLambdaExpressionBody { get; set; }
    public bool NewLineForElse { get; set; }
    public bool NewLineForCatch { get; set; }
    public bool NewLineForFinally { get; set; }
    public bool NewLineForMembersInObjectInit { get; set; }
    public bool NewLineForMembersInAnonymousTypes { get; set; }
    public bool NewLineForClausesInQuery { get; set; }
  }
}
