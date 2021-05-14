using System.ComponentModel.DataAnnotations;

namespace DtoModels.User
{
    public class StudentDto
    {
        [ScaffoldColumn(false)]
        public int UserId { get; set; }

        public string StudentName { get; set; }

        public bool IsAssigned { get; set; }
    }
}
