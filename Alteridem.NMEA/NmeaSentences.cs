using System;
using System.Collections.Generic;
using System.Reflection;

namespace Alteridem.NMEA;

/// <summary>
/// The NMEA Sentences that are supported by this library
/// </summary>
public static class NmeaSentences
{
    private static readonly Dictionary<string, Type> _sentences = new Dictionary<string, Type>
    {
        { "GGA", typeof(GgaSentence) },
        { "GLL", typeof(UnknownSentence) },
        { "GSA", typeof(UnknownSentence) },
        { "GSV", typeof(UnknownSentence) },
        { "RMC", typeof(UnknownSentence) },
        { "TXT", typeof(UnknownSentence) },
        { "VTG", typeof(UnknownSentence) },
    };

    public static BaseSentence? Parse(string sentence)
    {
        if (string.IsNullOrEmpty(sentence) ||
            sentence.Length < 7 ||
            !sentence.StartsWith('$'))
        {
            return new UnknownSentence(sentence);
        }

        var sentenceId = sentence.Substring(3, 3);
        if (!_sentences.ContainsKey(sentenceId))
            return new UnknownSentence(sentence);

        var type = _sentences[sentenceId];
        BaseSentence? instance = null;
        try
        {
            instance = Activator.CreateInstance(type, sentence) as BaseSentence;
        }
        catch (TargetInvocationException ex)
        {
            Console.WriteLine(ex.Message);
        }

        return instance ?? new UnknownSentence(sentence);
    }
}
