using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users
{
    public interface IUserAddressService
    {
        List<UserAddressDto> GetAddress(string userId);
        void AddNewAddress(AddUserAddressDto address);
    }
}
