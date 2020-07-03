using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using AutoMapper;
using Repositories;
using Entities.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using LoggerService;

namespace Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public UploadController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        
        [EnableCors("MangoPolicy")]
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("media");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                //  Validation
                var supportedTypes = new[] { "jpg", "jpeg", "gif", "png", "bmp"};
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);  
                if (!supportedTypes.Contains(fileExt)) {  
                    _logger.LogError("File Extension Is InValid.");
                    return BadRequest("File Extension Is InValid - Only Upload JPG/GIF/PNG/BMP Files");
                } else if (file.Length > (8 * 1024 * 1024) ) {  
                    _logger.LogError($"File size Should Be UpTo {(8 * 1024 * 1024)} KB");
                    return BadRequest($"File size Should Be UpTo {(8 * 1024 * 1024)} KB");
                }

                if (file.Length > 0) {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);

                    if ( System.IO.File.Exists(fullPath) ) {
                        fileName = Guid.NewGuid().ToString() + "." + fileExt;
                        fullPath = Path.Combine(pathToSave, fileName);
                    }
                    
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    
                    _logger.LogInfo($"Uploaded new file @ {dbPath}.");
                    return Ok(new { dbPath });
                }
                else
                {
                    _logger.LogError("Empty file.");
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
        }

    }
}