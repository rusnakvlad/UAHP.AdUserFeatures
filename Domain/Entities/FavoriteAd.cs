using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class FavoriteAd
{
    public string Id { get; set; }
    public string AdShortInfoId { get; set; }
    public string UserId { get; set; }
}
