var customValidate = $("#form-nhanvien").kendoValidator().data("kendoValidator");
function AddOrEditNhanVienSuccess(res) {
    if (res && res.success) {
        if (res.type === "create") {
            abp.message.success("Thêm mới nhân viên thành công.");
        } else {
            abp.message.success("Cập nhật nhân viên thành công.");
        }
        var wnd = $("#NhanVienPopup").data("kendoWindow");
        if (wnd) {
            wnd.close();
        }
    }
}
function AddOrEditNhanVienBegin() {
    
}
function AccountLoginEnd(args) {
    var id = $("#hdnAccountLoginId").val();
    if (args.type && args.type === "read") {
        if (args.response && args.response.length > 0) {
            var dropdownlist = $('#UserId').data("kendoDropDownList");
            if (dropdownlist) {
                dropdownlist.value(id);
            }
        }
    }
}
$(document).ready(function () {
    $('input[name="NgaySinh"]').attr("data-val-date", "Ngày sinh không hợp lệ");
});