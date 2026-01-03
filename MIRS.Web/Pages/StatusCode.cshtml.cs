using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MIRS.Web.Pages;

public class StatusCodeModel : PageModel
{
    public int StatusCodeValue { get; set; }
    public string StatusMessage { get; set; }
    public string? ExceptionMessage { get; set; }

    public void OnGet(int code)
    {
        StatusCodeValue = code;
        ExceptionMessage = HttpContext.Items["ExceptionMessage"] as string;

        StatusMessage = code switch
        {
            400 => "Bad Request - The server could not understand the request due to invalid syntax or business rule violation.",
            401 => "Unauthorized - You do not have permission to access this resource.",
            403 => "Forbidden - You are not allowed to access this resource.",
            404 => "Not Found - The resource you are looking for could not be found.",
            500 => "Internal Server Error - Something went wrong on our end.",
            _ => "An unexpected error occurred."
        };
    }
}
