namespace Alteridem.NMEA.Sentences;

/// <summary>
/// Unknown sentence type
/// </summary>
public class UnknownSentence : BaseSentence
{
    public override string Description => "Unknown Sentence";

    public UnknownSentence(string sentence) : base(sentence) { }

    public override string ToString() => $"UNK: {Sentence}";
}
