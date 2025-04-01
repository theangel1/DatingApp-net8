using System;

namespace API.Extensions;

public static class DateTimeExtensions
{
    public static int CalculateAge(this DateOnly dob)//date of birth
    {
        var today = DateOnly.FromDateTime(DateTime.Now);

        var age = today.Year - dob.Year;

        if(dob > today.AddYears(-age)) age--;

        return age;

    }

}
