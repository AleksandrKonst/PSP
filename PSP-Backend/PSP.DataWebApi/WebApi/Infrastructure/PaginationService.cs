using System.Dynamic;

namespace WebApi.Infrastructure;

public static class PaginationService
{
    public static dynamic PaginateAsDynamic(string baseUrl, int index, int count, long total) {
        dynamic links = new ExpandoObject();
        links.self = new { href = baseUrl };
        if (index < total) {
            links.next = new { href = $"{baseUrl}?index={index + count}" };
            links.final = new { href = $"{baseUrl}?index={total - (total % count)}&count={count}" };
        }
        if (index > 0) {
            links.prev = new { href = $"{baseUrl}?index={index - count}" };
            links.first = new { href = $"{baseUrl}?index=0" };
        }
        return links;
    }
}