using Application.Interfaces.Contexts;
using AutoMapper;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users
{
    public interface IUserAddressService
    {
        List<UserAddressDto> GetAddress(string userId);
        void AddNewAddress(AddUserAddressDto address);
    }

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

    public class UserAddressDto
    {
        public int Id { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string PostalAddress { get; set; }
        public string ReciverName { get; set; }
    }

    public class AddUserAddressDto
    {
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string PostalAddress { get; set; }
        public string ReciverName { get; set; }
        public string UserId { get; set; }
    }
}
