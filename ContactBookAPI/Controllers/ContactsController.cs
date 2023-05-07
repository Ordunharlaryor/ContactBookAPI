using AutoMapper;
using ContactBook.Core.Abstraction.Interfaces;
using ContactBook.Core.DTOs;
using ContactBook.Core.Utilities;
using ContactBook.Domain.Enums;
using ContactBook.Domain.Models;
using Core.ContactBookAPI.DTOs;
using Core.ContactBookAPI.Implementations.Services;
using Core.ContactBookAPI.Utilities;
//using Domain.ContactBookAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
 

namespace ContactBook.API.Controllers
{

    [ApiController]
   // [Authorize]
    [Route("api/Contacts")]
    
    public class ContactsController : ControllerBase
    {
        
        
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;
        private readonly IUploadService uploadService;

        public ContactsController(IContactService contactService, IMapper mapper, IUploadService uploadService)
        {
        _contactService = contactService;
        _mapper = mapper;
         uploadService = uploadService;
        }

       
        
        [HttpGet("AllPages")]
        //[Authorize(Roles = "admin")]
        
        public async Task<IActionResult> GetContactsAsync(int pageNumber = 1, int pageSize = 10)
        {
            var user = User.Claims.FirstOrDefault(u => u.Type == "role")?.Value;
            
            var pagedResult = await _contactService.GetContactsAsync(pageNumber, pageSize);

            if (pagedResult.Items == null || pagedResult.Items.Count == 0)
            {
                return NotFound();
            }

            var pagedResultDto = _mapper.Map<PagedResultDTO<ContactDTO>>(pagedResult);

            return Ok(ResponseDto<object>.Success(pagedResultDto, "Contacts were retrieved Successfuly"));
        }


        [HttpGet("GetContact/{id}")]
        //[Authorize(Roles = UsersRoleManager.ADMIN)]


        public async Task<ActionResult<ResponseDto<ContactsModel>>> GetContactById(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);

            if (contact == null)
            {
        
                return NotFound(ResponseDto<object>.Fail(new List<string> {$"The Contact with the following id = {id}, wasn't found"}));
            }
            return Ok(ResponseDto<object>.Success(contact, $"The contact with the following id = {id}, was retrieved successfully"));
        }

       
        [HttpPost("AddContacts")]
        //[Authorize(Roles = UsersRoleManager.ADMIN)]
        public async Task<IActionResult> AddContactAsync([FromBody] ContactDTO contactDTO)
        {
            if (contactDTO == null)
            {
                return BadRequest();
            }

            var contact = _mapper.Map<ContactsModel>(contactDTO);

           var result = await _contactService.AddContactAsync(contact);
      

            return Ok(ResponseDto<object>.Success(result, "Contact was added successfully"));
        }



        
        [HttpPut("Update/{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateContactAsync(int id, [FromBody] UpdateContactDTO UpdatecontactDTO)
        {
            if (UpdatecontactDTO == null)
            {
                return BadRequest("Contact cannot be null");
            }

            var existingContact = await _contactService.GetContactByIdAsync(id);
             
            if (existingContact == null)
            {
                return NotFound();
            }

            _mapper.Map(UpdatecontactDTO, existingContact);
           var response = await _contactService.UpdateContactAsync(existingContact);
    
            return Ok(ResponseDto<object>.Success(response, "Contact was updated Successfully"));
        }

       
        
        [HttpPatch("Patch/{id}")]
       // [Authorize(Roles = "ADMIN, REGULAR")]
        
        public async Task<IActionResult> PatchContactColumnsAsync(int id, [FromBody] UpdateContactDTO UpdatecontactDto)
        {
            if (UpdatecontactDto == null)
            {
                return BadRequest("Contact object cannot be null");
            }

            var existingContact = await _contactService.GetContactByIdAsync(id);
            if (existingContact == null)
            {
                return NotFound();
            }

            _mapper.Map(UpdatecontactDto, existingContact);

            if (!TryValidateModel(existingContact))
            {
                return BadRequest(ModelState);
            }

           var response = await _contactService.PatchContactColumnsAsync(existingContact);

            return Ok(ResponseDto<object>.Success(response, "Contact was patched successfully"));
        }


       
        [HttpGet("Search/{SearchTerm}")]
       // [Authorize(Roles = "ADMIN")]
        
        public async Task<IActionResult> SearchContacts(string searchTerm, int pageNumber, int pageSize)
        {

            var contacts = await _contactService.SearchContactsAsync(searchTerm, pageNumber, pageSize);
            if (contacts != null)
            {

                return NotFound();
            }

            return Ok(ResponseDto<object>.Success(contacts));
            
        }

        
        
        [HttpDelete("Delete/{id}")]
       // [Authorize(Roles = "ADMIN")]
       
        public async Task<IActionResult> DeleteContact(int id)
        {
            var result = await _contactService.DeleteContactAsync(id);
        
            return Ok(ResponseDto<object>.Success(result, "The Contact was deleted succesfully"));
        }

        [HttpPost("upload")]
       
        public async Task <IActionResult> UploadPhoto([FromForm] IFormFile model)
        {
            var uploadResult = await uploadService.UploadFileAsync(model);
            if (uploadResult != null) 
            {
                return Ok($"publicId {uploadResult["PublicId"]}, Url {uploadResult["Url"]}");
            }
            else
            {
                return BadRequest("upload failed");
            }
        }
    }
}
