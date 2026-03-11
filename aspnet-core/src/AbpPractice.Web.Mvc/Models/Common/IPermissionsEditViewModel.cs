using AbpPractice.Roles.Dto;
using System.Collections.Generic;

namespace AbpPractice.Web.Models.Common;

public interface IPermissionsEditViewModel
{
    List<FlatPermissionDto> Permissions { get; set; }
}