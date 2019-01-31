using System;

namespace communication.Models{
  public class CommunicationRecord {
    public DateTime CreationTime {get;} = DateTime.UtcNow;
    public DateTime LastUpdated {get;set;}
    public string Method {get;set;}
    public string EmailAddress {get;set;}
    public string PhoneNumber {get;set;}
    public string[] Parameters {get;set;}
    public string TemplateId {get;set;}
    public string Status {get;set;}
  }
}