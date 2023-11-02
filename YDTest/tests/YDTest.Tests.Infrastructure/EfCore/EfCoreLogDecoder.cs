using System.Text.RegularExpressions;

namespace YDTest.Tests.Infrastructure.EfCore;

internal class EfCoreLogDecoder
{
    private const string EfCoreCommandExecutedEventId = "Microsoft.EntityFrameworkCore.Database.Command.CommandExecuting";

    private static readonly Regex ParamRegex = new Regex("(@p\\d+|@__\\w*?_\\d+)='(.*?)'(\\s\\(\\w*?\\s=\\s\\w*\\))*(?:,\\s|\\]).*?");

    private const string ParameterStart = "[Parameters=[";

    private readonly string _paramName;

    private readonly string _paramValue;

    private readonly string[] _paramTypes;

    private EfCoreLogDecoder(Match matchedParam)
    {
        _paramName = matchedParam.Groups[1].Value;
        _paramValue = matchedParam.Groups[2].Value;
        _paramTypes = (from Capture x in matchedParam.Groups[3].Captures
                       select x.Value).ToArray();
    }

    private string ValueToInsert()
    {
        if (_paramValue == string.Empty)
        {
            return "NULL";
        }

        if (_paramTypes.Any())
        {
            return "'" + _paramValue.Replace("'", "''") + "'";
        }

        if (!(_paramValue == "True") && !(_paramValue == "False"))
        {
            return "'" + _paramValue + "'";
        }

        if (_paramValue == "True")
        {
            return "1";
        }

        return "0";
    }

    public override string ToString()
    {
        string text = string.Join(",", _paramTypes);
        return _paramName + "=" + ValueToInsert() + ", " + text;
    }

    public static string DecodeMessage(LogOutput log)
    {
        if (log.EventId.Name != "Microsoft.EntityFrameworkCore.Database.Command.CommandExecuting")
        {
            return log.Message;
        }

        string[] array = (from x in log.Message.Split('\n')
                          select x.Trim()).ToArray();
        int num = array[0].IndexOf("[Parameters=[");
        if (num <= 0)
        {
            return log.Message;
        }

        List<EfCoreLogDecoder> list = (from Match x in ParamRegex.Matches(array[0].Substring(num + "[Parameters=[".Length))
                                       select new EfCoreLogDecoder(x)).ToList();
        if (list.All((EfCoreLogDecoder x) => x._paramValue == "?"))
        {
            return log.Message;
        }

        list.Reverse();
        for (int i = 1; i < array.Length; i++)
        {
            string text = array[i];
            foreach (EfCoreLogDecoder item in list)
            {
                text = text.Replace(item._paramName, item.ValueToInsert());
            }

            array[i] = text;
        }

        return string.Join("\r\n", array);
    }
}
