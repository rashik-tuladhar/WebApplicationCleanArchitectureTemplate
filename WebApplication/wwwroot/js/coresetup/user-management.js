var UserManagement = (function (userManagement, $) {
    "use strict";
    userManagement.user = function () {
        var config = {
            base_url: null,
            file_url: "/",
            img_url: "/img/",
            state: {

            }
        };
        var viewModel = {
        };
        return ({
            renderManage: function () {
                jQuery.validator.addMethod("nospace", function (value, element) {
                    return value.indexOf(" ") < 0 && value !== "";
                }, "Please enter text without white space");
                $("#createUser").validate({
                    rules: {
                        FirstName: "required"
                        , LastName: "required"
                        , Gender: "required"
                        , PhoneNumber: {
                            required: true,
                            number: true
                        }
                        , UserName: {
                            required: true,
                            nospace: true
                        }
                        , Role: "required"
                    },
                    messages: {
                        FirstName: "Please enter the first name"
                        , LastName: "Please enter the last name"
                        , Gender: "Please select a gender"
                        , PhoneNumber: {
                            required: "Please enter the phone number",
                            number: "Please enter a valid phone number"
                        }
                        , UserName: {
                            required: "Please enter the username",
                            nospace: "Please enter the username without space"
                        }
                        , Role: "Please select a role"
                    },
                    highlight: function (element, errorClass, validClass) {
                        if ($(element).hasClass("select2-hidden-accessible")) {
                            $(element).next().contents().find(".select2-selection--single").addClass(errorClass);
                        } else {
                            $(element).addClass(errorClass);
                        }
                    },
                    unhighlight: function (element, errorClass, validClass) {
                        if ($(element).hasClass("select2-hidden-accessible")) {
                            $(element).next().contents().find(".select2-selection--single").removeClass(errorClass);
                        } else {
                            $(element).removeClass(errorClass);
                        }
                    }
                });

                $("#UserName").on("blur",
                    function () {
                        var hdnRowid = $("#hdnRowId").val();
                        if (hdnRowid === "") {
                            $.ajax({
                                type: "POST",
                                url: "/CoreSetup/User/CheckUserName",
                                data: { UserName: $(this).val() },
                                success: function (data) {
                                    if (data.code === "111") {
                                        alert('error');
                                        $("#userNameError").text(data.message);
                                        $("#UserName").addClass("error");
                                        $("#btnAddUser").prop("disabled", true);
                                    } else {
                                        $("#userNameError").text("");
                                        $("#UserName").removeClass("error");
                                        $("#btnAddUser").prop("disabled", false);
                                    }
                                }
                            });
                        }
                    });


                $("#Gender").select2().on('change', function () {
                    $(this).valid();
                });
                $("#Role").select2().on('change', function () {
                    $(this).valid();
                });
            },
            renderIndex: function () {
                $('#userList').DataTable({
                    "responsive": true,
                    "processing": true,
                    "serverSide": true,
                    "ajax": {
                        "type": 'POST',
                        "dataType": 'json',
                        "url": "/CoreSetup/User/GetGridDetails",
                        'data': function (json) {
                            return json;
                        }
                    },
                    "lengthMenu":
                        [
                            [25, 50, 100],
                            [25, 50, 100]
                        ],
                    "columns": [
                        { 'data': 'UserName', "orderable": false },
                        { 'data': 'FullName', "orderable": false },
                        { 'data': 'PhoneNumber', "orderable": false },
                        { 'data': 'Status', "orderable": false },
                        { 'data': 'Action', "orderable": false }
                    ],
                    "columnDefs":
                        [
                            { "className": "text-center", "targets": [3] },
                            { "className": "text-center", "targets": [4] }
                ],
                    "initComplete": function () {
                        $("#userList").on("click", ".confirmation",
                            function (event) {
                                event.preventDefault();
                                Swal.fire({
                                    title: "Confirmation",
                                    text: "Are you sure to carry out the operation?",
                                    type: 'info',
                                    showCancelButton: true,
                                    confirmButtonColor: '#2C94FB',
                                    cancelButtonColor: '#ff2801',
                                    confirmButtonText: 'Yes'
                                }).then((result) => {
                                    if (!result.value) {
                                        event.preventDefault();
                                    } else {
                                        $(location).prop("href", $(this).prop("href"));
                                    }
                                });
                            });
                    }
                });
            }
        });
    };
    return userManagement;
}(UserManagement || {}, jQuery));