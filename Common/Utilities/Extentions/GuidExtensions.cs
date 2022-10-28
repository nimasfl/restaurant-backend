namespace Restaurant.Common;

public static class GuidExtensions
{
    public static bool IsDefault(this Guid id)
    {
        if (!Guid.TryParse(id.ToString(), out _))
        {
            return true;
        }

        return id == new Guid("00000000-0000-0000-0000-000000000000");
    }
        
    public static bool IsNullOrDefault(this Guid? id)
    {
        if (id == null)
        {
            return true;
        }
        if (!Guid.TryParse(id.ToString(), out _))
        {
            return true;
        }

        return id == new Guid("00000000-0000-0000-0000-000000000000");
    }
    
    public static Guid? ParseGuidOrNull(this string stringGuid)
    {
        try
        {
            return new Guid(stringGuid);
        }
        catch
        {
            return null;
        }
    }
}
