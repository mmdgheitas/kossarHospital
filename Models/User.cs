using Microsoft.AspNetCore.Identity;

namespace kosarHospital.Models
{
    public class User : IdentityUser
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string codemeli { get; set; }
        public string? shomareShenase { get; set; }

        public List<Time>? times { get; set; }
    }
}