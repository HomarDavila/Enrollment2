using Audit.WebApi;
using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.Logging;
using Core.API.Helpers;
using Core.API.Model;
using Core.API.Model.Response;
using CoreAPI.Common;
using Domain.Entity_Models;
using Renci.SshNet;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;

namespace Core.API.Controllers
{
    //[AuditApi(EventTypeName = "{controller}/{action} ({verb})", IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
    [RoutePrefix("api/File/v1")]
    public class FileController : ApiController
    {
        private readonly IFileServices fileServices = DependencyFactory.GetInstance<IFileServices>();
        private readonly IConfigurationLib config = DependencyFactory.GetInstance<IConfigurationLib>();
        private readonly ICustomLog logger;
        // GET: File
        public FileController()
        {
            fileServices = DependencyFactory.GetInstance<IFileServices>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        //[Authorize]
        [Route("GetFile/{FileId}")]
        [HttpGet]
        public EResponseBase<FileResponseV1> GetFile(int FileId)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(FileId);
                    EResponseBase<Files> responseJSON = fileServices.GetFile(FileId);
                    logger.Print_Response(responseJSON);
                    EResponseBase<FileResponseV1> response = Mapper.Map<EResponseBase<FileResponseV1>>(responseJSON);

                    //using (SftpClient ftpEnrollment = new SftpClient(CustomConfigurationLib.FTPServer, CustomConfigurationLib.FTPPort, CustomConfigurationLib.FTPUser, CustomConfigurationLib.FTPPassword))
                    //{
                    //    ftpEnrollment.Connect();
                    //    ftpEnrollment.ChangeDirectory(((List<FileResponseV1>)response.listado)[0].Path);
                    //    using (FileStream fs = File.Create(CustomConfigurationLib.PathEnrollmentFiles + ((List<FileResponseV1>)response.listado)[0].Name))
                    //    {
                    //        //client.BufferSize = 4 * 1024;
                    //        ftpEnrollment.DownloadFile(((List<FileResponseV1>)response.listado)[0].Path + ((List<FileResponseV1>)response.listado)[0].Name, fs);
                    //    }
                    //}
                    //Byte[] byFileEnrollment = File.ReadAllBytes(string.Concat(CustomConfigurationLib.PathEnrollmentFiles, ((List<FileResponseV1>)response.listado)[0].Name));

                    Byte[] byFileEnrollment;
                    using (SftpClient ftpEnrollment = new SftpClient(CustomConfigurationLib.FTPServer, CustomConfigurationLib.FTPPort, CustomConfigurationLib.FTPUser, CustomConfigurationLib.FTPPassword))
                    {
                        ftpEnrollment.Connect();
                        ftpEnrollment.ChangeDirectory(((List<FileResponseV1>)response.listado)[0].Path);
                        byFileEnrollment = ftpEnrollment.ReadAllBytes(((List<FileResponseV1>)response.listado)[0].Path + ((List<FileResponseV1>)response.listado)[0].Name);
                    }

                    ((List<FileResponseV1>)response.listado)[0].Content = Convert.ToBase64String(byFileEnrollment);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<FileResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        //[Authorize]
        [Route("GetByFilters")]
        [HttpPost]
        public EResponseBase<FileResponseV1> Get([FromBody] FileRequestV1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(request);
                    ////////var responseJSON = memberServices.Get(request.MPI, request.Last4SSN, request.DateOfBirth, request.FirstName, request.FirtLastName, request.SecondLastName);
                    int responseJSON = 0;
                    logger.Print_Response(responseJSON);
                    EResponseBase<FileResponseV1> response = Mapper.Map<EResponseBase<FileResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<FileResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        //[Authorize]
        [Route("GetEnrollmentFiles/{MemberId}")]
        [HttpGet]
        public EResponseBase<FileResponseV1> GetEnrollmentFiles(int MemberId)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(MemberId);
                    EResponseBase<Files> responseJSON = fileServices.GetEnrollmentFiles(MemberId);
                    logger.Print_Response(responseJSON);
                    EResponseBase<FileResponseV1> response = Mapper.Map<EResponseBase<FileResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<FileResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }

        }


