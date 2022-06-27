using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Entity.Models;
using WebApplication5.ViewModel;

namespace WebApplication5.Mapper
{
    public static class UserMapper
    {
        public static User ToModel(this UserModel viewModel, User model)
        {
            model.FirstName = viewModel.firstName;
            model.LastName = viewModel.lastName;
            model.UserType = viewModel.userType.ToString();
            model.Gender = viewModel.gender;
            model.DateOfBirth = viewModel.dateOfBirth;
            model.Address = viewModel.address;
            model.City = viewModel.city;
            model.Country = viewModel.country;
            model.Address = viewModel.address;
            model.Email = viewModel.email;

            return model;
        }
    }
}
