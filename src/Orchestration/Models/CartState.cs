using System.Collections.Generic;
using System.Linq;

namespace CaisseClaire.Orchestration.Models;

public class CartState
{
    public List<CartItem> Items { get; set; } = new List<CartItem>();
    
    public decimal TotalAmount => Items.Sum(i => i.TotalPrice);
    
    public decimal ClientMoney { get; set; }
    
    public decimal ChangeToReturn => ClientMoney >= TotalAmount ? ClientMoney - TotalAmount : 0;
}
