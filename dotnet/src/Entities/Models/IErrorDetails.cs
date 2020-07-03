using System.Text.Json;
using System.Text.Json.Serialization;

namespace Entities.Models {
    public class IErrorDetails {
        public int StatusCode {get;set;}
        public string Message {get;set;}
    }
}