var KycDetailManagement = (function (kycDetailManagement, $) {
    "use strict";
    kycDetailManagement.kycDetail = function () {
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
                $("#kycDetail").DataTable({
                    "responsive": true,
                    "processing": true,
                    "serverSide": true,
                    "ajax": {
                        "type": 'POST',
                        "url": "/KYCDetail/GetGridDetails",
                        "data": function (json) {
                            return json;
                        }
                    },
                    "lengthMenu": [
                        [25, 50, 100],
                        [25, 50, 100]
                    ],
                    "columns": [
                        { 'data': 'RowNum', "orderable": false },
                        { 'data': 'KYCCode', "orderable": false },
                        { 'data': 'FullName', "orderable": false },
                        { 'data': 'DateOfBirth', "orderable": false },
                        { 'data': 'CurrentAddress', "orderable": false },
                        { 'data': 'ContactNumber', "orderable": false },
                        { 'data': 'Email', "orderable": false },
                        { 'data': 'Action', "orderable": false }
                    ],
                    "columnDefs":
                        [
                            { "className": "text-center", "targets": "_all" }
                        ],
                    "initComplete": function () {
                        $("#agentType").on("click", ".confirmation",
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

            },
            renderManage: function () {
                $("#btnApproveDetail").click(function () {
                    debugger
                    $("#hdnApprove").val("Approve")
                });
                $("manage-KYCDetails-form").validate({
                    rules: {
                        FirstName: "required",
                        LastName: "required",
                        DateOfBirth: "required",
                        Gender: "required",
                        ParmanentAddress: "required",
                        CitizenshipNumber: "required",
                        Remarks: "required"
                    },
                    messages: {
                        FirstName: "Please enter first name.",
                        LastName: "Please enter last name.",
                        DateOfBirth: "Please select date of birth",
                        Gender: "Please select gender",
                        ParmanentAddress: "Please enter permanent address.",
                        CitizenshipNumber: "Please enter citizenship number",
                        Remarks: "Please enter citizenship number"
                    },
                    errorPlacement: function (error, element) {
                        var attrName = $(element).attr("name");
                        error.appendTo($("#" + attrName + "_jserror"));
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
            }
        });
    };

    return kycDetailManagement;
}(KycDetailManagement || {}, jQuery));