using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Address { get; set; }

    public int? Phonenumber { get; set; }

    public int? IdRole { get; set; }

    public bool? Status { get; set; }

    public virtual Permission? IdRoleNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public User()
    {
        // Gọi hàm SetDefaults() trong constructor để thiết lập các giá trị mặc định
        SetDefaults();
    }

    public void SetDefaults()
    {
        Id = GenerateRandomId();
        IdRole = 2; // Gán giá trị IdRole là 2
        Status = false; // Gán giá trị Status là false

    }
    private int GenerateRandomId()
    {
        // Sử dụng thư viện System.Random để tạo số ngẫu nhiên
        Random random = new Random();
        return random.Next(1000, 9999); // Số ngẫu nhiên từ 1000 đến 9999 (có thể điều chỉnh phạm vi theo nhu cầu)
    }
}
