using System.ComponentModel.DataAnnotations;


namespace Pumox.Model
{
    public abstract class EnterpriseBaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int EstablishmentYear { get; set; }
    }
}
