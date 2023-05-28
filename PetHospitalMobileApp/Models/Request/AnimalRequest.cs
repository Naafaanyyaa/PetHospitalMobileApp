using PetHospital.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace PetHospital.Business.Models.Request
{
    public class AnimalRequest
    {
        public string? ClinicId { get; set; }
        public string UserId { get; set; }
        public string? AnimalName { get; set; }
        public string? AnimalDescription { get; set; }
        public AnimalType AnimalType { get; set; }
    }
}
