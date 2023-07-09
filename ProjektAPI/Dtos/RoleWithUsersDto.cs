namespace ProjektAPI.Dtos
{
    public class RoleWithUsersDto : RoleDto
    {
        public List<UserDto> Users { get; set; }
    }
}
