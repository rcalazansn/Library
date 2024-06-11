using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Enum
{
    public enum UserTypeEnm
    {
        [Display(Name = "Nenhum")]
        None = 0,

        [Display(Name = "Administrador")]
        Admin = 1,

        [Display(Name = "Professor")]
        Teacher = 2,

        [Display(Name = "Aluno")]
        Student = 3
    }
}
