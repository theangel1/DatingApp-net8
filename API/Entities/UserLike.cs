using System;

namespace API.Entities;

//está clase/tabla será la encargada de la relacion de entidades entre like y appuser. Tecnicamente ef puede hacerlo más automatico, pero 
//el nombre de la tabla y otras propiedades se vuelve... engorroso y no ideal. Por eso se hace este ejemplo
public class UserLike
{
    public AppUser SourceUser { get; set; } = null!;
    public int SourceUserId { get; set; }
    public AppUser TargetUser { get; set; } = null!;
    public int TargetUserId { get; set; }

}
