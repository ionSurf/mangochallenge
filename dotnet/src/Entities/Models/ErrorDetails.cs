using System.Text.Json;
using System.Text.Json.Serialization;

namespace Entities.Models {
    public class ErrorDetails : IErrorDetails {
        
        
        public override string ToString() {
            return JsonSerializer.Serialize(this);
        }
    }
}