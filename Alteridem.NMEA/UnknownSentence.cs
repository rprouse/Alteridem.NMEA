﻿namespace Alteridem.NMEA;

/// <summary>
/// Unknown sentence type
/// </summary>
public class UnknownSentence : BaseSentence
{
    public override string Description => "Unknown Sentence";

    public UnknownSentence(string sentence) : base(sentence) { }
}