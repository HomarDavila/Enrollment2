$(document).ready(function () {
    var entityRequest = new Object();

    birthdateField("#register-birthdate-field");
    $("#register-ssn-field").inputFilter(function (value) {
        return /^\d*$/.test(value);
    });
    $("#register-zipcode-field").inputFilter(function (value) {
        return /^\d*$/.test(value);
    });
    function isValid() {
        var form = document.getElementById("registerForm");
        var isOk = true;
        var isOkPassword = validPassword2();
        var isOkConfirmPassword = validPassword();
        var isOkBirthDate = validBirthDate();
        var isOkForm = form.checkValidity();
        isOk = isOkConfirmPassword && isOkForm && isOkBirthDate && isOkPassword;
        return isOk;
    };
    $('#registerForm').submit(function (e) {

        $("#register-lastnameOne-field").removeAttr("required");        

        if (
            $("#register-lastnameOne-field").val() === '' &&
            $("#register-lastNameTwo-field").val() === '' 
        ) {
            $("#register-lastnameOne-field").attr("required", true);
        }
        

        e.preventDefault();
        if (isValid()) {

            $.ajax({
                url: urlRegister,
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

                        $.ajax({
                            url: urlSendUserRegisterEmail,
                            dataType: 'json',
                            beforeSend: function () {
                                start = (new Date()).getTime();
                                $.blockUI({ message: '<div class="icon-spinner9 icon-spin icon-lg"></div>', timeout: 60000, overlayCSS: { backgroundColor: "#000000", opacity: .8, cursor: "wait" }, css: { border: 0, padding: 0, backgroundColor: "transparent" } });
                            },
                            complete: function () {
                                end = (new Date()).getTime();
                                var total = end - start;
                                $.unblockUI();
                            },
                            type: 'POST',
                            async: true,
                            data: {
                                entityRequest: BuildRequestEmail()
                            },                            
                            success: function (data) {     
                                console.log('To:' + entityRequest.Email);
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {   
                                console.log('Error:' + errorThrown);
                            }
                        });      

                        swal({
                            title: i18next.t('common_succesfull'),
                            html: i18next.t('Message_Success'),
                            type: "success",
                            confirmButtonText: "OK"
                        }).then(function(result) {
                            if (result.value) {
                                window.location.href = urlLogin;
                                CleanForm();
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

function validBirthDate() {

    var birthdateFormGroup = document.getElementById("birthdateFormGroup");
    var birthdate = document.getElementById("register-birthdate-field");
    var divToWrite = document.getElementById("register-message-birthdate-validation");
    var divToWrite2 = document.getElementById("register-message-birthdate-validation2");

    
    if (birthdate.value === null || birthdate.value === "") {
        
        divToWrite.innerHTML = i18next.t('registerUser_birthdate_validation');
        divToWrite.classList.remove("custom-valid-feedback");
        divToWrite.classList.add("custom-invalid-feedback");
        divToWrite2.classList.add("valid-feedback");
        
        
        
        return false;
    } else {        
        //var divToWrite2 = document.getElementById("register-message-birthdate-validation");        
        divToWrite.classList.remove("custom-invalid-feedback");
        divToWrite.classList.add("custom-valid-feedback");                     
        divToWrite.innerHTML = '';        
        divToWrite2.classList.add("custom-invalid-feedback");
        divToWrite2.classList.remove("custom-valid-feedback");
        divToWrite2.classList.remove("valid-feedback");
                   
        return true;
    }
}

function birthdateField(name) {
    var fecha = new Date();
    var ano = fecha.getFullYear();        
    $(name).dateDropdowns(
        {
            displayFormat: "dmy",
            maxYear: ano,
            minYear: ano - 100,
            monthLabel: i18next.t('month'),
            dayLabel: i18next.t('day'),
            yearLabel: i18next.t('year'),
            submitFormat: "dd-mm-yyyy",
            defaultDateFormat: 'dd-mm-yyyy',
            submitFieldName: name,
            required: true,
            dropdownClass: "custom-select browser-default",
            monthLongValues: [i18next.t('enero'), i18next.t('febrero'), i18next.t('marzo'), i18next.t('abril'), i18next.t('mayo'), i18next.t('junio'), i18next.t('julio'), i18next.t('agosto'), i18next.t('setiembre'), i18next.t('octubre'), i18next.t('noviembre'), i18next.t('diciembre')],
            daySuffixes: false
        });
}

function BuildRequest() {
    var UserName = null;
    var Email = null;
    var PassWithoutEncrypt = null;
    var FirstName = null;
    var LastName1 = null;
    var LastName2 = null;
    var SSNLast4 = null;
    var DateoFBirth = null;
    var ZipCode = null;

    UserName = $("#register-username-field").val();
    Email = $("#register-username-field").val();
    PassWithoutEncrypt = $("#register-password-field").val();
    FirstName = $("#register-names-field").val();
    LastName1 = $("#register-lastnameOne-field").val();
    LastName2 = $("#register-lastNameTwo-field").val();
    SSNLast4 = $("#register-ssn-field").val();
    DateoFBirth = $("#register-birthdate-field").val();
    ZipCode = $("#register-zipcode-field").val();

    var from = $("#register-birthdate-field").val().split("-");
    var parsedDate = new Date(from[2], from[1] - 1, from[0]);

    var obj = new Object();
    obj.UserName = UserName;
    obj.Email = Email;
    obj.PassWithoutEncrypt = PassWithoutEncrypt;
    obj.FirstName = FirstName;
    obj.LastName1 = LastName1;
    obj.LastName2 = LastName2;
    obj.SSNLast4 = SSNLast4;
    obj.DateoFBirth = parsedDate.toJSON();
    obj.ZipCode = ZipCode;
    return obj;
}

function BuildRequestEmail() {
    var request = new Object();   

    var Email = $("#register-username-field").val();
    var FirstName = $("#register-names-field").val();
    var LastName1 = $("#register-lastnameOne-field").val();
    var LastName2 = $("#register-lastNameTwo-field").val();    
    request.NameTo = FirstName + ' ' + LastName1 + ' ' + LastName2;
    request.Email = Email;
    return request;
}
function CleanForm() {
    $("#user-field").val('');
    $("#password-field").val('');
}

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

// catch the event and make changes accordingly
i18next.on('languageChanged', function (lng) {
    birthdateField("#register-birthdate-field");    
});

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