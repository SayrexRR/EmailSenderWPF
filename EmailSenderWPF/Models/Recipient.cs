using System.Text.Json.Serialization;

namespace EmailSenderWPF.Models;

public class Recipient
{
    [JsonPropertyName("mails")]
    public List<string> Emails { get; set; }
}
