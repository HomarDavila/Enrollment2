using Audit.WebApi;
using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.Logging;
using Core.API.Helpers;
using Core.API.Model;
using Core.API.Model.Response;
using Domain.Custom_Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;

namespace Core.API.Controllers
{
    //[AuditApi(EventTypeName = "{controller}/{action} ({verb})", IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
    [RoutePrefix("api/Common/v1")]
    public class CommonController : ApiController
    {
        private readonly IFileServices fileServices = DependencyFactory.GetInstance<IFileServices>();
        private readonly IConfigurationLib config = DependencyFactory.GetInstance<IConfigurationLib>();
        private readonly ICustomLog logger;
        // GET: File
        public CommonController()
        {
            fileServices = DependencyFactory.GetInstance<IFileServices>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        //[Authorize]
        [Route("CreatePDF")]
        [HttpPost]
        public EResponseBase<PDFResponseV1> CreatePDF([FromBody] PcpPmgMcoRequestV1 request)
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
                    ///
                    EResponseBase<MemberResponseV1> oMember;
                    MemberController oMemberController = new MemberController();
                    oMember = oMemberController.Get(request.MemberId);

                    EResponseBase<PcpPmgMcoResponseV1> oPcpPmgMco;
                    PcpPmgMcoController oPcpPmgMcoController = new PcpPmgMcoController();
                    oPcpPmgMco = oPcpPmgMcoController.GetPcpPmgMco(request);

                    Document docChangeEnrollment = new Document();
                    string strNamePDF = oMember.objeto.MPI + string.Format("{0:ddMMyyyyhhmmss}", DateTime.Now) + ".pdf";
                    string strPathPDF = config.PathEnrollmentCreatePDF + strNamePDF;
                    PdfWriter.GetInstance(docChangeEnrollment, new FileStream(strPathPDF, FileMode.Create));
                    docChangeEnrollment.Open();
                    Chunk c1 = new Chunk("Enrollment cambio de MCO.");
                    Phrase pr = new Phrase
                    {
                        c1
                    };
                    Paragraph p = new Paragraph
                    {
                        pr
                    };
                    docChangeEnrollment.Add(p);

                    PdfPTable tabla = new PdfPTable(3);
                    PdfPCell cell = new PdfPCell(new Phrase("Actualización de datos"))
                    {
                        Colspan = 3,
                        HorizontalAlignment = 1, //0=Izquierda, 1=Centro, 2=Derecha
                        BackgroundColor = new BaseColor(0, 150, 0),
                        //cell.BorderColor = new BaseColor(255, 242, 0);
                        Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER,
                        BorderWidthBottom = 3f,
                        BorderWidthTop = 3f,
                        PaddingBottom = 10f,
                        PaddingLeft = 20f,
                        PaddingTop = 4f
                    };

                    tabla.AddCell(cell);
                    tabla.AddCell("MCO");
                    tabla.AddCell(":");
                    tabla.AddCell(((List<PcpPmgMcoResponseV1>)oPcpPmgMco.listado)[0].MCO.CarrierName);
                    tabla.AddCell("PMG");
                    tabla.AddCell(":");
                    tabla.AddCell(((List<PcpPmgMcoResponseV1>)oPcpPmgMco.listado)[0].PMG.PmgName);
                    tabla.AddCell("PCP");
                    tabla.AddCell(":");
                    tabla.AddCell(((List<PcpPmgMcoResponseV1>)oPcpPmgMco.listado)[0].PCP.Person.FullName);

                    tabla.TotalWidth = 216f;
                    float[] tamanos = new float[] { 1.48f, 0.1f, 1.48f };
                    tabla.SetWidths(tamanos);
                    tabla.SpacingBefore = 20f;
                    tabla.SpacingAfter = 30f;

                    docChangeEnrollment.Add(tabla);
                    docChangeEnrollment.Close();

                    string responseJSON = strNamePDF;
                    logger.Print_Response(responseJSON);
                    EResponseBase<PDFResponseV1> response = new EResponseBase<PDFResponseV1>
                    {
                        objeto = new PDFResponseV1(),
                        Code = 0
                    };
                    response.objeto.filePDF = strNamePDF;
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<PDFResponseV1>(config).setResponseBaseForException(ex);
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