using api.Dtos.SubCategory;

namespace api.Controllers
{
    [ApiController]
    [Route("api/subCategories")]
   
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IFileService _fileService;
        private readonly IStringLocalizer<SubCategoryController> _localizer;

        public SubCategoryController(ISubCategoryRepository subCategoryRepository, IFileService fileService, IStringLocalizer<SubCategoryController> localizer)
        {
            _subCategoryRepository = subCategoryRepository;
            _fileService = fileService;
            _localizer = localizer;
        }

        [HttpGet]
        [ApiExplorerSettings(GroupName = "v1-admin")]
        public async Task<ActionResult<List<SubCategoryDto>>> GetAll()
        {
            var subCategories = await _subCategoryRepository.GetAllAsync();
            return Ok(subCategories.Select(x => x.ToSubCategoryDto()).ToList());
        }

        [HttpGet("{id:int}")]
        [ApiExplorerSettings(GroupName = "v1-customer")]
        public async Task<ActionResult<SubCategoryDto>> GetSubCategoryById(int id)
        {
            var subCategory = await _subCategoryRepository.GetSubCategoryByIdAsync(id);
            if (subCategory == null)
            {
                return NotFound(_localizer.GetString(AppStrings.subCategoryNotFound).Value);
            }
            return Ok(subCategory.ToSubCategoryDto());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ApiExplorerSettings(GroupName = "v1-admin")]
        public async Task<ActionResult<SubCategoryDto>> CreateSubCategory([FromForm] SubCategoryCreateRequestDto subCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imagePath = await _fileService.SaveFileAsync(subCategoryDto.Image, "images", "subCategories");
            var subCategory = subCategoryDto.ToSubCategoryFromCreateRequestDto(imagePath);
            var createSubCategory = await _subCategoryRepository.CreateSubCategoryAsync(subCategory);

            if (createSubCategory == null)
            {
                return BadRequest(_localizer.GetString(AppStrings.subCategoryNotCreated).Value);
            }

            return Ok(createSubCategory.ToSubCategoryDto());
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        [ApiExplorerSettings(GroupName = "v1-admin")]
        public async Task<ActionResult<SubCategoryDto>> UpdateSubCategory(int id, [FromForm] SubCategoryUpdateRequestDto subCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imagePath = await _fileService.SaveFileAsync(subCategoryDto.Image, "images", "subCategories");
            var subCategory = subCategoryDto.ToSubCategoryFromUpdateRequestDto(id, imagePath);
            var updatedSubCategory = await _subCategoryRepository.UpdateSubCategoryAsync(subCategory);

            if (updatedSubCategory == null)
            {
                return NotFound(_localizer.GetString(AppStrings.subCategoryNotFound).Value);
            }

            return Ok(updatedSubCategory.ToSubCategoryDto());
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        [ApiExplorerSettings(GroupName = "v1-admin")]
        public async Task<ActionResult<SubCategoryDto>> DeleteSubCategory(int id)
        {
            var subCategory = await _subCategoryRepository.DeleteSubCategoryAsync(id);
            if (subCategory == null)
            {
                return NotFound(_localizer.GetString(AppStrings.subCategoryNotFound).Value);
            }

            _fileService.DeleteFile(subCategory.Image);
            return Ok(subCategory.ToSubCategoryDto());
        }

        [HttpPost("bulk-create-subcategories")]
        [ApiExplorerSettings(GroupName = "v1-admin")]
        public async Task<ActionResult<List<SubCategoryDto>>> CreateSubCategories([FromBody] List<SubCategoryJsonCreateRequestDto> subCategoryDtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subCategoryDtosList = new List<SubCategoryDto>();

            foreach (var subCategoryDto in subCategoryDtos)
            {
                var subCategory = subCategoryDto.ToSubCategoryFromCreateRequestJsonDto();
                var createSubCategory = await _subCategoryRepository.CreateSubCategoryAsync(subCategory);

                if (createSubCategory == null)
                {
                    return BadRequest(_localizer.GetString(AppStrings.subCategoryNotCreated).Value);
                }

                subCategoryDtosList.Add(createSubCategory.ToSubCategoryDto());
            }

            return Ok(subCategoryDtosList);
        }
    }
}
