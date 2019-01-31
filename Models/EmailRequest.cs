using System;

namespace communication.Models
{
  public class EmailRequest
  {
    public string Recipient { get; set; }
    public string Content { get; set; }
  }
}