using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Enum
{
    public enum BookStatusEnum
    {
        [Display(Name = "Nenhum")]
        None = 0,

        [Display(Name = "Disponível")]
        Available = 1,

        [Display(Name = "Indisponível")]
        Unavailable = 2,

        [Display(Name = "Emprestado")]
        Borrowed = 3,

        [Display(Name = "Perdido")]
        Lost = 4
    }
}
