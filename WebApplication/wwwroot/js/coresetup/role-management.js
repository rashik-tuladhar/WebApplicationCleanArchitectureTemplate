var RoleManagement = (function (roleManagement, $) {
    "use strict";
    roleManagement.role = function () {
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
            renderIndex: function () {
                $('#roleDetails').DataTable({
                    "responsive": true,
                    "processing": true,
                    "serverSide": true,
                    "ajax": {
                        "type": 'POST',
                        "dataType": 'json',
                        "url": "/CoreSetup/Role/GetGridDetails",
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
                        { 'data': 'Name', "orderable": false },
                        { 'data': 'Action', "orderable": false }
                    ],
                    "columnDefs":
                    [
                        { "className": "text-center", "targets": [1] }
                    ]
                });
            },
            renderManage: function () {
                $("#createRole").validate({
                    rules: {
                        RoleName: "required"
                    },
                    messages: {
                        RoleName: "Please Enter the Role Name"
                    }
                });

                $(".checkAll").change(function () {
                    $(this).closest('fieldset').find(':checkbox').prop('checked', $(this).prop("checked"));

                });
                $('.checkRole').change(function () {
                    if (this.checked === false) {
                        $(this).closest('fieldset').find(':checkbox.checkAll').prop('checked', false);
                    }
                    if ($(this).closest('fieldset').find('.checkRole:checked').length === $(this).closest('fieldset').find('.checkRole').length) {
                        $(this).closest('fieldset').find(':checkbox.checkAll').prop('checked', true);
                    }
                });
                $('fieldset').each(function () {
                    if ($(this).find('.checkRole:checked').length === $(this).find('.checkRole').length) {
                        $(this).find(':checkbox.checkAll').prop('checked', true);
                    }
                });
            }
        });
    };
    return roleManagement;
}(RoleManagement || {}, jQuery));