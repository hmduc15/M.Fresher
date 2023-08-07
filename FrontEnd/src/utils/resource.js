
/**
 * Declare resource project
 * Author: HMDUC(01/06/2023)
 */
export const MISAResources = {

    //name
    name: {
        nameSotfware: "MISA QLTS"
    },

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
        EditTitle: "Sửa tài sản",
        DuplicateTitle: "Nhân bản tài sản",
        AddReceipt: "Thêm chứng từ điều chuyển",
        EditReceipt: "Sửa chứng từ điều chuyển",
        ComonInfor: "Thông tin chung",
        TranferInfor: "Thông tin tài sản điều chuyển",
        AddReceiptAsset: "Chọn tài sản điều chuyển"
    },

    //placeholder
    placeholder: {
        AssetCode: "Nhập mã tài sản",
        AssetName: "Nhập tên tài sản",
        DepartmentCode: "Chọn mã bộ phận sử dụng",
        CategoryCode: "Chọn mã loại tài sản",
        Note: "Nhập ghi chú",
        FullName: "Nhập họ và tên",
        Representation: "Nhập đại diện",
        PositionName: "Nhập chức vụ",
        SearchInput: "Tìm kiếm tài sản"
    },

    //form mode text:
    form__mode: {
        add: "add",
        edit: "edit",
        duplicate: "duplicate",
    },

    number: {
        quantity: 1,
        depreciation: 0.01
    },

    //content toast message
    toast__content: {
        DeleteSuccess: "Xóa tài sản thành công.",
        DeleteReceiptSuccess: "Xóa chứng từ thành công.",
        DeleteError: "Xóa tài sản thất bại.",
        DeleteReceiptError: "Xóa chứng từ thất bại.",
        EditSuccess: "Lưu tài sản thành công.",
        EditError: "Lưu tài sản thất bại.",
        RequireNotice: "Cần phải nhập thông tin.",
        InsertSuccess: "Thêm mới tài sản thành công.",
        InsertReceiptSuccess: "Thêm mới chứng từ thành công.",
        InsertError: "Thêm mới tài sản thất bại.",
        NotAsset: "Bạn chưa chọn tài sản để xóa.",
        UpdateSuccess: "Cập nhật tài sản thành công.",
        UpdateReceiptSuccess: "Cập nhật chứng từ thành công.",
        UpdateError: "Cập nhật tài sản thất bại.",
        ErrorServer: "Có lỗi xảy ra vui lòng liên MISA để được hỗ trợ.",
        NoEdit: "Tài sản chưa được thay đổi.",
        NoEditReceipt: "Chứng từ chưa được thay đổi."

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
        PriceUsed: "Giá trị còn lại",
        ReceiptCode: "Mã chứng từ",
        ReceiptDate: "Ngày chứng từ",
        TranferDate: "Ngày điều chuyển",
        Note: "Ghi chú",
        DepartmentCodeNew: "Bộ phận sử dụng mới",
        UserNew: "Người sử dụng mới",
        ckboxSelectDepartment: "Chọn ban giao nhận",
        ckboxDepartmentBefore: "Thêm ban giao nhận từ lần nhập trước",
        Stt: "STT",
        FullName: "Họ và tên",
        Representation: "Đại diện",
        PositionName: "Chức vụ"
    },

    //error message input
    text__error: {
        inputErr: "không được để trống.",
        tranferDate: "Ngày điều chuyển phải lớn hơn ngày chứng từ."
    },

    //text combobox
    text__combobox: {
        type: "Loại tài sản",
        department: "Bộ phận sử dụng",
    },

    //question popup
    popup: {
        deleteOnly: "Bạn có muốn xóa tài sản ",
        deleteMore: "tài sản đã được chọn. Bạn có muốn xóa các tài sản này khỏi danh sách ?",
        addCancel: "Bạn có muốn hủy khai báo tài sản này?",
        editCancel: "Thông tin thay đổi sẽ không được cập nhật nếu bạn không lưu. Bạn có muốn lưu các thay đổi này?",
        addReceipt: "Dữ liệu đã thay đổi bạn có muốn lưu không?",
        choseAsset: "Bạn chưa thêm tài sản điều chuyển.",
        noDepartmentReceipt: "Vui lòng chọn bộ phận sử dụng mới.",
        existDepartment: "Vui lòng chọn bộ phận sử dụng mới khác bộ phận sử dụng tài sản.",
        deleteOnlyRecipt: "Bạn có muốn xóa chứng từ ",
        deleteMoreReceipt: "chứng từ đã được chọn. Bạn có muốn xóa các chứng từ này khỏi danh sách ?",
        noFreeText: "không ?",
        dupliDepart: "Vui lòng chọn bộ phận điều chuyển đến khác bộ phận đang sử dụng.",
        receiptDate: "Ngày điều chuyển phải lớn hơn hoặc bằng ngày chứng từ.",
        asset: "Tài sản",
        accuredReceipt: " đã có phát sinh chứng từ. Bạn không thể xóa chứng từ này.",
        receipt: "- Chứng từ điều chuyển ",
        noMember: "Ban giao nhận không được phép để trống.",
        assetReceipt: " đã có phát sinh chứng từ. Bạn không thể thực hiện chức năng này.",
    },

    //content buton
    content__button: {
        addAsset: "Thêm tài sản",
        reloadAsync: "Cập nhật dữ liệu",
        cancelPopup: "Hủy bỏ",
        delete: "Xóa",
        no: "Không",
        yes: "Có",
        noSave: "Không lưu",
        save: "Lưu",
        year: "Năm",
        cancelForm: "Hủy",
        agree: "Đồng ý",
        tranferIn: "Điều chuyển trong đơn vị",
        tranferOut: "Điều chuyển ngoài đơn vị",
        receive: "Tiếp nhận tài sản",
        addReceipt: "Thêm chứng từ",
        infor: "Thông tin chi tiết",
        choseAsset: "Chọn tài sản",
        clearSelected: "Bỏ chọn",
        viewDetail: "Xem chi tiết phát sinh",
        hideDetail: "Ẩn chi tiết phát sinh"
    },

    //tooltip button
    tooltip__btn: {
        edit: "Sửa",
        duplicate: "Nhân bản",
        excel: "Xuất file excel",
        delete: "Xóa",
        notice: "Thông báo",
        helper: "Trợ giúp",
        profile: "Cá nhân",
        firstPage: "Trang đầu",
        lastPage: "Trang cuối",
        nextPage: "Trang sau",
        prevPage: "Trang trước",
        setting: "Cài đặt",
        collapsed: "Thu gọn",
        expanded: "Mở rộng",
        close: "Đóng (ESC)",
        reload: "Nạp",
        print: "In",
        feedBack: "Phản hồi",
        up: "Di chuyển lên",
        down: "Di chuyển xuống",
        add: "Thêm"
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
            residualPrice: "Giá trị còn lại",
            receiptCode: "Mã chứng từ",
            receiptDate: "Ngày chứng từ",
            tranferDate: "Ngày điều chuyển",
            receiptNote: "Ghi chú",
            orgPrice: "Nguyên giá",
            departmentNameNew: "Bộ phận điều chuyển đến",
            reason: "Lý do",
            userAsset: "Người đang sử dụng",
            newUserAsset: "Người sử dụng mới",
            oldDepartment: "Bộ phận sử dụng"
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
            residualPrice: "ResidualPrice",
            receiptCode: "ReceiptCode",
            receiptDate: "ReceiptDate",
            tranferDate: "TranferDate",
            receiptNote: "Note",
            orgPrice: "OrgPrice",
            departmentNameNew: "DepartmentReceipt",
            reason: "Reason",
            userAsset: "UserAsset",
            newUserAsset: "NewUserAsset",
            oldDepartment: "OldDepartmentName"

        }
    },
    //day of Week for Datepicker
    listDay: ['T2', 'T3', 'T4', 'T5', 'T6', 'T7', 'CN'],

    //drop down setting
    title__dropdown: {
        display: "Tùy chỉnh hiển thị"
    },

    //drop down setting
    setting__display: {
        summary: "Summary",
        pagging: "Paging"
    },

    //sidebar
    sidebar: {
        dashboard: "Tổng quan",
        asset: "Tài sản",
        assetIncrease: "Ghi tăng",
        changeInfor: "Thay đổi thông tin",
        assetAssessment: "Đánh giá lại",
        tranfer: "Điều chuyển tài sản",
        receiptRecommend: "Đề nghị xử lý",
        assetReducing: "Ghi giảm",
        depreciationBusiness: "Tính khấu hao",
        depreciationAsset: "Tính hao mòn",
        assetInventory: "Kiểm kê",
        assetTrilist: "Tài sản HT-ĐB",
        equipmentList: "Công cụ dụng cụ",
        dictionary: "Danh mục",
        search: "Tra cứu",
        report: "Báo cáo"
    },

    //title tab view
    title__tab: {
        tranfer: "Điều chuyển"
    },

    //free text
    free__text: {
        selected: "Đã chọn: ",
        stt: "Số thứ tự"
    },

    colTableAssetChoseEdit: [
        {
            title: "",
            checkbox: true,
            key: "checkbox",
        },
        {
            title: "STT",
            key: "order",
            tooltip: "Số thứ tự",
        },
        {
            title: "Mã tài sản",
            posLeft: true,
            key: "AssetCode",
        },
        {
            title: "Tên tài sản",
            posLeft: true,
            width: 200,
            key: "AssetName",
        },
        {
            title: "Nguyên giá",
            posLeft: false,
            key: "Cost",
        },
        {
            title: "Giá trị còn lại",
            posLeft: false,
            key: "ResidualPrice",
        },
        {
            title: "Bộ phận sử dụng",
            posLeft: true,
            width: 200,
            key: "OldDepartmentName",
        },
        {
            title: "Bộ phận điều chuyển đến",
            posLeft: true,
            width: 200,
            key: "DepartmentReceipt",
        },
        {
            title: "Lý do",
            posLeft: true,
            width: 200,
            key: "Reason",
        },
    ],

    colTableAssetChoseAdd: [
        {
            title: "",
            checkbox: true,
            key: "checkbox",
        },
        {
            title: "STT",
            key: "order",
            tooltip: "Số thứ tự",
        },
        {
            title: "Mã tài sản",
            posLeft: true,
            key: "AssetCode",
        },
        {
            title: "Tên tài sản",
            posLeft: true,
            width: 200,
            key: "AssetName",
        },
        {
            title: "Nguyên giá",
            posLeft: false,
            key: "Cost",
        },
        {
            title: "Giá trị còn lại",
            posLeft: false,
            key: "ResidualPrice",
        },
        {
            title: "Bộ phận sử dụng",
            posLeft: true,
            width: 200,
            key: "DepartmentName",
        },
        {
            title: "Bộ phận điều chuyển đến",
            posLeft: true,
            width: 200,
            key: "DepartmentReceipt",
        },
        {
            title: "Lý do",
            posLeft: true,
            width: 200,
            key: "Reason",
        },
    ],


}




