using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Products{

    [Key]
    public int PId{get;set;}

    public string PName{get;set;}

    public decimal PPrice{get;set;}

    public int PQauantity{get;set;}

    public string PDescription{get;set;}

    public string PType{get;set;}

}