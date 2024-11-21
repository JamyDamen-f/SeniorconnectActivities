using System.ComponentModel.DataAnnotations;

namespace SeniorConnectActivitiesShared.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public RolModel RolId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool Active { get; set; }
        public string GoogleId { get; set; }
        public string FacebookId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NameAffix { get; set; }
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
