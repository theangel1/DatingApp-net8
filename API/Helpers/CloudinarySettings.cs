using System;

namespace API.Helpers;

public class CloudinarySettings
{
    //usando nuestra config de appsettings
    public required string CloudName { get; set; }
    public required string ApiKey { get; set; }
    public required string ApiSecret { get; set; }

}
