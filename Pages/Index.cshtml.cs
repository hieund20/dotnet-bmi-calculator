using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BMI.Pages;

public enum BMI_STATUS
{
    Underweight,
    Normal,
    Overweight,
    Obesity1,
    Obesity2,
    Obesity3
}

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    [BindProperty]
    [Required(ErrorMessage = "You must type the weight")]
    [Range(0, double.MaxValue, ErrorMessage = "The {0} must be a positive number")]
    public double weight { get; set; } = 0;

    [BindProperty]
    [Required(ErrorMessage = "You must type the height")]
    [Range(0, double.MaxValue, ErrorMessage = "The {0} must be a positive number")]
    public double height { get; set; } = 0;

    public double bmi { get; set; } = 0;

    public string status { get; set; } = "";

    public string statusLevel { get; set; } = "";

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }

    public IActionResult OnPostCalculate()
    {
        if (!ModelState.IsValid)
        {
            // Validation failed; return to the page with error messages
            return Page();
        }

        this.bmi = double.Parse((this.weight /
                    ((this.height * 0.01) * (this.height * 0.01)))
                    .ToString("0.00"));

        if (this.bmi <= 18.5)
        {
            this.status = $"{BMI_STATUS.Underweight}";
            this.statusLevel = $"{BMI_STATUS.Underweight}";
        }
        else if (this.bmi >= 18.5 && this.bmi <= 24.9)
        {
            this.status = $"{BMI_STATUS.Normal}";
            this.statusLevel = $"{BMI_STATUS.Normal}";
        }
        else if (this.bmi >= 25 && this.bmi <= 29.9)
        {
            this.status = $"{BMI_STATUS.Overweight}";
            this.statusLevel = $"{BMI_STATUS.Overweight}";
        }
        else if (this.bmi >= 30 && this.bmi <= 34.9)
        {
            this.status = "Obesity (Class I)";
            this.statusLevel = $"{BMI_STATUS.Obesity1}";
        }
        else if (this.bmi >= 35 && this.bmi <= 39.9)
        {
            this.status = "Obesity (Class II)";
            this.statusLevel = $"{BMI_STATUS.Obesity2}";
        }
        else
        {
            this.status = "Obesity (Class III)";
            this.statusLevel = $"{BMI_STATUS.Obesity3}";
        }
        return Page();
    }

    public IActionResult OnPostClear()
    {
        this.height = 0;
        this.weight = 0;
        this.bmi = 0;
        this.status = String.Empty;

        return RedirectToPage("Index");
    }
}