        //[Authorize]
        [Route("DisabledEnrollmentFiles")]
        [HttpPost]
        public EResponseBase<FileResponseV1> DisabledEnrollmentFiles([FromBody] FileRequestV1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(request);
                    EResponseBase<Files> responseJSON = fileServices.Disabled(request.Id, request.Enabled);
                    logger.Print_Response(responseJSON);
                    EResponseBase<FileResponseV1> response = Mapper.Map<EResponseBase<FileResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<FileResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        //[Authorize]
        [Route("SetEnrollmentFiles")]
        [HttpPost]
        public EResponseBase<FileResponseV1> SetEnrollmentFiles([FromBody] FileRequestV1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(request);

                    //File.WriteAllBytes(string.Concat(CustomConfigurationLib.PathEnrollmentFiles, request.Name), Convert.FromBase64String(request.Content.Substring(request.Content.LastIndexOf(",") + 1)));
                    //using (SftpClient ftpEnrollment = new SftpClient(CustomConfigurationLib.FTPServer, CustomConfigurationLib.FTPPort, CustomConfigurationLib.FTPUser, CustomConfigurationLib.FTPPassword))
                    //{
                    //    ftpEnrollment.Connect();
                    //    ftpEnrollment.ChangeDirectory(CustomConfigurationLib.FTPFileEnrollment);
                    //    using (FileStream fs = new FileStream(CustomConfigurationLib.PathEnrollmentFiles + request.Name, FileMode.Open))
                    //    {
                    //        ftpEnrollment.BufferSize = 4 * 1024;
                    //        ftpEnrollment.UploadFile(fs, Path.GetFileName(CustomConfigurationLib.PathEnrollmentFiles + request.Name));
                    //    }
                    //}

                    request.Name = string.Concat(request.MemberId,
                                "_",
                                request.Name.Substring(0, request.Name.IndexOf(".")),
                                "_",
                                DateTime.Now.ToString("yyyyMMddHHmmss"),
                    request.Name.Substring(request.Name.LastIndexOf("."), request.Name.Length - request.Name.LastIndexOf(".")));

                    using (SftpClient ftpEnrollment = new SftpClient(CustomConfigurationLib.FTPServer, CustomConfigurationLib.FTPPort, CustomConfigurationLib.FTPUser, CustomConfigurationLib.FTPPassword))
                    {
                        ftpEnrollment.Connect();
                        ftpEnrollment.ChangeDirectory(CustomConfigurationLib.FTPFileEnrollment);
                        ftpEnrollment.BufferSize = 4 * 1024;
                        ftpEnrollment.UploadFile(new MemoryStream(Convert.FromBase64String(request.Content.Substring(request.Content.LastIndexOf(",") + 1))), Path.GetFileName(CustomConfigurationLib.PathEnrollmentFiles + request.Name));
                    }

                    Files oFile = new Files
                    {
                        Id = request.Id,
                        MemberId = request.MemberId,
                        Name = request.Name,
                        Path = CustomConfigurationLib.FTPFileEnrollment,
                        Extension = request.Extension,
                        CreatedBy = request.CreatedBy,
                        CreatedOn = request.CreatedOn,
                        Enabled = request.Enabled
                    };
                    EResponseBase<Files> responseJSON = fileServices.InsertOrUpdate(oFile);
                    logger.Print_Response(responseJSON);
                    EResponseBase<FileResponseV1> response = Mapper.Map<EResponseBase<FileResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<FileResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Route("GetPDF")]
        [HttpPost]
        public EResponseBase<FileResponseV1> GetPDF([FromBody] FileRequestV1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(request);
                    //using (SftpClient ftpEnrollment = new SftpClient(CustomConfigurationLib.FTPServer, CustomConfigurationLib.FTPPort, CustomConfigurationLib.FTPUser, CustomConfigurationLib.FTPPassword))
                    //{
                    //    ftpEnrollment.Connect();
                    //    ftpEnrollment.ChangeDirectory(CustomConfigurationLib.FTPFileChangeMCO);
                    //    using (FileStream fs = File.Create(CustomConfigurationLib.PathEnrollmentCreatePDF + request.Name))
                    //    {
                    //        //client.BufferSize = 4 * 1024;
                    //        ftpEnrollment.DownloadFile(CustomConfigurationLib.FTPFileChangeMCO + request.Name, fs);
                    //    }
                    //}
                    //Byte[] byPDF = File.ReadAllBytes(string.Concat(CustomConfigurationLib.PathEnrollmentCreatePDF, request.Name));

                    Byte[] byPDF;
                    using (SftpClient ftpEnrollment = new SftpClient(CustomConfigurationLib.FTPServer, CustomConfigurationLib.FTPPort, CustomConfigurationLib.FTPUser, CustomConfigurationLib.FTPPassword))
                    {
                        ftpEnrollment.Connect();
                        ftpEnrollment.ChangeDirectory(CustomConfigurationLib.FTPFileChangeMCO);
                        using (FileStream fs = File.Create(CustomConfigurationLib.PathEnrollmentCreatePDF + request.Name))
                        {
                            byPDF = ftpEnrollment.ReadAllBytes(CustomConfigurationLib.FTPFileChangeMCO + request.Name);
                        }
                    }

                    EResponseBase<FileResponseV1> responseJSON = new EResponseBase<FileResponseV1>
                    {
                        objeto = new FileResponseV1(),
                        Code = 0
                    };
                    responseJSON.objeto.Content = Convert.ToBase64String(byPDF);
                    logger.Print_Response(responseJSON);
                    EResponseBase<FileResponseV1> response = Mapper.Map<EResponseBase<FileResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<FileResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Route("SetPDF")]
        [HttpPost]
        public EResponseBase<FileResponseV1> SetPDF([FromBody] FileRequestV1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(request);
                    using (SftpClient ftpEnrollment = new SftpClient(CustomConfigurationLib.FTPServer, CustomConfigurationLib.FTPPort, CustomConfigurationLib.FTPUser, CustomConfigurationLib.FTPPassword))
                    {
                        ftpEnrollment.Connect();
                        ftpEnrollment.ChangeDirectory(CustomConfigurationLib.FTPFileChangeMCO);
                        using (FileStream fs = new FileStream(CustomConfigurationLib.PathEnrollmentCreatePDF + request.Name, FileMode.Open))
                        {
                            ftpEnrollment.BufferSize = 4 * 1024;
                            ftpEnrollment.UploadFile(fs, Path.GetFileName(CustomConfigurationLib.PathEnrollmentCreatePDF + request.Name));
                        }
                    }
                    EResponseBase<FileResponseV1> responseJSON = new EResponseBase<FileResponseV1>
                    {
                        objeto = new FileResponseV1(),
                        Code = 0
                    };
                    logger.Print_Response(responseJSON);
                    EResponseBase<FileResponseV1> response = Mapper.Map<EResponseBase<FileResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<FileResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        private void ConfigureService()
        {
            fileServices.Transaction = RequestUtility.GetHeaders().Transaction;
            fileServices.Logger = logger;
        }

        private CustomHeader ConfigureLogHeader()
        {
            CustomHeader header = RequestUtility.GetHeaders();
            logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, header.Transaction);
            logger.Header = RequestHelpers.AuditUserData(header);
            return header;
        }
    }
}