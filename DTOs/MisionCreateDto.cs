using System.ComponentModel.DataAnnotations;

namespace DSW_3P_PR01.DTOs;

public class MisionCreateDto
{
    [Required(ErrorMessage = "El nombre de la misión es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La dificultad es obligatoria.")]
    [RegularExpression("^(Fácil|Media|Difícil)$", ErrorMessage = "La dificultad únicamente podrá ser 'Fácil', 'Media' o 'Difícil'.")]
    public string Dificultad { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "La recompensa XP no puede ser negativa.")]
    public int RecompensaXP { get; set; }
    
    public bool Completada { get; set; } = false;
}