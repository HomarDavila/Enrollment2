$(document).ready(function () {
    function isValid() {
        var form = document.getElementById("changePasswordForm");
        var isOk = true;
        var isOkPassword = validPassword2();
        var isOkConfirmPassword = validPassword();
        var isOkForm = form.checkValidity();
        isOk = isOkConfirmPassword && isOkForm && isOkPassword;
        return isOk;
    };

    $('#changePasswordForm').submit(function (e) {
        e.preventDefault();
        if (isValid()) {
            $.ajax({
                url: urlChangePasswordExternal,
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
                    //console.log(data);
                    if (data.code === 0) {
                        swal({
                            title: i18next.t('common_succesfull'),
                            html: i18next.t('Message_Success'),
                            type: "success",
                            confirmButtonText: "OK"
                        }).then(function(result) {
                            if (result.value) {
                                window.location.href = urlHome;
                                //CleanForm();
                            }
                        });
                    } else {
                        if (data.code > 0) {
                            swal({
                                title: i18next.t('common_warning'),
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
        }
    });

});

function validPassword2() {    
    var password = document.getElementById("register-password-field");
    var divToWrite = document.getElementById("registerUser_password_validation_one");
    var divToWrite2 = document.getElementById("registerUser_password_validation_two");
    var divToWrite3 = document.getElementById("registerUser_password_validation_three");
    var isOk, isOk2, isOk3;
    if (password.value.length < 8 || password.value.length > 20) {
        password.setCustomValidity(i18next.t('registerUser_password_validation'));
        divToWrite.classList.remove("custom-valid-feedback");
        divToWrite.classList.add("custom-invalid-feedback");
        isOk = false;
    } else {
        divToWrite.classList.remove("custom-invalid-feedback");
        divToWrite.classList.add("custom-valid-feedback");   
        isOk = true;
    }
    if (hasNumber(password.value)) {
        divToWrite2.classList.remove("custom-invalid-feedback");
        divToWrite2.classList.add("custom-valid-feedback");        
        isOk2 = true;
    } else {
        password.setCustomValidity(i18next.t('registerUser_password_validation'));
        divToWrite2.classList.remove("custom-valid-feedback");
        divToWrite2.classList.add("custom-invalid-feedback");
        isOk2 = false;
    }
    if (/[#$%&'()*+,-./:;<=>?[\]^_`{|}~]/.test(password.value)) {
        divToWrite3.classList.remove("custom-invalid-feedback");
        divToWrite3.classList.add("custom-valid-feedback");
        isOk3 = true;
    } else {
        password.setCustomValidity(i18next.t('registerUser_password_validation'));
        divToWrite3.classList.remove("custom-valid-feedback");
        divToWrite3.classList.add("custom-invalid-feedback");
        isOk3 = false;
    }
    var isOk4 = isOk && isOk2 && isOk3;
    if (isOk4) {
        password.setCustomValidity("");
    }
    return isOk4;
}

function validPassword() {
    var password = document.getElementById("register-password-field");
    var confirm_password = document.getElementById("register-confirm-password-field");
    if (password.value !== confirm_password.value) {
        confirm_password.setCustomValidity(i18next.t('registerUser_password_validation'));
        var divToWrite = document.getElementById("confirmPasswordInvalidFeedback");
        divToWrite.innerHTML = i18next.t('registerUser_ConfirmPassword_validation');
        return false;
    } else {
        confirm_password.setCustomValidity('');
        var divToWrite2 = document.getElementById("confirmPasswordInvalidFeedback");
        divToWrite2.innerHTML = '';
        return true;
    }
}



function BuildRequest() {
    var oldPassword = null;
    var newPassword = null;


    userName = $("#userName-field").val();
    mpi = $("#mpi-field").val();
    oldPassword = $("#old-register-password-field").val();
    newPassword = $("#register-password-field").val();

    var obj = new Object();

    //obj.Id = UserId;
    obj.userName = userName;
    obj.MPI = mpi;
    obj.OldPassword = oldPassword;
    obj.NewPassword = newPassword;
    return obj;
}

//function CleanForm() {
//    $("#user-field").val('');
//    $("#password-field").val('');
//}

function RedirectToLogin() {
    window.location.href = urlLogin;
}

function hasNumber(myString) {
    return /\d/.test(myString);
}

(function ($) {
    $.fn.inputFilter = function (inputFilter) {
        return this.on("input keydown keyup mousedown mouseup select contextmenu drop", function () {
            if (inputFilter(this.value)) {
                this.oldValue = this.value;
                this.oldSelectionStart = this.selectionStart;
                this.oldSelectionEnd = this.selectionEnd;
            } else if (this.hasOwnProperty("oldValue")) {
                this.value = this.oldValue;
                this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
            }
        });
    };
}(jQuery));


$('.ShowPassword').on('click', function () {
    let icon = $(this).find('i');
    let input = $(this).closest('.input-group').find('.PasswordInput');

    if (icon.attr('class') == 'fas fa-eye') {
        input.attr('type', 'text');
        icon.attr('class', 'fas fa-eye-slash');
    } else {
        input.attr('type', 'password');
        icon.attr('class', 'fas fa-eye');
    }
});