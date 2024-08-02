using System.ComponentModel.DataAnnotations;

public class Checkout
{
    [Key]
    public int DocId { get; set; }

    public string Name { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public string Zipcode { get; set; }

    public string State { get; set; }

    public string CartListings { get; set; }
    public DateTime? DocDate { get; set; } = DateTime.Now; // Default to current date and time

    public string AvailCapital { get; set; }

    public string DesiredLoc { get; set; }

    public string TimeFrame { get; set;}

    public bool NewsLetter { get; set; }
}
