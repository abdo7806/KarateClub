using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    [Required(ErrorMessage = "اسم المستخدم مطلوب")]
    public string Username { get; set; }

    [Required(ErrorMessage = "كلمة المرور مطلوبة")]
    [DataType(DataType.Password)]
    public string Password { get; set; }



}