using System.ComponentModel.DataAnnotations;

namespace SeniorConnectActivities.Models.Entities
{
    public class UserModel
    {
        public int Id { get; set; }
        public RolModel RolId { get; set; }
        [Required(ErrorMessage = "Email is vereist.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is vereist.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool Active { get; set; }
        public int GoogleId { get; set; }
        public int FacebookId { get; set; }
        public string FirstName { get; set; }
        public string Affix { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        [Phone]
        public string Phonenumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string picture { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }


        public UserModel()
        {
            RolId = new RolModel();
        }
    }
}
