function onNhanVienPopupClose(e) {
    var nhanVienGrid = $('#nhanvienGrid').data('kendoGrid');
    if (nhanVienGrid) {
        nhanVienGrid.dataSource.read();
    }
    e.sender.content('');
}
function onNhanVienPopupOpen(w) {
    if (w.sender) {
        kendo.ui.progress(w.sender.element, true);
    }
}
function editNhanVien(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));

    var wnd = $("#NhanVienPopup").data("kendoWindow");
    if (wnd) {
        wnd.refresh({
            url: "/NhanVien/Edit/" + dataItem.Id
        });
        wnd.title("Cập nhật thông tin nhân Viên");
        wnd.center().open();
        wnd.maximize();
    }
}
function deleteNhanVien(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    abp.message.confirm(
        'Bạn có chắc muốn xóa nhân viên này khỏi hệ thống?',
        null,
        function (isConfirmed) {
            if (isConfirmed) {
                if (dataItem.Id) {
                    abp.ui.setBusy(
                        null,
                        abp.ajax({
                            contentType: 'application/json; charset=utf-8',
                            url: '/NhanVien/Delete',
                            data: JSON.stringify({ id: dataItem.Id })
                        }).done(function (result) {
                            if (result) {
                                if (result.success) {
                                    abp.message.success('Xóa nhân viên thành công','Đã xóa nhân viên');
                                    var nhanVienGrid = $('#nhanvienGrid').data('kendoGrid');
                                    if (nhanVienGrid) {
                                        nhanVienGrid.dataSource.read();
                                    }
                                }
                            }
                        })
                    );
                }
            }
        }
    );
}