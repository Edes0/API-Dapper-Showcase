namespace Boilerplate.CoreServices.DateTimeHelpers;

public static class TimeStampGenerator
{
    public static string GenerateTimeStamp()
    {
        return $" {DateTimeOffset.UtcNow.Year}-{DateTimeOffset.UtcNow.Month}-{DateTimeOffset.UtcNow.Day}" +
               $" {DateTimeOffset.UtcNow.LocalDateTime.TimeOfDay:hh\\:mm\\:ss}.";
    }
}