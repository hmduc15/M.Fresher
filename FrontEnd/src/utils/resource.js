/**
 * Declare resource project
 * Author: HMDUC(01/06/2023)
 */

export const MISAResources = {

    //title page
    page__title: {
        ListAsset: "Danh sách tài sản"
    },

    //organization
    organization: {
        Finance: "Sở tài chính"
    },

    //combobox
    combobox: {
        EmptyOpt: "Không có dữ liệu"
    },

    //title form
    form__title: {
        AddTitle: "Thêm tài sản",
        EditTitle: "Sửa tài sản"
    },

    //form mode text:
    form__mode: {
        add: "add",
        edit: "edit",
        duplicate: "duplicate",
    },

    //content toast message
    toast__content: {
        DeleteSuccess: "Xóa tài sản thành công",
        DeleteError: "Xóa tài sản thất bại",
        EditSuccess: "Lưu tài sản thành công",
        EditError: "Lưu tài sản thất bại",
        RequireNotice: "Cần phải nhập thông tin",
        InsertSuccess: "Thêm mới tài sản thành công",
        InsertError: "Thêm mới tài sản thất bại",
        NotAsset: "Bạn chưa chọn tài sản để xóa",
        UpdateSuccess: "Cập nhật tài sản thành công",
        UpdateError: "Cập nhật tài sản thất bại",
        ErrorServer: "Có lỗi xảy ra vui lòng liên MISA để được hỗ trợ"

    },

    //text label
    label__input: {
        AssetName: "Tên tài sản",
        AssetCode: "Mã tài sản",
        DepartmentCode: "Mã bộ phận sử dụng",
        DepartmentName: "Tên bộ phận sử dụng",
        CategoryCode: "Mã loại tài sản",
        CategoryName: "Tên loại tài sản",
        Quantity: "Số lượng",
        Cost: "Nguyên giá",
        LifeTime: "Số năm sử dụng",
        DepreciationRate: "Tỷ lệ hao mòn",
        DepreciationYear: "Giá trị hao mòn năm",
        PurchaseDate: "Ngày mua",
        ProductionYear: "Ngày bắt đầu sử dụng",
        TrackedYear: "Năm theo dõi",
        PriceUsed: "Giá trị còn lại"
    },

    //error message input
    text__error: {
        input_err: "không được để trống"
    },

    //text combobox
    text__combobox: {
        type: "Loại tài sản",
        department: "Bộ phận sử dụng",
    },

    //question popup
    popup: {
        delete_only: "Bạn có muốn xóa tài sản ",
        delete_more: "tài sản đã được chọn. Bạn có muốn xóa các tài sản này khỏi danh sách ?",
        add__cancel: "Bạn có muốn hủy khai báo tài sản này?",
        edit__cancel: "Thông tin thay đổi sẽ không được cập nhật nếu bạn không lưu. Bạn có muốn lưu các thay đổi này?"
    },

    //content buton
    content__button: {
        add_asset: "Thêm tài sản",
        reload_async: "Cập nhật dữ liệu",
        cancel_popup: "Hủy bỏ",
        delete: "Xóa",
        no: "Không",
        no_save: "Không lưu",
        save: "Lưu",
        year: "Năm"
    },

    //tooltip button
    tooltip__btn: {
        edit: "Sửa",
        duplicate: "Nhân bản",
        excel: "Xuất file excel",
        delete: "Xóa",
        notice: "Thông báo",
        setting: " Cài đặt",
        helper: "Trợ giúp",
        profile: "Cá nhân",
        firstPage: "Trang đầu",
        lastPage: "Trang cuối",
        nextPage: "Trang sau",
        prevPage: "Trang trước",
    },
    //table paging
    paging: {
        total: "Tổng số",
        record: "bản ghi",
        rowPerPage: "Số dòng/Trang"
    },

    //title table
    table: {
        title: {
            order: "STT",
            orderTooltip: "Số thứ tự",
            assetCode: "Mã tài sản",
            assetName: "Tên tài sản",
            categoryName: "Loại tài sản",
            departmentName: "Bộ phận sử dụng",
            quantity: "Số lượng",
            depreciationAmount: "HM/KH lũy kế",
            depreciationAmountTooltip: "Hao mòn/Khấu hao lũy kế",
            cost: "Nguyên giá",
            trackedYear: "Năm theo dõi",
            action: "Chức năng",
            residualPrice: "Giá trị còn lại"
        },

        key: {
            checkbox: "checkbox",
            order: "order",
            assetCode: "AssetCode",
            assetName: "AssetName",
            categoryName: "CategoryName",
            departmentName: "DepartmentName",
            quantity: "Quantity",
            depreciationAmount: "DepreciationAmount",
            cost: "Cost",
            trackedYear: "TrackedYear",
            residualPrice: "ResidualPrice"
        }
    }
}




