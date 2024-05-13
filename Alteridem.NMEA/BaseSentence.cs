using Alteridem.NMEA.Extensions;

namespace Alteridem.NMEA;

public abstract class BaseSentence
{
    public string Sentence { get; set; }
    public string TalkerId { get; set; } = "UN";
    public string SentenceId { get; set; } = "UNK";
    public byte Checksum { get; set; } = 0x00;
    public string[] Fields { get; set; }

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
        Fields = sentence.Split(',');

        // Extract the Talker ID and Sentence ID
        if (Fields.Length > 0 && Fields[0].Length >= 7)
        {
            TalkerId = Fields[0].Substring(1, 2);
            SentenceId = Fields[0].Substring(3);
        }
    }

    public override string ToString() => Sentence;
}
