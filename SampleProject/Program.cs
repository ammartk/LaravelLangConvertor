// See https://aka.ms/new-console-template for more information
using LaravelLangConvertor.Parser.LangParsers;
using LaravelLangConvertor.Translator;

Console.WriteLine("Hello, World!");
var stringParser = new StringParser();
var keyValuePairs = stringParser.GetParsedLaravelKeyValuePair("common.php");
var translator = new DeepLTranslator();
var values = await translator.TranslateToLanguage(keyValuePairs, "de");
foreach(var value in values)
{
    Console.WriteLine(value);
}