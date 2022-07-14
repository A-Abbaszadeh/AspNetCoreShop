using Application.Interfaces.Contexts;
using AutoMapper;
using Domain.Users;
using System.Collections.Generic;
using System.Linq;

namespace Application.Users
{
    public class UserAddressService : IUserAddressService
    {
        private readonly IDatabaseContext _context;
        private readonly IMapper _mapper;
        public UserAddressService(IDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddNewAddress(AddUserAddressDto address)
        {
            var data = _mapper.Map<UserAddress>(address);
            _context.UserAddresses.Add(data);
            _context.SaveChanges();
        }

        public List<UserAddressDto> GetAddress(string userId)
        {
            var address = _context.UserAddresses.Where(ua => ua.UserId == userId);
            var data = _mapper.Map<List<UserAddressDto>>(address);
            return data;
        }
    }
}
