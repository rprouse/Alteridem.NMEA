using Alteridem.NMEA.Extensions;

namespace Alteridem.NMEA;

public abstract class BaseSentence
{
    public string Sentence { get; }
    public string TalkerId { get; } = "UN";
    public string SentenceId { get; } = "UNK";
    public byte Checksum { get; } = 0x00;
    public string[] Fields { get; }

    public abstract string Description { get; }

    protected BaseSentence(string sentence)
    {
        // Split out the checksum if it exists
        var parts = sentence.Split('*');
        if (parts.Length == 2)
        {
            Sentence = parts[0];
            Checksum = parts[1].ParseByte();
        }
        else
        {
            Sentence = sentence;
        }

        // Split the sentence into the constituent fields
        Fields = Sentence.Split(',');

        // Extract the Talker ID and Sentence ID
        if (Fields.Length > 0 && Fields[0].Length >= 7)
        {
            TalkerId = Fields[0].Substring(1, 2);
            SentenceId = Fields[0].Substring(3);
        }
    }

    public override string ToString() => Sentence;
}
