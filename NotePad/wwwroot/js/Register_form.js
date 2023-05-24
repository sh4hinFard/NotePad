    //form Submit
    $(document).ready(function () {
        $("#Registerform").submit(function (evt) {
            evt.preventDefault();
            if ($('#registerPassword').val() == $('#registerRepeatPassword').val()) {
                var formData = new FormData($(this)[0]);
                $.ajax({
                    url: "/Account/Register",
                    type: 'POST',
                    data: formData,
                    async: false,
                    cache: false,
                    contentType: false,
                    enctype: 'multipart/form-data',
                    processData: false,
                    success: function (response) {
                        console.log(response);
                        if (response === "Failed") {
                            Swal.fire('This User already exists!')
                        } else {
                            Swal.fire({
                                position: 'center',
                                icon: 'success',
                                title: 'Your account has been created. Please check your email box and activate your account.',
                                showConfirmButton: false,
                                timer: 1500
                            });
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log(textStatus, errorThrown);
                    }
                });
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Passwords are not same!',
                })

            }

        });
        });