/*=========================================================================================
    File Name: query-string.js
    Description: internationalization library set language using query string
    --------------------------------------------------------------------------------------
    Item Name: Robust - Responsive Admin Theme
    Version: 1.2
    Author: PIXINVENT
    Author URL: http://www.themeforest.net/user/pixinvent
==========================================================================================*/


$(document).ready(function(){

    /*****************************************
    *               Query String             *
    *****************************************/
    debugger;
    i18next
        .use(window.i18nextBrowserLanguageDetector)
        //.use(window.i18nextXHRBackend)
        .init({
            debug: true,            
            nsSeparator: false,
            keySeparator: false,
                   
            detection: {
                lookupQuerystring: 'lng',
                //order: ['cookie', 'navigator', 'htmlTag', 'localStorage', 'querystring'],
                order: ['querystring'],                
            },            
            lng: String.language,
            getAsync: false,
            fallbackLng:'es',// String.language,
            //backend: {
            //    loadPath: "../app-assets/data/locales/{{lng}}/{{ns}}.json",
            //},
            resources: {
                en: {
                    translation: {
                        "common_warning": "Warning!",
                        "common_info": "info!",
                        "common_unexpectedError": "An unexpected error has occurred",
                        "common_reloadTitel1": "Change Language",
                        "common_reloadSubTitle1": "When you change the language the saved data will be lost, do you want to continue?",
                        "common_reloadButtonOK": "Yes",
                        "common_reloadButtonCancel": "No",                      
                        "common_ButtonYes" : "Yes",
                        "common_ButtonNo": "No",
                        "common_ButtonNext": "Next",
                        "common_ButtonPrevious": "Previous",
                        "common_ButtonClose": "Close",
                        "common_ShouldSelectOne": "(Your should select at least one)",
                        "common_Confirm": "You will confirm the action",
                        "common_Sure": "Are you sure?",
                        "DefaultIndex_Subtitle": "ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.",
                        "DefaultIndex_LearnMore": "Learn More",
                        "DefaultIndex_GettingStarted": "Getting Started",
                        "DefaultIndex_GettingStarted_subtitle": "ASP.NET Web API is a framework that makes it easy to build HTTP services that reach a broad range of clients, including browsers and mobile devices.ASP.NET Web API is an ideal platform for building RESTful applications on the.NET Framework.",
                        "DefaultIndex_GetMoreLibraries": "Get more libraries",
                        "DefaultIndex_GetMoreLibraries_subtitle": "NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.",
                        "DefaultIndex_WebHosting": "Web Hosting",
                        "DefaultIndex_WebHosting_subtitle": "You can easily find a web hosting company that offers the right mix of features and price for your applications.",
                        "DefaultIndex_GoDemo": "Go to Demo",
                        "CustomNavBar_Welcome_Text": "Welcome",
                        "CustomNavBar_CloseSesion_Text": "Close Session",
                        "Index_Search_Person": "Search Beneficiary",
                        "Index_Conselor_Additional_Resources": "Counselor additional Resources",
                        "Index_Configuration": "Configuration",
                        "Index_Search_PCP/PCM": "Search PCP/PCM",
                        "Index_Reporting": "Reporting",
                        "SearchPerson_Tittle": "Search Beneficiary",
                        "SearchPerson_By_MPI": "By MPI",
                        "SearchPerson_By_Others": "By Others",
                        "SearchPerson_MPI": "MPI",
                        "SearchPerson_Search": "Search",
                        "SearchPerson_SSN4": "SSN (Last 4 digits)",
                        "SearchPerson_Birthday": "BirthDay",
                        "SearchPerson_Firstname": "First Name",
                        "SearchPerson_Last_Name": "Last Name",
                        "SearchPerson_Second_Last_Name": "Second Last Name",
                        "SearchPerson_Results": "Results",
                        "SearchPerson_Header_View": "View",
                        "SearchPerson_Header_Application_Number": "Application Number",
                        "SearchPerson_Header_Member_Name": "Beneficiary",
                        "SearchPerson_Header_Contact_Name": "Contact Name",
                        "SearchPerson_Validation_One": "Please Enter your MPI",
                        "SearchPerson_Validation_Two": "The field SSN4 must be only 4 digits",
                        "SearchPerson_Validation_Three": "Unless 2 of 5 fields are required",
                        "ChangeMCO_open": "Next open enrollment period",
                        "Email_valid": "You must write the email address correctly",
                        "Phone_valid": "You must write the cell phone",
                        "Email_Note": "Nota: Recibir\u00E1 la confirmaci\u00f3n del cambio realizado al correo electr\u00f3nico principal.  Seleccione el encasillado de requerir el env\u00EDo de confirmaci\u00f3n a un correo secundario.",
                        "Enrollment_Eligibility": "Eligibility",
                        "Enrollment_MPI": "Contact Name",
                        "Enrollment_TittleTab1": "Enrollment",
                        "Enrollment_TittleTab2": "Change History",
                        "Enrollment_Tittle1": "General Data",
                        "Enrollment_Tittle2": "Select PCP/PMG Optional",
                        "Enrollment_Tittle3": "PCP/PMG results under MCO selected",
                        "Enrollment_Tittle4": "Select one member above to display change history",                       
                        "Enrollment_Check": "Check",
                        "Enrollment_MemberName": "Member Name",
                        "Enrollment_EffectiveDate": "Effective Date",
                        "Enrollment_ExpirationDate": "Expiration Date",
                        "Enrollment_CurrentMCO": "Current MCO",
                        "Enrollment_PCP/PMG": "PCP/PMG",
                        "Enrollment_Comment": "Comment",
                        "Enrollment_SelectNewMCO": "Select new MCO",
                        "Enrollment_Message": "Message",
                        "Enrollment_McoDescription": "MCO Description",
                        "Enrollment_PMGDescription": "PMG Description",
                        "Enrollment_PhisycianName": "Phisycian Name",
                        "Enrollment_Speciality": "Speciality",
                        "Enrollment_NPI": "NPI",
                        "Enrollment_PcpCapacity": "PCP Capacity",
                        "Enrollment_BtnApplyPCP/PMG": "Apply PCP/PMG to selected member",
                        "Enrollment_ChangeSourceMco": "Change Source MCO",
                        "Enrollment_WhochangeMco": "Who change MCO",
                        "Enrollment_ChangeDateMco": "Change Date MCO",
                        "Enrollment_ChangeSourcePcp": "Change Source PCP",
                        "Enrollment_WhochangePcp": "Who change PCP",
                        "Enrollment_ChangeDatePcp": "Change Date PCP",
                        "Enrollment_User": "User",
                        "Enrollment_ChangeSource": "Source of Change",
                        "Enrollment_ChangeDate": "Date of Change",
                        "Enrollment_MCO": "MCO",
                        "Enrollment_PMG": "PMG",
                        "Enrollment_PCP": "PCP", 
                        "Enrollment_MCO_Old": "MCO Old",
                        "Enrollment_PMG_Old": "PMG Old",
                        "Enrollment_PCP_Old": "PCP Old",
                        "Enrollment_Status": "Status", 
                        "Enrollment_CMCO": "Current MCO",
                        "Enrollment_CPMG": "Current PMG",
                        "Enrollment_CPCP": "Current PCP",
                        "Enrollment_Elegible": "Eligible",
                        "Enrollment_Validation_One": "Must be select at least one application",
                        "Enrollment_Validation_Two": "Must be select a same type of MCO",
                        "Enrollment_sucessfull_register": "The data was recorded correctly",  
                        "Enrollment_btnDisplaySelection": "Make MCO Change", 
                        "Enrollment_btnhideselection": "Discard MCO Change",
                        "Enrollment_Org": "Care Management Organization",  
                        "Enrollment_Medical": "Primary Medical Group", 
                        "Enrollment_Seleccion": "choose",
                        "Enrollment_Pregunta": "Are you sure you make the following change ?",
                        "Enrollment_Datosact": "Current data",
                        "Enrollment_Envcorreo": "Send to the mail",
                        "EnrollmentApply": "Apply",
                        "EnrollmentChooseOrganitation": "Choose Aseguradora",
                        "EnrollmentSearchProviders": "Search Providers",
                        "EnrollmentChooseProvider": "Choose Provider",
                        "EnrollmentBeneficiaryDoChange": "Select the beneficiary what do the change",
                        "EnrollmentOnlySelectOne": "(You only can select one)",
                        "EnrollmentSelectCarrier": "Select the insurance carrier to consult",
                        "EnrollmentSelectCriteriaSearchProvider": "Select some criteria to search your provider",
                        "EnrollmentOnlySelectCarrierNA": "(you only can select 5 insurance carriers, on the contrary can omit this step)",
                        "EnrollmentOnlySelectCarrier": "(you only can select 4 insurance carriers, on the contrary can omit this step)",
                        "EnrollmentOnlySeeResumen": "(if you want see the result complete, you can omit this step)",
                        "EnrollmentChooseProviderPrefered": "Choose your provider preferred",
                        "Enrollment_Actua": "Update",
                        "common_succesfull": "Successfull",
                        "AllEnterText": "Enter text",
                        "EnterEmail": "Enter email",
                        "AllCboSelectAll": "All",
                        "AllBtnSearch": "Search",
                        "FullName": "Contact Name",
                        "Members": "Members",
                        "NoData": "No Data Found",
                        "Enrollment_Rejected": "Rejected",
                        "NavBar_Beneficiario": "Search Beneficiary",
                        //DLM - Inicio//
                        "Suppliers_of_list": "Suppliers of list",
                        "Enrollment_Reject_Select": "Select",
                        "Enrollment_Change_Management": "Change Management",
                        "Enrollment_Reject_Select_Descripcion": "Update",
                        "Enrollment_Reject_Cancel": "Cancel",
                        "Enrollment_Reject_Cancel_Description": "Cancel"
                    }
                },
                es : {
                    translation: {
                        "common_warning": "\u0020Advertencia",
                        "common_info": "informativo",
                        "common_unexpectedError": "Ocurri\u00f3 un error inesperado",                     
                        "common_reloadTitel1": "Cambiar idioma",
                        "common_reloadSubTitle1": "Al cambiar el idioma, se perder\u00e1n la informaci\u00f3n. \u00BF Desea continuar?",
                        "common_reloadButtonOK": "Si",
                        "common_reloadButtonCancel": "No",                        
                        "common_ButtonYes" : "Si",
                        "common_ButtonNo": "No",
                        "common_ButtonNext": "Siguiente",
                        "common_ButtonPrevious": "Anterior",
                        "common_ButtonClose": "Cerrar",
                        "common_ShouldSelectOne": "(Debe seleccionar al menos uno)",
                        "common_Confirm": "Confirme la acci\u00f3n",
                        "common_Sure": "\u00BFEst\u00E1 seguro?",
                        "DefaultIndex_Subtitle": "ASP.NET es un framework web gratuito para crear excelentes sitios web y aplicaciones web utilizando HTML, CSS y JavaScript.",
                        "DefaultIndex_LearnMore": "Aprender m\u00E1s",
                        "DefaultIndex_GettingStarted": "Iniciando",
                        "DefaultIndex_GettingStarted_subtitle": "ASP.NET Web API es un marco que facilita la creaci\u00F3n de servicios HTTP que lleguen a una amplia gama de clientes, incluidos navegadores y dispositivos m\u00F3viles.ASP.NET Web API es una plataforma ideal para compilar aplicaciones RESTful en .NET Framework.",
                        "DefaultIndex_GetMoreLibraries": "Conseguir m\u00E1s librerias",
                        "DefaultIndex_GetMoreLibraries_subtitle": "NuGet es una extensi\u00F3n gratuita de Visual Studio que facilita agregar, eliminar y actualizar bibliotecas y herramientas en proyectos de Visual Studio.",
                        "DefaultIndex_WebHosting": "Alojamiento Web",
                        "DefaultIndex_WebHosting_subtitle": "Puede encontrar f\u00E1cilmente una empresa de alojamiento web que ofrezca la combinaci\u00F3n adecuada de caracteristicas y precio para sus aplicaciones.",
                        "DefaultIndex_GoDemo": "Ir al Demo",
                        "CustomNavBar_Welcome_Text": "Bienvenido",
                        "CustomNavBar_CloseSesion_Text": "Cerrar Sesi\u00f3n",
                        "Index_Search_Person": "Buscar Beneficiario",
                        "Index_Conselor_Additional_Resources": "Recursos Adicionales del Counselor",
                        "Index_Configuration": "Configuraci\u00f3n",
                        "Index_Search_PCP/PCM": "Buscar PCP/PCM",
                        "Index_Reporting": "Reportes",
                        "SearchPerson_Tittle": "Buscar Beneficiario",
                        "SearchPerson_By_MPI": "Por MPI",
                        "SearchPerson_By_Others": "Por Otros",
                        "SearchPerson_MPI": "MPI",
                        "SearchPerson_Search": "Buscar",
                        "SearchPerson_SSN4": "SSN (\u00DAltimos 4 d\u00EDgitos)",
                        "SearchPerson_Birthday": "Fecha de Nacimiento",
                        "SearchPerson_Firstname": "Primer Nombre",
                        "SearchPerson_Last_Name": "Apellido Paterno",
                        "SearchPerson_Second_Last_Name": "Apellido Materno",
                        "SearchPerson_Results": "Resultados",
                        "SearchPerson_Header_View": "Ver",
                        "SearchPerson_Header_Application_Number": "N\u00famero de Aplicaci\u00f3n",
                        "SearchPerson_Header_Member_Name": "Beneficiario",
                        "SearchPerson_Header_Contact_Name": "Nombre de Contacto",
                        "SearchPerson_Validation_One": "Por favor ingrese su MPI",
                        "SearchPerson_Validation_Two": "El campo SSN4 debe tener solo 4 d\u00EDgitos",
                        "SearchPerson_Validation_Three": "Al menos 2 de los 5 campos son requeridos",
                        "ChangeMCO_open": "Pr\u00f3ximo per\u00EDodo de inscripci\u00f3n abierta",
                        "Email_valid": "Debe escribir la direcci\u00f3n de correo electr\u00f3nico correctamente",
                        "Phone_valid": "Debe escribir el celular",
                        "Email_Note": "Nota: Recibir\u00E1 la confirmaci\u00f3n del cambio realizado al correo electr\u00f3nico principal.  Seleccione el encasillado de requerir el env\u00EDo de confirmaci\u00f3n a un correo secundario.",
                        "Enrollment_MPI": "Nombre del Contacto",
                        "Enrollment_TittleTab1": "Enrollment",
                        "Enrollment_TittleTab2": "Historial de Cambios",
                        "Enrollment_Tittle1": "Datos Generales",
                        "Enrollment_Tittle2": "Seleccionar PCP/PMG Opcional",
                        "Enrollment_Tittle3": "Resultados PCP/PMG bajo el MCO seleccionado",
                        "Enrollment_Tittle4": "Seleccionar uno de los miembros para mostrar el hist\u00f3rico de cambios",                       
                        "Enrollment_Check": "Seleccionar",
                        "Enrollment_MemberName": "Miembro",
                        "Enrollment_Eligibility": "Eligibilidad",
                        "Enrollment_EffectiveDate": "Fecha efectiva",
                        "Enrollment_ExpirationDate": "Fecha de expiraci\u00f3n",
                        "Enrollment_CurrentMCO": "MCO Actual",
                        "Enrollment_PCP/PMG": "PCP/PMG",
                        "Enrollment_Comment": "Comentario",
                        "Enrollment_SelectNewMCO": "Seleccionar nuevo MCO",
                        "Enrollment_Message": "Mensaje",
                        "Enrollment_McoDescription": "Descripci\u00f3n del MCO",
                        "Enrollment_PMGDescription": "Descripci\u00f3n del PMG",
                        "Enrollment_PhisycianName": "Nombre del m\u00E9dico",
                        "Enrollment_Speciality": "Especialidad",
                        "Enrollment_NPI": "NPI",
                        "Enrollment_PcpCapacity": "Capacidad del PCP",
                        "Enrollment_BtnApplyPCP/PMG": "Aplicar PCP/PMG a los miembros seleccionados",
                        "Enrollment_ChangeSourceMco": "Medio del cambio MCO",
                        "Enrollment_WhochangeMco": "Qui\u00E9n cambi\u00f3 el MCO",
                        "Enrollment_ChangeDateMco": "Fecha del cambio MCO",
                        "Enrollment_ChangeSourcePcp": "Medio del cambio PCP",
                        "Enrollment_WhochangePcp": "Qui\u00E9n cambi\u00f3 el PCP",
                        "Enrollment_ChangeDatePcp": "Fecha del cambio PCP",
                        "Enrollment_User": "Usuario",
                        "Enrollment_ChangeSource": "Origen del Cambio",
                        "Enrollment_ChangeDate": "Fecha del Cambio",
                        "Enrollment_MCO": "MCO",
                        "Enrollment_PMG": "PMG",
                        "Enrollment_PCP": "PCP",
                        "Enrollment_MCO_Old": "MCO Anterior",
                        "Enrollment_PMG_Old": "PMG Anterior",
                        "Enrollment_PCP_Old": "PCP Anterior",
                        "Enrollment_Rejected": "Rechazado",
                        "Enrollment_Status": "Estatus",
                        "Enrollment_CMCO": "MCO Actual",
                        "Enrollment_CPMG": "PMG Actual",
                        "Enrollment_CPCP": "PCP Actual",
                        "Enrollment_Elegible": "Elegible",
                        "Enrollment_btnDisplaySelection": "Realizar Cambio de MCO",
                        "Enrollment_btnhideselection": "Descartar Cambio de MCO",
                        "Enrollment_Org": "Organizaci\u00F3n de Manejo de Cuidado", 
                        "Enrollment_Medical": "Grupo M\u00E9dico Primario", 
                        "Enrollment_Seleccion": "Seleccionar",
                        "Enrollment_Pregunta": "\u00BFEstas seguro de realizar el siguiente cambio?",
                        "Enrollment_Datosact": "Datos Actuales",
                        "Enrollment_Actua": "Actualizaci\u00F3n",
                        "Enrollment_Envcorreo": "Enviar al correo",
                        "EnrollmentApply": "Aplicar",
                        "EnrollmentChooseOrganitation": "Escoger Aseguradora",
                        "EnrollmentSearchProviders": "Buscar Proveedores",
                        "EnrollmentChooseProvider": "Escoger Proveedor",
                        "EnrollmentBeneficiaryDoChange": "Selecciona el beneficiario que va a realizar el cambio",
                        "EnrollmentOnlySelectOne": "(Solo puedes seleccionar uno)",
                        "EnrollmentSelectCarrier": "Seleccione las aseguradoras a consultar",
                        "EnrollmentSelectCriteriaSearchProvider": "Seleccione algunos filtros para buscar a su proveedor",
                        "EnrollmentOnlySelectCarrierNA": "(Puede seleccionar hasta 5 aseguradoras, de lo contrario puede omitir este paso)",
                        "EnrollmentOnlySelectCarrier": "(Puede seleccionar hasta 4 aseguradoras, de lo contrario puede omitir este paso)",
                        "EnrollmentOnlySeeResumen": "(Si desea ver el resultado completo, puede omitir este paso)",
                        "EnrollmentChooseProviderPrefered": "Escoja a su proveedor de preferencia",
                        "AllEnterText": "Ingresar Texto",
                        "EnterEmail": "Ingresar correo",
                        "AllCboSelectAll": "Todos",
                        "AllBtnSearch": "Buscar",
                        "Contact_FullName": "Nombre de Contacto",
                        "Members": "Miembros",                        
                        "Enrollment_Validation_One": "Debe seleccionar al menos una aplicaci\u00F3n",
                        "Enrollment_Validation_Two": "Debe seleccionar el mismo tipo de MCO",
                        "Enrollment_sucessfull_register": "La data fue registrada correctamente",
                        "common_succesfull": "\u00C9xito",
                        "NoData": "No Data Found",
                        "NavBar_Beneficiario": "Buscar Beneficiario",
                        //DLM - Inicio
                        "Suppliers_of_list": "Lista de proveedores",
                        "Enrollment_Reject_Select": "Seleccione",
                        "Enrollment_Change_Management": "Gesti\u00f3n de Cambios",
                        "Enrollment_Reject_Select_Descripcion": "Modificar",
                        "Enrollment_Reject_Cancel": "Cancelar",
                        "Enrollment_Reject_Cancel_Description": "Cancelar"
                    }
                }
            },
            returnObjects: true
        },        
            function (err, t) {
                debugger;
            // resources have been loaded
            jqueryI18next.init(i18next, $);

            $('#mainCreateApplication').localize();          

            if(i18next.language == 'en'){
                $('.lng-nav li a').removeClass('active');
                $('.lng-nav li a[data-lng="en"]').addClass('active');

                $('.lng-dropdown a').removeClass('active');
                var drop_lng = $('.lng-dropdown a[data-lng="en"]').addClass('active');
                $('#dropdown-active-item').html(drop_lng.html());
            }

            if(i18next.language == 'es'){
                $('.lng-nav li a').removeClass('active');
                $('.lng-nav li a[data-lng="es"]').addClass('active');

                $('.lng-dropdown a').removeClass('active');
                var drop_lng = $('.lng-dropdown a[data-lng="es"]').addClass('active');
                $('#dropdown-active-item').html(drop_lng.html());
            }
            //if(i18next.language == 'pt'){
            //    $('.lng-nav li a').removeClass('active');
            //    $('.lng-nav li a[data-lng="pt"]').addClass('active');

            //    $('.lng-dropdown a').removeClass('active');
            //    var drop_lng = $('.lng-dropdown a[data-lng="pt"]').addClass('active');
            //    $('#dropdown-active-item').html(drop_lng.html());
            //}

            //if(i18next.language == 'fr'){
            //    $('.lng-nav li a').removeClass('active');
            //    $('.lng-nav li a[data-lng="fr"]').addClass('active');

            //    $('.lng-dropdown a').removeClass('active');
            //    var drop_lng = $('.lng-dropdown a[data-lng="fr"]').addClass('active');
            //    $('#dropdown-active-item').html(drop_lng.html());
            //}
        });
    i18next.on('languageChanged', function (lng) {
        debugger;
        // resources have been loaded
        jqueryI18next.init(i18next, $);

        $('#mainCreateApplication').localize();          

        if (i18next.language == 'en') {
            $('.lng-nav li a').removeClass('active');
            $('.lng-nav li a[data-lng="en"]').addClass('active');

            $('.lng-dropdown a').removeClass('active');
            var drop_lng = $('.lng-dropdown a[data-lng="en"]').addClass('active');
            $('#dropdown-active-item').html(drop_lng.html());
        }

        if (i18next.language == 'es') {
            $('.lng-nav li a').removeClass('active');
            $('.lng-nav li a[data-lng="es"]').addClass('active');

            $('.lng-dropdown a').removeClass('active');
            var drop_lng = $('.lng-dropdown a[data-lng="es"]').addClass('active');
            $('#dropdown-active-item').html(drop_lng.html());
        }


        // then re-render your app
        //app.render();
         //Keep language in sync
        //req.language = req.locale = req.lng = lng;
        //req.languages = i18next.services.languageUtils.toResolveHierarchy(lng);
        //if (i18next.services.languageDetector) {
        //    i18next.services.languageDetector.cacheUserLanguage(req, res, lng);
        //}
    });     
});