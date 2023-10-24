using System.ComponentModel.DataAnnotations;

namespace Module_02.Task_02.CatalogService.WebApi.Models;

public class ResourceLink
{
    [Required]
    public string Rel { get; init; }
    
    [Required]
    public string Href { get; init; }

    [Required]
    public string Method { get; init; }

    public ResourceLink(string rel, string href, string method)
    {
        Rel = rel;
        Href = href;
        Method = method;
    }
}