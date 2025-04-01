using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

//esto por un tema de convención. Por experiencia, a veces estás tablas se creaban automaticamente como "photoes"
[Table("Photos")]
public class Photo
{
    public int Id { get; set; }
    public required string Url { get; set; }
    public bool IsMain { get; set; }
    public string? PublicId { get; set; }

    //navigation properties

    public int AppUserId { get; set; }

    //para que fotos no tenga un appuser nulo, ponemos null!
    public AppUser AppUser { get; set; } = null!;
}