using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace authentication;
[Route("api/[controller]")]
[ApiController]

public class UserController:ControllerBase
{
    private readonly IUser _userServices;
    private readonly Ijwt _ijwt;
    private readonly IMapper _mapper;
    private readonly ResponseDto _response;
    public UserController(IUser user,Ijwt ijwt, IMapper mapper)
    {
        _userServices=user;
        _ijwt=ijwt;
        _mapper=mapper;
        _response= new ResponseDto();
    }

    [HttpPost("Register")]
    public async Task<ActionResult<ResponseDto>> RegisterUser(RegisterUserDto registerUser)
    {
        try
        {
            var new_user=_mapper.Map<User>(registerUser);
            new_user.Password=BCrypt.Net.BCrypt.HashPassword(registerUser.Password);
            var checkMail= await _userServices.GetUserByEmail(registerUser.Email);
            if(checkMail != null)
            {
                _response.Error=$"{checkMail.Email} exists!!";
                return BadRequest(_response);
            }
            var result= await _userServices.RegisterUser(new_user);
            _response.Result=result;

            return Created("",_response);

        }
        catch (Exception ex)
        {
             _response.Error = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return StatusCode(500, _response);
        }
    }

    [HttpPost("Login")]
    public async Task<ActionResult<ResponseDto>> Login(LoginRequestDto loginRequest)
    {
        try
        {
            var checkUser= await _userServices.GetUserByEmail(loginRequest.Email);
            if(checkUser == null)
            {
                _response.Error="Email does not exist";
                return BadRequest(_response);
            }

            var checkPassword=BCrypt.Net.BCrypt.Verify(loginRequest.Password,checkUser.Password);
            if (!checkPassword)
            {
                _response.Error="Wrong password";
                return BadRequest(_response);
            }

            var token= _ijwt.GenerateToken(checkUser);

            var loggedUser=new LoginResponseDto()
            {
                Token=token,
                User=checkUser

            };

            _response.Result=loggedUser;
            return Ok(_response);

        }
        catch (Exception ex)
        {
             _response.Error = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return StatusCode(500, _response);
        }
    }


    [HttpGet]
    public async Task<ActionResult<ResponseDto>> GetUsers()
    {
        try
        {
            var users= await _userServices.GetUsers();
            if(users !=null)
            {
                _response.Result=users;
                return Ok(_response);
            }

            _response.Error="Users not found";
            return NotFound(_response);

        }
        catch (Exception ex)
        {
             _response.Error = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return StatusCode(500, _response);
        }
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<ResponseDto>> GetUserById(Guid Id)
    {
        try
        {
            var user= await _userServices.GetUserById(Id);
            if(user == null)
            {
                _response.Error="User not found";
                return NotFound(_response);
            }

            _response.Result=user;
            return Ok(_response);

        }
        catch (Exception ex)
        {
             _response.Error = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return StatusCode(500, _response);
        }
    }

    [HttpGet("LoggedInUser")]
    public async Task<ActionResult<ResponseDto>> GetLoggedInUser()
    {
        try
        {
            var token= HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            var userId=User.Claims.FirstOrDefault(claim=>claim.Type == ClaimTypes.NameIdentifier)?.Value;
            if(string.IsNullOrEmpty(userId))
            {
                _response.Error="Please log in!!";
                return BadRequest(_response);
            }

            var UserId=Guid.Parse(userId);
            var user= await _userServices.GetUser(UserId,token);

            if (user == null)
            {
                _response.Error="Login first!!";
                return BadRequest(_response);
            }

            _response.Result=user;
            return Ok(_response);

        }
        catch (Exception ex)
        {
             _response.Error = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return StatusCode(500, _response);
        }
    }

    

}
