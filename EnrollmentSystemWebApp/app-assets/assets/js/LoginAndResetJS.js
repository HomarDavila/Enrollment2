$(document).ready(function () {
       
    $('#password-field').bind("enterKey", function (e) {
        $('#btnLoginSubmit').click();
    });
    $('#password-field').keyup(function (e) {
        if (e.keyCode == 13) {
            $(this).trigger("enterKey");
        }
    });


    $('#resetpassword-user-field').bind("enterKey", function (e) {
        $('#btnResetPasswordSubmit').click();
    });
    $('#resetpassword-user-field').keyup(function (e) {
        if (e.keyCode == 13) {
            $(this).trigger("enterKey");
        }
    });


    $('#loginForm').submit(function (e) {
        //debugger;
        e.preventDefault();
        var form = document.getElementById("loginForm");
        var isOkForm = form.checkValidity();
        if (isOkForm) {
            $.ajax({
                url: urlLogin,
                dataType: 'json',
                beforeSend: function () {
                    start = (new Date()).getTime();
                    $.blockUI({ message: '<div class="loading-icon  fa fa-spinner fa-spin fa-3x fa-fw"></div>', timeout: 60000, overlayCSS: { backgroundColor: "#000000", opacity: .8, cursor: "wait" }, css: { border: 0, padding: 0, backgroundColor: "transparent" } });
                },
                complete: function () {
                    end = (new Date()).getTime();
                    var total = end - start;
                    $.unblockUI();
                },
                data: BuildRequest(),
                type: 'POST',
                async: true,
                success: function (data) {
                    console.log(data);
                    if (data.code === 0) {
                        window.location.href = urlHome;
                        //CleanForm();
                    } else {
                        if (data.code > 0) {
                            swal({
                                title: "Info!",
                                html: i18next.language === 'es' ? data.message : data.messageEN,
                                type: "warning",
                                confirmButtonText: "OK"
                            });
                        } else {
                            swal({
                                title: "Error!",
                                html: i18next.language === 'es' ? data.message : data.messageEN,
                                type: "error",
                                confirmButtonText: "OK"
                            });
                        }

                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    swal({
                        title: "Error!",
                        html: textStatus,
                        type: "error",
                        confirmButtonText: "OK"
                    });
                }
            });
        } else
        {
            e.stopPropagation();
        };        
    });
    $('#resetPasswordForm').submit(function (e) {
        e.preventDefault();
        var form = document.getElementById("resetPasswordForm");
        var isOkForm = form.checkValidity();
        if (isOkForm) {
            $.ajax({
                url: urlResetPassword,
                dataType: 'json',
                beforeSend: function () {
                    start = (new Date()).getTime();
                    $.blockUI({ message: '<div class="loading-icon  fa fa-spinner fa-spin fa-3x fa-fw"></div>', timeout: 60000, overlayCSS: { backgroundColor: "#000000", opacity: .8, cursor: "wait" }, css: { border: 0, padding: 0, backgroundColor: "transparent" } });
                },
                complete: function () {
                    end = (new Date()).getTime();
                    var total = end - start;
                    $.unblockUI();
                },
                data: BuildRequestResetPasswordForm(),
                type: 'POST',
                async: true,
                success: function (data) {
                    console.log(data);
                    if (data.code === 0) {
                        swal({
                            title: "OK!",
                            html: i18next.language === 'es' ? data.message : data.messageEN,
                            type: "success",
                            confirmButtonText: "OK"
                        }).then((result) => {
                            if (result.value) {
                                window.location.href = urlLogin;
                            }
                        });
                        CleanForm();
                    } else {
                        if (data.code > 0) {
                            swal({
                                title: "Info!",
                                html: i18next.language === 'es' ? data.message : data.messageEN,
                                type: "warning",
                                confirmButtonText: "OK"
                            });
                        } else {
                            swal({
                                title: "Error!",
                                html: i18next.language === 'es' ? data.message : data.messageEN,
                                type: "error",
                                confirmButtonText: "OK"
                            });
                        }

                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    swal({
                        title: "Error!",
                        html: textStatus,
                        type: "error",
                        confirmButtonText: "OK"
                    });
                }
            });
        } else {
            e.stopPropagation();
        };
    });
});

function BuildRequest() {
    var UserName = null;
    var Password = null;
    UserName = $("#user-field").val();
    Password = $("#password-field").val();
    
    var obj = new Object();
    obj.UserName = UserName;
    obj.Password = Password;

    return obj;
}
function BuildRequestResetPasswordForm() {
    var UserName = null;    
    UserName = $("#resetpassword-user-field").val();
    var obj = new Object();
    obj.UserName = UserName;
    return obj;
}

function CleanForm() {
    $("#user-field").val('');
    $("#password-field").val('');
}
function CleanResetPasswordForm() {
    $("#resetpassword-user-field").val('');    
}
function RedirectToLogin() {
    window.location.href = urlLogin;
}
